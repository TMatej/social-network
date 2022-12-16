import { faEdit } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useQuery } from "@tanstack/react-query";
import { axios } from "api/axios";
import { Button } from "components/button";
import { Container } from "components/container";
import { LabeledItem } from "components/labeled-item";
import { Paper } from "components/paper";
import { Tab, Tabs } from "components/tabs";
import { format } from "date-fns";
import { Outlet, useNavigate, useMatch } from "react-router-dom";
import { useStore } from "store";
import { Profile as ProfileType } from "models";
import { ProfileEditDialog } from "components/dialogs/profile-edit-dialog";

type TabKeys = "info" | "galleries" | "wall" | "friends";

export const Profile = () => {
  const match = useMatch("/profile/:id/:tabKey/*");
  const { id, tabKey } = match?.params ?? {};
  const navigate = useNavigate();
  const user = useStore((store) => store.user);
  const openDialog = useStore((store) => store.openDialog);
  const { data: profile } = useQuery(["profile", id], () =>
    axios.get<ProfileType>(`/users/${id}/profile`).then((res) => res.data)
  );

  const isCurrentUser = profile?.user.id === user?.id;

  if (!profile) {
    return (
      <Container>
        <Paper className="p-3">Profile does not exist</Paper>
      </Container>
    );
  }

  return (
    <Container className="p-3">
      <Paper className="p-4 flex gap-4 items-center">
        <div className="border-2 border-white border-opacity-5 bg-cyan-900 w-32 rounded-full overflow-hidden">
          <img
            className="w-full h-full object-contain"
            src="https://picsum.photos/200"
            alt=""
          />
        </div>
        <div className="flex-grow ml-4">
          <span className="text-3xl font-bold">
            {user?.username ?? "Unknown user"}
          </span>
          <div className="flex flex-wrap justify-between items-center">
            <LabeledItem
              label="Member since"
              item={
                profile?.createdAt &&
                format(new Date(profile?.createdAt), "MM/dd/yyyy")
              }
            />
            {isCurrentUser && (
              <Button
                leftIcon={<FontAwesomeIcon icon={faEdit} />}
                onClick={() => {
                  openDialog({
                    title: "Edit profile",
                    Component: ProfileEditDialog,
                    props: {
                      profile: profile,
                    },
                  });
                }}
              >
                Edit
              </Button>
            )}
          </div>
        </div>
      </Paper>
      <Paper className="mt-4 p-4">
        <Tabs
          tabKey={tabKey}
          handleTabChange={(tabKey) =>
            navigate(`/profile/${id}/${tabKey}`, { relative: "path" })
          }
        >
          <Tab<TabKeys> tabKey="info" label="Information" />
          <Tab<TabKeys> tabKey="galleries" label="Gallery" />
          <Tab<TabKeys> tabKey="wall" label="Wall" />
          <Tab<TabKeys> tabKey="friends" label="Friends" />
        </Tabs>
      </Paper>

      <Outlet context={{ profile }} />
    </Container>
  );
};

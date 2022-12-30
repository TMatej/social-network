import {
  faCamera,
  faEdit,
  faUserMinus,
  faUserPlus,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
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
import { Avatar } from "components/avatar";
import { AvatarUploadDialog } from "components/dialogs/avatar-upload-dialog";
import { useIsFollowing } from "hooks/use-is-following";
import { useFollowActions } from "modules/following";

type TabKeys = "info" | "galleries" | "wall" | "following";

export const Profile = () => {
  const { id } = useMatch("/profile/:id/*")?.params ?? {};
  const { tabKey = "wall" } = useMatch("/profile/:id/:tabKey/*")?.params ?? {};
  const navigate = useNavigate();
  const user = useStore((store) => store.user);
  const openDialog = useStore((store) => store.openDialog);
  const { data: profile } = useQuery(["profile", id], () =>
    axios.get<ProfileType>(`/users/${id}/profile`).then((res) => res.data)
  );

  const isFollowing = useIsFollowing();
  const follows = isFollowing(profile?.user.id);
  const { follow, unfollow } = useFollowActions();

  const isCurrentUser = profile?.user.id === user?.id;

  if (!profile) {
    return (
      <Container className="p-3">
        <Paper className="p-3">Profile does not exist</Paper>
      </Container>
    );
  }

  return (
    <Container className="p-3">
      <Paper className="p-4 flex gap-4 items-center">
        <div className="rounded-full relative">
          <Avatar user={profile.user} size="lg" withoutTooltip />
          {isCurrentUser && (
            <Button
              className="!rounded-full absolute -bottom-1 -right-1"
              onClick={() =>
                openDialog({
                  Component: AvatarUploadDialog,
                  title: "Upload avatar",
                  props: {},
                })
              }
            >
              <FontAwesomeIcon icon={faCamera} />
            </Button>
          )}
        </div>
        <div className="flex-grow ml-4">
          <span className="text-3xl font-bold">
            {profile.user?.username ?? "Unknown user"}
          </span>
          <div className="flex flex-wrap justify-between items-center">
            <LabeledItem
              label="Member since"
              item={
                profile?.createdAt &&
                format(new Date(profile?.createdAt), "dd. MMMM yyyy")
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
            {!isCurrentUser && !follows && (
              <Button
                leftIcon={<FontAwesomeIcon icon={faUserPlus} />}
                onClick={() => follow(profile.user.id)}
              >
                Follow
              </Button>
            )}
            {!isCurrentUser && follows && (
              <Button
                leftIcon={<FontAwesomeIcon icon={faUserMinus} />}
                onClick={() => unfollow(profile.user.id)}
              >
                Unfollow
              </Button>
            )}
          </div>
        </div>
      </Paper>
      <Paper className="mt-4 p-4">
        <Tabs
          tabKey={tabKey}
          handleTabChange={(tabKey) =>
            tabKey === "wall"
              ? navigate(`/profile/${id}`)
              : navigate(`/profile/${id}/${tabKey}`)
          }
        >
          <Tab<TabKeys> tabKey="wall" label="Wall" />
          <Tab<TabKeys> tabKey="info" label="Information" />
          <Tab<TabKeys> tabKey="galleries" label="Gallery" />
          <Tab<TabKeys> tabKey="following" label="Following" />
        </Tabs>
      </Paper>

      <Outlet context={{ profile }} />
    </Container>
  );
};

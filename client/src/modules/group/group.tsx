import { faEdit, faTrash } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { axios } from "api/axios";
import { Button } from "components/button";
import { Container } from "components/container";
import { Paper } from "components/paper";
import { Tab, Tabs } from "components/tabs";
import { Outlet, useNavigate, useMatch } from "react-router-dom";
import { useStore } from "store";
import { Group as GroupType } from "models";
import { LabeledItem } from "components/labeled-item";

type TabKeys = "wall" | "members";

export const Group = () => {
  const { id } = useMatch("/groups/:id/*")?.params ?? {};
  const { tabKey = "wall" } = useMatch("/groups/:id/:tabKey/*")?.params ?? {};
  const navigate = useNavigate();
  const user = useStore((store) => store.user);
  const showNotification = useStore((store) => store.showNotification);
  const queryClient = useQueryClient();
  const { data: group } = useQuery(["groups", id], () =>
    axios.get<GroupType>(`/groups/${id}`).then((res) => res.data)
  );

  const { mutate: deleteGroup } = useMutation(
    () => axios.delete(`/groups/${group?.id}`),
    {
      onSuccess: () => {
        queryClient.invalidateQueries(["groups"]);
        showNotification({
          message: "Group deleted",
          type: "success",
        });
        navigate("/groups");
      },
      onError: () => {
        showNotification({
          message: "Failed to delete group",
          type: "error",
        });
      },
    }
  );

  const isCreator = group?.groupMembers.some(
    (member) => member.user.id === user?.id
  );

  if (!group) {
    return (
      <Container className="p-3">
        <Paper className="p-3">Group does not exist</Paper>
      </Container>
    );
  }

  return (
    <Container className="p-3">
      <Paper className="p-4 flex gap-4 items-center">
        <div className="flex-grow ml-4">
          <p className="text-3xl font-bold mb-2">
            {group.name ?? "Unnamed group"}
          </p>
          <div className="flex flex-wrap justify-between items-center">
            <LabeledItem label="Description" item={group.description} />
            {isCreator && (
              <Button
                variant="outlined"
                leftIcon={
                  <FontAwesomeIcon className="text-red-500" icon={faTrash} />
                }
                onClick={() => deleteGroup()}
              >
                Delete
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
              ? navigate(`/groups/${id}`)
              : navigate(`/groups/${id}/${tabKey}`)
          }
        >
          <Tab<TabKeys> tabKey="wall" label="Wall" />
          <Tab<TabKeys> tabKey="members" label="Members" />
        </Tabs>
      </Paper>

      <Outlet context={{ group }} />
    </Container>
  );
};

import { Paper } from "components/paper";
import { NavLink } from "react-router-dom";
import { useStore } from "store";
import { Container } from "components/container";
import { faAdd, faClose } from "@fortawesome/free-solid-svg-icons";
import { Button } from "components/button";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useQuery } from "@tanstack/react-query";
import { axios } from "api/axios";
import { Group } from "models";
import { AddGroupDialog } from "components/dialogs/add-group-dialog";

export const Groups = () => {
  const user = useStore((store) => store.user);
  const openDialog = useStore((store) => store.openDialog);
  const { data: groups } = useQuery(["users", user?.id, "groups"], () =>
    axios.get<Group[]>(`/users/${user?.id}/groups`).then((res) => res.data)
  );

  return (
    <Container className="p-3">
      <Paper className="p-4">
        <div className="flex justify-between gap-4">
          <p className="text-xl font-bold mb-4">Groups</p>
          <Button
            variant="outlined"
            leftIcon={<FontAwesomeIcon icon={faAdd} />}
            onClick={() =>
              openDialog({
                Component: AddGroupDialog,
                title: "Create group",
                props: {},
              })
            }
          >
            Create group
          </Button>
        </div>
        {groups?.length === 0 && <p>You are not member of any groups</p>}
        <div className="grid grid-cols-3 gap-2">
          {groups?.map((group) => (
            <Paper key={group.id} className="flex items-center gap-2 p-2">
              <NavLink to={`/groups/${group.id}`}>{group.name}</NavLink>
              <Button className="ml-auto" variant="clear">
                <FontAwesomeIcon className="text-red-500" icon={faClose} />
              </Button>
            </Paper>
          ))}
        </div>
      </Paper>
    </Container>
  );
};

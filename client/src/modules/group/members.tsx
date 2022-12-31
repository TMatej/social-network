import { Avatar } from "components/avatar";
import { Paper } from "components/paper";
import { Group } from "models";
import { NavLink, useOutletContext } from "react-router-dom";

export const Members = () => {
  const { group } = useOutletContext<{ group: Group }>();
  return (
    <Paper className="p-4 mt-4">
      <p className="text-xl font-bold mb-4">Members</p>
      {group.groupMembers?.length === 0 && <p>You do not follow anyone ...</p>}
      <div className="grid grid-cols-3 gap-2">
        {group.groupMembers?.map(({ user }) => (
          <Paper key={user.id} className="flex items-center gap-2 p-2">
            <Avatar user={user} />
            <NavLink to={`/profile/${user.id}`}>{user.username}</NavLink>
          </Paper>
        ))}
      </div>
    </Paper>
  );
};

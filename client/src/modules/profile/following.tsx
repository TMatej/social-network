import { useQuery } from "@tanstack/react-query";
import { axios } from "api/axios";
import { Paper } from "components/paper";
import { Profile, User } from "models";
import { NavLink, useOutletContext } from "react-router-dom";
import { Avatar } from "components/avatar";

export const ProfileFollowing = () => {
  const { profile } = useOutletContext<{ profile: Profile }>();
  const { data: following } = useQuery(
    ["contacts", profile.user.id],
    () => axios.get<User[]>(`/contacts/${profile.user.id}`),
    {
      suspense: false,
      select: (res) => res.data,
    }
  );

  return (
    <Paper className="p-4 mt-4">
      <p className="text-xl font-bold mb-4">Following</p>
      {following?.length === 0 && <p>No following</p>}
      <div className="grid grid-cols-4 gap-2">
        {following?.map((user) => (
          <Paper key={user.id} className="flex items-center gap-2 p-2">
            <Avatar user={user} />
            <NavLink to={`/profile/${user.id}`}>{user.username}</NavLink>
          </Paper>
        ))}
      </div>
    </Paper>
  );
};

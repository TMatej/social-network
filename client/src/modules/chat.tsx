// import { useQuery } from "@tanstack/react-query";
// import { axios } from "api/axios";
import clsx from "clsx";
import { Avatar } from "components/avatar";
import { Container } from "components/container";
import { Paper } from "components/paper";
import { NavLink, useParams } from "react-router-dom";
import { useStore } from "store";

export const Chat = () => {
  const following = useStore((store) => store.following);
  const { id } = useParams<{ id: string }>();
  // TODO
  // const { data: messages } = useQuery(["galleries"], () =>
  //   axios
  //     .get<Message[]>(`/messages/${profile.id}/galleries`)
  //     .then((res) => res.data)
  // );

  return (
    <Container className="p-4">
      <div className="h-full flex flex-col gap-4">
        <Paper className="p-4">
          <p className="text-xl font-bold">Chat</p>
        </Paper>
        <div className="h-full flex gap-4 items-stretch">
          <Paper className="p-3 overflow-y-auto">
            <p className="text-lg mb-3 pr-4 font-semibold">Direct messages</p>
            {following?.length === 0 && <p>No following</p>}
            {following?.map((user) => (
              <NavLink
                to={`/chat/${user.id}`}
                className={({ isActive }) =>
                  clsx(
                    "rounded text-white flex gap-2 p-2 hover:bg-slate-600 items-center",
                    {
                      "text-cyan-500": isActive,
                    }
                  )
                }
              >
                <Avatar user={user} />
                <p className="text-ellipsis">{user.username}</p>
              </NavLink>
            ))}
          </Paper>
          <Paper className="p-4 flex-grow"></Paper>
        </div>
      </div>
    </Container>
  );
};

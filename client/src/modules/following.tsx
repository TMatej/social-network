import { useMutation, useQueryClient } from "@tanstack/react-query";
import { axios } from "api/axios";
import { Paper } from "components/paper";
import { NavLink } from "react-router-dom";
import { Avatar } from "components/avatar";
import { useStore } from "store";
import { Container } from "components/container";
import { faClose, faCross } from "@fortawesome/free-solid-svg-icons";
import { Button } from "components/button";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useMemo } from "react";

export const useFollowActions = () => {
  const user = useStore((store) => store.user);
  const showNotification = useStore((store) => store.showNotification);
  const queryClient = useQueryClient();

  const { mutate: follow } = useMutation(
    (id: number) =>
      axios.put(`/contacts/${user?.id}/friends?targetUserId=${id}`),
    {
      onSuccess: () => {
        showNotification({
          message: "User succesfully followed",
          type: "success",
        });
        queryClient.invalidateQueries(["contacts", user?.id]);
      },
      onError: () => {
        showNotification({ message: "User follow failed", type: "error" });
      },
    }
  );

  const { mutate: unfollow } = useMutation(
    (id: number) =>
      axios.delete(`/contacts/${user?.id}/friends?targetUserId=${id}`),
    {
      onSuccess: () => {
        showNotification({
          message: "User succesfully unfollowed",
          type: "success",
        });
        queryClient.invalidateQueries(["contacts", user?.id]);
      },
      onError: () => {
        showNotification({ message: "User unfollow failed", type: "error" });
      },
    }
  );

  return useMemo(() => ({ follow, unfollow }), [follow, unfollow]);
};

export const Following = () => {
  const following = useStore((store) => store.following);
  const { unfollow } = useFollowActions();

  return (
    <Container className="p-3">
      <Paper className="p-4">
        <p className="text-xl font-bold mb-4">Following</p>
        {following?.length === 0 && <p>You do not follow anyone ...</p>}
        <div className="grid grid-cols-3 gap-2">
          {following?.map((user) => (
            <Paper key={user.id} className="flex items-center gap-2 p-2">
              <Avatar user={user} />
              <NavLink to={`/profile/${user.id}`}>{user.username}</NavLink>
              <Button
                className="ml-auto"
                variant="clear"
                onClick={() => unfollow(user.id)}
              >
                <FontAwesomeIcon className="text-red-500" icon={faClose} />
              </Button>
            </Paper>
          ))}
        </div>
      </Paper>
    </Container>
  );
};

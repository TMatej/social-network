import { useMemo } from "react";
import { useStore } from "store";

export const useIsFollowing = () => {
  const following = useStore((store) => store.following);

  const userIdToFollowing = useMemo(
    () =>
      following?.reduce<{ [key: number]: boolean }>(
        (acc, user) => ({ ...acc, [user.id]: true }),
        {}
      ) ?? {},
    [following]
  );

  const isFollowing = (userId?: number) =>
    userId ? userIdToFollowing[userId] ?? false : false;

  return isFollowing;
};

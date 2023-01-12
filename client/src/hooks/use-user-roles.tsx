import { useCallback, useMemo } from "react";
import { useStore } from "store";

export const useUserRoles = () => {
  const user = useStore((store) => store.user);

  const hasRoles = useCallback(
    (...roles: string[]) =>
      roles.every(
        (role) =>
          user?.roles.find((userRole) => userRole === role) !== undefined
      ),
    [user?.roles]
  );

  return useMemo(() => ({ hasRoles }), [hasRoles]);
};

import { faTrash } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { axios } from "api/axios";
import { Button } from "components/button";
import { Column, Table } from "components/table";
import { roles } from "constants/roles";
import { useUserRoles } from "hooks/use-user-roles";
import { Paginated, User } from "models";
import { useMemo, useState } from "react";
import { Navigate } from "react-router-dom";
import { useStore } from "store";

const useColumns = () => {
  const user = useStore((store) => store.user);
  const queryClient = useQueryClient();
  const showNotification = useStore((store) => store.showNotification);

  const { mutate: deleteUser } = useMutation(
    (id: number) => axios.delete(`/users/${id}`),
    {
      onSuccess: () => {
        queryClient.invalidateQueries(["users"]);
        showNotification({
          message: "User succesfully deleted",
          type: "success",
        });
      },
      onError: () => {
        showNotification({ message: "Failed to delete user", type: "error" });
      },
    }
  );

  const columns = useMemo<Column<User>[]>(
    () => [
      {
        key: "id",
        label: "Id",
        render: (data) => data.id,
      },
      {
        key: "username",
        label: "Username",
        render: (data) => data.username,
      },
      {
        key: "email",
        label: "Email",
        render: (data) => data.email,
      },
      {
        key: "roles",
        label: "Roles",
        render: (data) => data.roles.join(", "),
      },
      {
        key: "actions",
        label: "Actions",
        render: (data) =>
          data.id !== user?.id && (
            <Button onClick={() => deleteUser(data.id)} variant="outlined">
              <FontAwesomeIcon className="text-red-500" icon={faTrash} />
            </Button>
          ),
      },
    ],
    [user?.id]
  );

  return columns;
};

const size = 5;

export const Users = () => {
  const user = useStore((store) => store.user);
  const [page, setPage] = useState(1);
  const columns = useColumns();
  const { data: users } = useQuery({
    queryKey: ["users", page],
    queryFn: () =>
      axios
        .get<Paginated<User>>(`/users?page=${page}&size=${size}`)
        .then((res) => res.data),
    suspense: false,
  });
  const { hasRoles } = useUserRoles();
  const isAdmin = hasRoles(roles.admin);

  if (!isAdmin) {
    return <Navigate to={`/profiles/${user?.id}`} />;
  }

  return (
    <Table
      title={<span className="text-xl font-bold">Users</span>}
      columns={columns}
      data={users?.items ?? []}
      pagination={{
        page,
        size,
        total: (users?.items?.length ?? 0) >= size ? page * size + 1 : 0,
        onChange: (index) => setPage(index),
      }}
    />
  );
};

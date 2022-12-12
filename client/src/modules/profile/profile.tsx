import { useQuery } from "@tanstack/react-query";
import { axios } from "api/axios";
import { Container } from "components/container";
import { Paper } from "components/paper";
import { useParams } from "react-router-dom";

export const Profile = () => {
  const { id } = useParams<{ id: string }>();
  const { data } = useQuery(["profile", id], () =>
    axios.get(`/users/${id}/profile`)
  );
  console.log({ id, data });

  return (
    <Container className="py-3">
      <Paper className="p-4">
        <div className="border-2 border-white border-opacity-5 bg-cyan-900 w-44 h-44 rounded-full overflow-hidden">
          <img
            className="w-full h-full object-contain"
            src="https://picsum.photos/200"
            alt=""
          />
        </div>
      </Paper>
    </Container>
  );
};

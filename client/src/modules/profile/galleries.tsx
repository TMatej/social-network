import { faPlusCircle } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Button } from "components/button";
import { Paper } from "components/paper";
import { Gallery, Profile } from "models";
import { NavLink, useOutletContext } from "react-router-dom";
import { useStore } from "store";

export const Galleries = () => {
  const { profile } = useOutletContext<{ profile: Profile }>();
  const user = useStore((state) => state.user);
  // const { data: galleries, error } = useQuery(["galleries"], () =>
  //   axios.get<Gallery[]>("/galleries").then((res) => res.data)
  // );
  const isCurrentUser = user?.id === profile?.user?.id;

  const galleriesMock: Gallery[] = [{ id: "1" }, { id: "2" }];

  return (
    <Paper className="mt-4 p-4">
      <div className="flex justify-between items-center">
        <span className="text-xl font-bold">Galleries</span>
        {isCurrentUser && (
          <Button leftIcon={<FontAwesomeIcon icon={faPlusCircle} />}>
            Add gallery
          </Button>
        )}
      </div>
      <div className="grid grid-cols-4 gap-4 mt-4">
        {galleriesMock?.map((gallery) => (
          <NavLink
            to={`./${gallery.id}`}
            key={gallery.id}
            className="w-full border-2 border-white border-opacity-5 rounded overflow-hidden"
          >
            <Paper className="p-2">
              <p className="mb-2 text-white text-lg text-center">
                gallery name
              </p>
              <img
                className="w-full h-full object-contain"
                src="https://picsum.photos/200"
                alt=""
              />
            </Paper>
          </NavLink>
        ))}
      </div>
    </Paper>
  );
};

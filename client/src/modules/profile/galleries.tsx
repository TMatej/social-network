import { faPlusCircle } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import { Button } from "components/button";
import { AddGalleryDialog } from "components/dialogs/add-gallery-dialog";
import { Paper } from "components/paper";
import { Gallery, Profile } from "models";
import { NavLink, useOutletContext } from "react-router-dom";
import { useStore } from "store";

export const Galleries = () => {
  const { profile } = useOutletContext<{ profile: Profile }>();
  const openDialog = useStore((state) => state.openDialog);
  const user = useStore((state) => state.user);
  const { data: galleries } = useQuery(
    ["profiles", profile.id, "galleries"],
    () =>
      axios
        .get<Gallery[]>(`/profiles/${profile.id}/galleries`)
        .then((res) => res.data),
    {
      suspense: false,
    }
  );
  const isCurrentUser = user?.id === profile?.user?.id;

  return (
    <Paper className="mt-4 p-4">
      <div className="flex justify-between items-center">
        <span className="text-xl font-bold">Galleries</span>
        {isCurrentUser && (
          <Button
            leftIcon={<FontAwesomeIcon icon={faPlusCircle} />}
            onClick={() =>
              openDialog({
                Component: AddGalleryDialog,
                props: { profile },
                title: "Add gallery",
              })
            }
          >
            Add gallery
          </Button>
        )}
      </div>
      <div className="grid grid-cols-4 gap-4 mt-4">
        {galleries?.length === 0 && "No galleries"}
        {galleries?.map((gallery) => (
          <NavLink
            to={`./${gallery.id}`}
            key={gallery.id}
            className="w-full border-white border-opacity-5 rounded overflow-hidden"
          >
            <Paper className="p-2 h-full flex flex-col">
              <p className="pb-2 text-white text-lg text-center border-b border-b-slate-600">
                {gallery.title}
              </p>
              <p className="p-2 text-slate-300 text-center flex-grow flex items-center justify-center">
                {gallery.description || "No description"}
              </p>
            </Paper>
          </NavLink>
        ))}
      </div>
    </Paper>
  );
};

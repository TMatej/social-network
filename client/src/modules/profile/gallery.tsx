import {
  faAdd,
  faArrowLeft,
  faEye,
  faTrash,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { Button } from "components/button";
import { Paper } from "components/paper";
import { Profile, Gallery as GalleryType } from "models";
import { useNavigate, useOutletContext, useParams } from "react-router-dom";
import { useStore } from "store";
import { axios } from "api/axios";
import { AddPhotoDialog } from "components/dialogs/add-photo-dialog";
import { baseUrl } from "api";
import { useMemo } from "react";
import { PhotoDialog } from "components/dialogs/photo-dialog";

export const Gallery = () => {
  const queryClient = useQueryClient();
  const { profile } = useOutletContext<{ profile: Profile }>();
  const { galleryId } = useParams<{ galleryId: string }>();
  const openDialog = useStore((state) => state.openDialog);
  const showNotification = useStore((state) => state.showNotification);
  const user = useStore((state) => state.user);
  const navigate = useNavigate();

  const { data: galleries } = useQuery(
    ["profiles", profile.id, "galleries"],
    () =>
      axios
        .get<GalleryType[]>(`/profiles/${profile.id}/galleries`)
        .then((res) => res.data)
  );

  const isCurrentUser = user?.id === profile?.user?.id;
  const gallery = useMemo(
    () => galleries?.find((gallery) => gallery.id === Number(galleryId)),
    [galleries, galleryId]
  );

  const { mutate: deletePhoto } = useMutation(
    (photoId: number) =>
      axios.delete(`/galleries/${gallery!.id}/photos/${photoId}`),
    {
      onSuccess: () => {
        queryClient.invalidateQueries(["profiles", profile.id, "galleries"]);
        showNotification({
          message: "Photo deleted",
          type: "success",
        });
      },
      onError: () => {
        showNotification({
          message: "Failed to delete photo",
          type: "error",
        });
      },
    }
  );

  return (
    <Paper className="mt-4 p-4">
      <div className="flex justify-between items-center">
        <span className="text-xl font-bold">{gallery?.title}</span>
        <div className="flex items-center gap-2">
          <Button onClick={() => navigate("../galleries")}>
            <FontAwesomeIcon icon={faArrowLeft} />
          </Button>
          {isCurrentUser && (
            <Button
              leftIcon={<FontAwesomeIcon icon={faAdd} />}
              onClick={() =>
                openDialog({
                  Component: AddPhotoDialog,
                  props: { gallery: gallery!, profile },
                  title: "Add photo",
                })
              }
            >
              add photo
            </Button>
          )}
        </div>
      </div>
      <div className="grid grid-cols-4 gap-4 mt-4">
        {gallery?.photos?.length === 0 && "No photos"}

        {gallery?.photos?.map((photo, index) => (
          <Paper key={index} className="p-2 relative">
            <img
              className="w-full h-full min-h-[250px] object-contain"
              src={`${baseUrl}/files/${photo?.fileEntity?.guid}`}
              alt="gallery photo"
            />
            <div className="absolute right-2 bottom-2 flex items-center gap-2">
              <Button
                variant="outlined"
                onClick={() =>
                  openDialog({
                    Component: PhotoDialog,
                    title: photo.title,
                    props: { guid: photo.fileEntity.guid },
                  })
                }
              >
                <FontAwesomeIcon icon={faEye} />
              </Button>
              {isCurrentUser && (
                <Button
                  variant="outlined"
                  onClick={() => deletePhoto(photo.id)}
                >
                  <FontAwesomeIcon className="text-red-500" icon={faTrash} />
                </Button>
              )}
            </div>
          </Paper>
        ))}
      </div>
    </Paper>
  );
};

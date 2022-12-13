import { faAdd, faArrowLeft, faTrash } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Button } from "components/button";
import { Paper } from "components/paper";
import { Profile, Gallery as GalleryType } from "models";
import { useNavigate, useOutletContext, useParams } from "react-router-dom";
import { useStore } from "store";

export const Gallery = () => {
  const { profile } = useOutletContext<{ profile: Profile }>();
  const { galleryId } = useParams<{ galleryId: string }>();
  const user = useStore((state) => state.user);
  const navigate = useNavigate();
  // const { data: galleries, error } = useQuery(["galleries"], () =>
  //   axios.get<Gallery[]>("/galleries").then((res) => res.data)
  // );
  const isCurrentUser = user?.id === profile?.user?.id;

  const galleriesMock: GalleryType[] = [
    { id: "1", photos: [{ url: "test" }] },
    { id: "2", photos: [] },
  ];
  const gallery = galleriesMock.find((g) => g.id === galleryId);

  return (
    <Paper className="mt-4 p-4">
      <div className="flex justify-between items-center">
        <span className="text-xl font-bold">Gallery</span>
        {isCurrentUser && (
          <Button leftIcon={<FontAwesomeIcon icon={faAdd} />}>add photo</Button>
        )}
        <Button onClick={() => navigate("../galleries")}>
          <FontAwesomeIcon icon={faArrowLeft} />
        </Button>
      </div>
      <div className="grid grid-cols-4 gap-4 mt-4">
        {gallery?.photos?.length === 0 && "No photos"}

        {gallery?.photos?.map((photo, index) => (
          <Paper key={index} className="p-2 relative">
            <img
              className="w-full h-full min-h-[250px] object-contain"
              src={photo.url}
              alt=""
            />
            {isCurrentUser && (
              <Button className="!bg-red-500 absolute right-2 bottom-2">
                <FontAwesomeIcon icon={faTrash} />
              </Button>
            )}
          </Paper>
        ))}
      </div>
    </Paper>
  );
};

import { baseUrl } from "api";
import { DialogProps } from "components/dialog";

export const PhotoDialog = ({ guid }: DialogProps<{ guid: string }>) => {
  return (
    <img
      className="rounded bg-slate-900 flex-grow min-h-[250px] object-contain"
      src={`${baseUrl}/files/${guid}`}
      alt="gallery photo"
    />
  );
};

import { faUpload } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { axios } from "api/axios";
import { Button } from "components/button";
import { DialogProps } from "components/dialog";
import { FormFileField } from "components/input/file-field";
import { FormTextField } from "components/input/text-field";
import { Form, Formik } from "formik";
import { Gallery, Profile } from "models";
import { useStore } from "store";
import * as yup from "yup";

const schema = yup
  .object()
  .shape({
    file: yup.mixed().required(),
    title: yup.string().min(3).required(),
    description: yup.string().min(3).required(),
  });

type AddPhotoData = {
  title: string;
  description: string;
  file?: File;
};

export const AddPhotoDialog = ({
  closeDialog,
  gallery,
  profile,
}: DialogProps<{ gallery: Gallery; profile: Profile }>) => {
  const showNotification = useStore((store) => store.showNotification);
  const queryClient = useQueryClient();

  const { mutate, isLoading } = useMutation(
    ({ file, title, description }: AddPhotoData) => {
      if (!file) return Promise.reject();
      const formData = new FormData();
      formData.append("file", file, file.name);
      formData.append("title", title);
      formData.append("description", description);
      return axios.post(`/galleries/${gallery.id}/photos`, formData);
    },
    {
      onSuccess: () => {
        queryClient.invalidateQueries(["profiles", profile.id, "galleries"]);
        showNotification({
          message: "Photo uploaded successfully",
          type: "success",
        });
        closeDialog();
      },
      onError: () => {
        showNotification({
          message: "Failed to upload photo",
          type: "error",
        });
      },
    }
  );

  return (
    <Formik<AddPhotoData>
      validationSchema={schema}
      initialValues={{ file: undefined, title: "", description: "" }}
      onSubmit={(data) => mutate(data)}
    >
      <Form>
        <FormTextField name="title" label="Title" />
        <FormTextField name="description" label="description" />
        <FormFileField className="mt-2" name="file" accept=".jpg,.png,.jpeg" />
        <Button disabled={isLoading} type="submit" className="w-full mt-4">
          <FontAwesomeIcon icon={faUpload} /> Upload
        </Button>
      </Form>
    </Formik>
  );
};

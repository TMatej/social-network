import { useMutation, useQueryClient } from "@tanstack/react-query";
import { DialogProps } from "components/dialog";
import { axios } from "api/axios";
import { Profile } from "models";
import { Form, Formik } from "formik";
import { FormTextField } from "components/input/text-field";
import { Button } from "components/button";
import { useStore } from "store";

type AddGalleryDialogProps = {
  profile: Profile;
};

type AddGalleryData = {
  title: string;
  description: string;
};

export const AddGalleryDialog = ({
  closeDialog,
  profile,
}: DialogProps<AddGalleryDialogProps>) => {
  const queryClient = useQueryClient();
  const showNotification = useStore((state) => state.showNotification);
  const { mutate } = useMutation(
    (data: AddGalleryData) =>
      axios.post(`/profiles/${profile.id}/galleries`, data),
    {
      onSuccess: () => {
        showNotification({
          message: "Gallery added",
          type: "success",
        });
        closeDialog();
        queryClient.invalidateQueries(["profiles", profile.id, "galleries"]);
      },
      onError: () => {
        showNotification({
          message: "Failed to add gallery",
          type: "error",
        });
      },
    }
  );

  return (
    <Formik<AddGalleryData>
      initialValues={{ title: "", description: "" }}
      onSubmit={(data) => mutate(data)}
    >
      <Form>
        <FormTextField name="title" label="Title" />
        <FormTextField name="description" label="Description" />
        <Button type="submit" className="w-full mt-4">
          Add Gallery
        </Button>
      </Form>
    </Formik>
  );
};

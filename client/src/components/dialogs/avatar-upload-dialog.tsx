import { faUpload } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useMutation } from "@tanstack/react-query";
import { axios } from "api/axios";
import { Button } from "components/button";
import { DialogProps } from "components/dialog";
import { FormFileField } from "components/input/file-field";
import { Form, Formik } from "formik";
import { useStore } from "store";

type AvatarUploadData = {
  avatar?: File;
};

export const AvatarUploadDialog = ({ closeDialog }: DialogProps) => {
  const { user } = useStore((store) => store);
  const showNotification = useStore((store) => store.showNotification);

  const { mutate, isLoading } = useMutation(
    ({ avatar }: AvatarUploadData) => {
      if (!avatar) return Promise.reject();
      const formData = new FormData();
      formData.append("avatar", avatar, avatar.name);
      return axios.put(`/users/${user?.id}/avatar`, formData);
    },
    {
      onSuccess: () => {
        showNotification({
          message: "Avatar uploaded successfully",
          type: "success",
        });
        closeDialog();
      },
      onError: () => {
        showNotification({
          message: "Failed to upload avatar",
          type: "error",
        });
      },
    }
  );

  return (
    <Formik<AvatarUploadData>
      initialValues={{ avatar: undefined }}
      onSubmit={(data) => mutate(data)}
    >
      <Form>
        <FormFileField name="avatar" accept=".jpg,.png,.jpeg" />
        <Button disabled={isLoading} type="submit" className="w-full mt-4">
          <FontAwesomeIcon icon={faUpload} /> Upload
        </Button>
      </Form>
    </Formik>
  );
};

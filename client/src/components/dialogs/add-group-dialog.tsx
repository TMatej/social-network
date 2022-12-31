import { useMutation, useQueryClient } from "@tanstack/react-query";
import { DialogProps } from "components/dialog";
import { axios } from "api/axios";
import { Form, Formik } from "formik";
import { FormTextField } from "components/input/text-field";
import { Button } from "components/button";
import { useStore } from "store";
import * as yup from "yup";

const schema = yup.object().shape({
  name: yup.string().min(3).required(),
  description: yup.string().min(3).required(),
});

type AddGroupData = {
  name: string;
  description: string;
};

export const AddGroupDialog = ({ closeDialog }: DialogProps) => {
  const queryClient = useQueryClient();
  const showNotification = useStore((state) => state.showNotification);
  const user = useStore((store) => store.user);
  const { mutate } = useMutation(
    (data: AddGroupData) => axios.post(`/groups`, data),
    {
      onSuccess: () => {
        showNotification({
          message: "Group added",
          type: "success",
        });
        closeDialog();
        queryClient.invalidateQueries(["users", user?.id, "groups"]);
      },
      onError: () => {
        showNotification({
          message: "Failed to add group",
          type: "error",
        });
      },
    }
  );

  return (
    <Formik<AddGroupData>
      initialValues={{ name: "", description: "" }}
      validationSchema={schema}
      onSubmit={(data) => mutate(data)}
    >
      <Form>
        <FormTextField name="name" label="Name" />
        <FormTextField name="description" label="Description" />
        <Button type="submit" className="w-full mt-4">
          Add Group
        </Button>
      </Form>
    </Formik>
  );
};

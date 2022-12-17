import { faPaperPlane } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import axios from "axios";
import { Button } from "components/button";
import { DialogProps } from "components/dialog";
import { FormTextField } from "components/input/text-field";
import { Form, Formik } from "formik";
import { Profile } from "models";
import { useStore } from "store";

type Props = {
  profile: Profile;
};

export const ProfileEditDialog = ({
  profile,
  closeDialog,
}: DialogProps<Props>) => {
  const user = useStore((store) => store.user);
  const showNotification = useStore((store) => store.showNotification);
  const queryClient = useQueryClient();
  const { mutate: updateProfile } = useMutation(
    (data: Profile) => {
      const { id, user: _user, createdAt, ...body } = data;
      return axios.patch(`/users/${user?.id}/profile`, body);
    },
    {
      onSuccess: () => {
        showNotification({
          message: "profile updated",
          type: "success",
        });
        queryClient.invalidateQueries(["profile"]);
        closeDialog();
      },
      onError: () => {
        showNotification({
          message: "failed to update profile",
          type: "error",
        });
      },
    }
  );

  return (
    <Formik<Profile>
      initialValues={{ ...profile }}
      onSubmit={(data) => updateProfile(data)}
    >
      <Form>
        <div className="grid grid-cols-2 gap-y-2 gap-x-4">
          <FormTextField name="name" label="Name" />
          <FormTextField name="dateOfBirth" label="Date of birth" />
          <FormTextField name="phoneNumber" label="Phone number" />
          <FormTextField name="sex" label="Sex" />
          <FormTextField name="address.city" label="City" />
          <FormTextField name="address.state" label="State" />
          <FormTextField name="address.street" label="Street" />
          <FormTextField name="address.region" label="Region" />
          <FormTextField name="address.postalCode" label="Postal code" />
          <FormTextField name="address.houseNumber" label="House number" />
        </div>
        <Button
          type="submit"
          className="mt-4 ml-auto"
          leftIcon={<FontAwesomeIcon icon={faPaperPlane} />}
        >
          Submit
        </Button>
      </Form>
    </Formik>
  );
};

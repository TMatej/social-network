import { LabeledItem } from "components/labeled-item";
import { Paper } from "components/paper";
import { Profile } from "models";
import { useOutletContext } from "react-router-dom";

export const Info = () => {
  const { profile } = useOutletContext<{ profile: Profile }>();

  return (
    <Paper className="mt-4 p-4 grid grid-cols-2 gap-x-4">
      <LabeledItem label="Name" item={profile?.name} />
      <LabeledItem label="Phone number" item={profile?.phoneNumber} />
      <LabeledItem label="Date of birth" item={profile?.dateOfBirth} />
      <LabeledItem label="Sex" item={profile?.sex} />
      <LabeledItem label="City" item={profile?.address?.city} />
      <LabeledItem label="State" item={profile?.address?.state} />
      <LabeledItem label="Region" item={profile?.address?.region} />
      <LabeledItem label="Street" item={profile?.address?.street} />
    </Paper>
  );
};

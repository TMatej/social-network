import { ReactNode } from "react";

export const LabeledItem = ({
  label,
  item,
  className,
}: {
  label: string;
  item: ReactNode;
  className?: string;
}) => {
  return (
    <div className={className}>
      <span className="font-bold text-gray-300">{label}:</span>{" "}
      <span>{item ?? "-"}</span>
    </div>
  );
};

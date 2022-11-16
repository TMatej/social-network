import clsx from "clsx";
import { ReactNode } from "react";

export const Paper = ({
  children,
  className,
}: {
  children: ReactNode;
  className?: string;
}) => {
  return (
    <div className={clsx("bg-white bg-opacity-5 rounded p-4", className)}>
      {children}
    </div>
  );
};

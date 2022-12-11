import { ReactNode } from "react";
import clsx from "clsx";

export const Container = ({
  children,
  className,
}: {
  children: ReactNode;
  className?: string;
}) => {
  return (
    <div className={clsx("flex justify-center h-full", className)}>
      <div className="max-w-6xl w-full">{children}</div>
    </div>
  );
};

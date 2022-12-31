import clsx from "clsx";
import { ReactNode } from "react";

export const Paper = ({
  children,
  className,
  onClick,
}: {
  children?: ReactNode;
  className?: string;
  onClick?: () => void;
}) => {
  return (
    <div
      onClick={onClick}
      className={clsx("bg-white bg-opacity-5 rounded", className, {
        "cursor-pointer": !!onClick,
      })}
    >
      {children}
    </div>
  );
};

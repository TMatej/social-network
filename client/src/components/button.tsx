import { ReactNode } from "react";
import clsx from "clsx";

export const Button = ({
  onClick,
  leftIcon,
  rightIcon,
  disabled,
  children,
  className,
  type,
}: {
  children?: ReactNode;
  leftIcon?: ReactNode;
  rightIcon?: ReactNode;
  onClick?: () => void;
  className?: string;
  disabled?: boolean;
  type?: "submit";
}) => {
  return (
    <button
      onClick={onClick}
      disabled={disabled}
      type={type}
      className={clsx(
        "w-full p-1 flex justify-center items-center rounded bg-cyan-600 hover:bg-cyan-700 disabled:bg-cyan-500",
        className
      )}
    >
      {leftIcon}
      <span
        className={clsx("font-bold", {
          "ml-2": leftIcon,
          "mr-2": rightIcon,
        })}
      >
        {children}
      </span>
      {rightIcon}
    </button>
  );
};

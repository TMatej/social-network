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
  variant = "block",
}: {
  children?: ReactNode;
  leftIcon?: ReactNode;
  rightIcon?: ReactNode;
  onClick?: () => void;
  className?: string;
  disabled?: boolean;
  type?: "submit";
  variant?: "block" | "clear";
}) => {
  return (
    <button
      onClick={onClick}
      disabled={disabled}
      type={type}
      className={clsx(
        "flex justify-center items-center",
        {
          "py-1 px-2 bg-cyan-600 disabled:bg-cyan-900 hover:brightness-90":
            variant === "block",
          "outline-none text-gray-300 py-1": variant === "clear",
        },
        className
      )}
    >
      {leftIcon}
      <span
        className={clsx({
          "ml-2": leftIcon,
          "mr-2": rightIcon,
          "font-semibold text-cyan-600 hover:text-cyan-700":
            variant === "clear",
          "font-bold": variant === "block",
        })}
      >
        {children}
      </span>
      {rightIcon}
    </button>
  );
};

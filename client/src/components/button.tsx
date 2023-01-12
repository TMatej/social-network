import { ReactNode, useState } from "react";
import clsx from "clsx";
import { animated, useSpring } from "react-spring";

export const Button = ({
  onClick,
  leftIcon,
  rightIcon,
  disabled,
  children,
  className,
  type,
  variant = "block",
  animate = true,
}: {
  children?: ReactNode;
  leftIcon?: ReactNode;
  rightIcon?: ReactNode;
  onClick?: () => void;
  className?: string;
  disabled?: boolean;
  type?: "submit";
  variant?: "block" | "clear" | "outlined";
  animate?: boolean;
}) => {
  const [hovered, setHovered] = useState(false);
  const style = useSpring({
    scaleX: hovered ? 1.025 : 1,
    cancel: !animate,
  });

  return (
    <animated.button
      style={style}
      onMouseEnter={() => setHovered(true)}
      onMouseLeave={() => setHovered(false)}
      onClick={onClick}
      disabled={disabled}
      type={type}
      className={clsx(
        "flex justify-center items-center rounded transition-colors",
        {
          "py-1 px-2 bg-cyan-600 disabled:bg-cyan-900 enabled:hover:brightness-90":
            variant === "block",
          "outline-none text-gray-300 px-2": variant === "clear",
          "border py-1 px-2 border-slate-600 enabled:hover:bg-slate-600 disabled:border-slate-700 disabled:text-slate-500":
            variant === "outlined",
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
    </animated.button>
  );
};

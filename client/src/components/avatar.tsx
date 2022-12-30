import { User } from "models";
import clsx from "clsx";
import { useNavigate } from "react-router-dom";
import { Tooltip, useTooltip } from "./tooltip";
import { baseUrl } from "api";

export const Avatar = ({
  user,
  size = "md",
  onClick,
  withoutTooltip = false,
}: {
  user?: Pick<User, "id" | "username" | "avatar">;
  size?: "md" | "lg";
  onClick?: () => void;
  withoutTooltip?: boolean;
}) => {
  const navigate = useNavigate();
  const { bind, tooltipProps } = useTooltip();

  return (
    <>
      <button
        {...bind()}
        className={clsx(
          "border-2 flex-shrink-0 border-white border-opacity-5 bg-cyan-900 flex items-center justify-center rounded-full overflow-hidden outline-none",
          {
            "w-10 h-10": size === "md",
            "w-20 h-20": size === "lg",
          }
        )}
        onClick={() => (onClick ? onClick() : navigate(`/profile/${user?.id}`))}
      >
        {user?.avatar ? (
          <img
            className="w-full h-full object-contain"
            src={`${baseUrl}/files/${user.avatar.guid}`}
            alt="user avatar"
          />
        ) : (
          <span
            className={clsx(
              "font-extrabold cursor-default pointer-events-none",
              {
                "text-xl": size === "md",
                "text-3xl": size === "lg",
              }
            )}
          >
            {user?.username.charAt(0).toUpperCase() ?? "X"}
          </span>
        )}
      </button>

      {!withoutTooltip && (
        <Tooltip {...tooltipProps}>
          <span className="font-bold">{user?.username}</span>
        </Tooltip>
      )}
    </>
  );
};

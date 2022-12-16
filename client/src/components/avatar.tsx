import { User } from "models";
import clsx from "clsx";
import { useNavigate } from "react-router-dom";
import { Tooltip, useTooltip } from "./tooltip";

export const Avatar = ({ user }: { user?: User }) => {
  const navigate = useNavigate();
  const { bind, tooltipProps } = useTooltip();

  return (
    <>
      <button
        {...bind()}
        className={clsx(
          "border-2 flex-shrink-0 border-white border-opacity-5 bg-cyan-900 w-10 h-10 flex items-center justify-center rounded-full overflow-hidden outline-none"
        )}
        onClick={() => navigate(`/profile/${user?.id}/info`)}
      >
        {user?.avatar ? (
          <img
            className="w-full h-full object-contain"
            src={user.avatar.data}
            alt="user avatar"
          />
        ) : (
          <span className="font-extrabold text-xl cursor-default pointer-events-none">
            {user?.username.charAt(0).toUpperCase() ?? "X"}
          </span>
        )}
      </button>

      <Tooltip {...tooltipProps}>
        <span className="font-bold">{user?.username}</span>
      </Tooltip>
    </>
  );
};

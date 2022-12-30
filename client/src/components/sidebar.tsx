import {
  faIdCard,
  faMessage,
  faPeopleGroup,
  faUserFriends,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import clsx from "clsx";
import { ReactNode } from "react";
import { NavLink } from "react-router-dom";
import { useStore } from "store";
import { Paper } from "./paper";

export const Sidebar = () => {
  const user = useStore((store) => store.user);

  return (
    <Paper className="sticky h-[calc(100vh-theme(spacing.16))] top-16 border-t !rounded-none border-t-slate-900 p-4 min-w-[300px]">
      <NavItem
        to={`/profile/${user?.id}`}
        icon={<FontAwesomeIcon icon={faIdCard} />}
        label="Profile"
      />
      <NavItem
        to={`/following`}
        icon={<FontAwesomeIcon icon={faUserFriends} />}
        label="Following"
      />
      <NavItem
        to={`/chat`}
        icon={<FontAwesomeIcon icon={faMessage} />}
        label="Chat"
      />
      <NavItem
        to={`/groups`}
        icon={<FontAwesomeIcon icon={faPeopleGroup} />}
        label="Groups"
      />
    </Paper>
  );
};

const NavItem = ({
  icon,
  to,
  label,
}: {
  icon?: ReactNode;
  to: string;
  label: string;
}) => {
  return (
    <NavLink
      to={to}
      className={({ isActive }) =>
        clsx(
          "rounded text-white flex gap-4 p-4 hover:bg-slate-600 items-center",
          {
            "text-cyan-500": isActive,
          }
        )
      }
    >
      {icon}
      {label}
    </NavLink>
  );
};

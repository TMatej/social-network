import {
  faCheckCircle,
  faExclamationCircle,
  faWarning,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { ReactNode } from "react";
import { createPortal } from "react-dom";
import { animated, useTransition } from "react-spring";
import clsx from "clsx";
import { NotificationType, useStore } from "store";

export const Notifications = ({ children }: { children: ReactNode }) => {
  const notifications = useStore((state) => state.notifications);

  const transitions = useTransition(notifications, {
    from: { opacity: 0, x: "-150%" },
    enter: { opacity: 1, x: "0%" },
    leave: { opacity: 0, x: "-150%" },
  });

  const notificationContent = (
    <div className="fixed left-3 bottom-3 flex flex-col gap-2">
      {transitions(
        (style, { id, message, type }) =>
          id && (
            <animated.div style={style}>
              <Notification message={message} type={type} />
            </animated.div>
          )
      )}
    </div>
  );

  return (
    <>
      {children}
      {createPortal(notificationContent, document.body)}
    </>
  );
};

const Notification = ({ message, type }: Omit<NotificationType, "id">) => {
  const icon =
    type === "success"
      ? faCheckCircle
      : type === "warning"
      ? faWarning
      : faExclamationCircle;

  return (
    <div
      className={clsx(
        "p-3 w-56 bg-opacity-50 flex items-center gap-3 rounded",
        {
          "bg-green-500": type === "success",
          "bg-yellow-500": type === "warning",
          "bg-red-500": type === "error",
        }
      )}
    >
      <div
        className={clsx({
          "text-green-300": type === "success",
          "text-yellow-300": type === "warning",
          "text-red-300": type === "error",
        })}
      >
        <FontAwesomeIcon icon={icon} />
      </div>
      <span>{message}</span>
    </div>
  );
};

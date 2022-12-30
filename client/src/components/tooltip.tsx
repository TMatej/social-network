import clsx from "clsx";
import { ReactNode, useCallback, useMemo, useRef, useState } from "react";
import { createPortal } from "react-dom";
import { usePopper } from "react-popper";
import { animated, useTransition } from "react-spring";
import classes from "./tooltip.module.css";

export const useTooltip = (props?: { toggle?: "hover" | "static" }) => {
  const { toggle = "hover" } = props ?? {};
  const [target, setTarget] = useState<HTMLElement | null>(null);
  const [open, setOpen] = useState(toggle === "static" ? true : false);
  const timeoutRef = useRef<ReturnType<typeof setTimeout>>();

  const bind = useCallback(
    () => ({
      ref: setTarget,
      ...(toggle === "hover" && {
        onMouseEnter: () => {
          clearTimeout(timeoutRef.current);
          setOpen(true);
        },
        onMouseLeave: () => {
          const timeout = setTimeout(() => setOpen(false), 250);
          timeoutRef.current = timeout;
        },
      }),
    }),
    [toggle]
  );

  const tooltipProps = useMemo(() => ({ target, open }), [target, open]);

  return {
    bind,
    tooltipProps,
  };
};

export const Tooltip = ({
  target,
  children,
  open = true,
}: {
  target: HTMLElement | null;
  children: ReactNode;
  open?: boolean;
}) => {
  const [hovering, setHovering] = useState(false);
  const [container, setContainer] = useState<HTMLDivElement | null>(null);
  const [arrow, setArrow] = useState<HTMLDivElement | null>(null);
  const { styles, attributes } = usePopper(target, container, {
    modifiers: [
      { name: "arrow", options: { element: arrow } },
      { name: "offset", options: { offset: [0, 5] } },
    ],
  });

  const transition = useTransition(open || hovering, {
    from: { opacity: 0, y: -5 },
    enter: { opacity: 1, y: 0 },
    leave: { opacity: 0, y: -5 },
  });

  return createPortal(
    transition(
      (style, item) =>
        item && (
          <animated.div
            onMouseEnter={() => setHovering(true)}
            onMouseLeave={() => setHovering(false)}
            className={clsx(
              classes.tooltip,
              "z-40 bg-slate-800 p-2 rounded border border-slate-600"
            )}
            ref={setContainer}
            style={{ ...style, ...styles.popper }}
            {...attributes.popper}
          >
            {children}
            <div
              ref={setArrow}
              style={styles.arrow}
              className={clsx(
                classes.arrow,
                "absolute w-2 h-2 bg-inherit invisble"
              )}
            >
              <div
                className={clsx(
                  classes.fill,
                  "absolute w-full h-full bg-inherit border border-slate-600 visible rotate-45"
                )}
              />
            </div>
          </animated.div>
        )
    ),
    document.body
  );
};

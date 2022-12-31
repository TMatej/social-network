import { faClose } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { FunctionComponent, useRef } from "react";
import { createPortal } from "react-dom";
import { useStore } from "store";
import { Button } from "./button";
import { useOnClickOutside } from "hooks/use-on-click-outside";
import { animated, useTransition } from "react-spring";

export type DialogProps<TProps extends {} = {}> = TProps & {
  closeDialog: () => void;
};

export type Dialog<TProps extends {}> = {
  id: string;
  title: string;
  props: TProps;
  Component: FunctionComponent<DialogProps<TProps>>;
};

export const Dialog = () => {
  const dialogs = useStore((state) => state.dialogs);
  const closeDialog = useStore((state) => state.closeDialog);
  const ref = useRef<HTMLDivElement>(null);

  const dialog = dialogs[dialogs.length - 1];
  const closeCurrentDialog = () => {
    if (!dialog) return;
    closeDialog(dialog.id);
  };

  useOnClickOutside(ref, closeCurrentDialog);

  const transition = useTransition(dialog, {
    from: { y: -40, opacity: 0 },
    enter: { y: 0, opacity: 1 },
    leave: { y: 40, opacity: 0 },
  });

  return transition(({ y, opacity }, dialog) =>
    createPortal(
      dialog && (
        <animated.div
          style={{ opacity }}
          className="z-30 fixed left-0 top-0 w-full h-full bg-black bg-opacity-40 p-4 sm:p-12 flex items-center justify-center"
        >
          <animated.div
            style={{ y }}
            ref={ref}
            className="bg-slate-800 rounded p-4 min-w-full max-h-full sm:min-w-[400px] flex flex-col"
          >
            <div className="flex justify-between items-center gap-8 pb-4">
              <span className="text-xl font-bold">{dialog.title}</span>
              <Button variant="clear" onClick={closeCurrentDialog}>
                <FontAwesomeIcon size="lg" icon={faClose} />
              </Button>
            </div>
            <dialog.Component
              {...dialog.props}
              closeDialog={closeCurrentDialog}
            />
          </animated.div>
        </animated.div>
      ),
      document.body
    )
  );
};

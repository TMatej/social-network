import { faClose } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { FunctionComponent, useRef } from "react";
import { createPortal } from "react-dom";
import { useStore } from "store";
import { Button } from "./button";
import { useOnClickOutside } from "hooks/use-on-click-outside";

export type DialogProps<TProps extends {}> = TProps & {
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

  if (!dialog) return null;

  return createPortal(
    <div className="fixed left-0 top-0 w-full h-full bg-black bg-opacity-25 p-4 flex items-center justify-center">
      <div
        ref={ref}
        className="bg-slate-800 rounded p-4 min-w-full sm:min-w-[400px]"
      >
        <div className="flex justify-between items-center gap-8 ">
          <span className="text-xl font-bold">{dialog.title}</span>
          <Button variant="clear" onClick={closeCurrentDialog}>
            <FontAwesomeIcon icon={faClose} />
          </Button>
        </div>
        <dialog.Component {...dialog.props} closeDialog={closeCurrentDialog} />
      </div>
    </div>,
    document.body
  );
};

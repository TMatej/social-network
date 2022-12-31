import { useEffect, useRef } from "react";
import { useIsInView } from "./use-is-in-view";

export const useViewIntersection = <TElement extends HTMLElement>(props?: {
  enabled?: boolean;
  onInView?: () => void;
  onNotInView?: () => void;
}) => {
  const { enabled = true, onInView, onNotInView } = props ?? {};
  const ref = useRef<TElement>(null);
  const isInView = useIsInView(ref);

  useEffect(() => {
    if (!enabled) return;

    isInView ? onInView?.() : onNotInView?.();
  }, [enabled, isInView]);

  return { ref, isInView };
};

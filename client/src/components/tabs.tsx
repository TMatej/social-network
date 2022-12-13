import clsx from "clsx";
import {
  createContext,
  ReactElement,
  RefObject,
  useCallback,
  useContext,
  useEffect,
  useRef,
  useState,
} from "react";
import { animated, useSpring } from "react-spring";

type TabsContextType<TabKey extends string> = {
  handleTabChange: (tabKey: TabKey) => void;
  registerTab: (tabKey: TabKey, ref: RefObject<HTMLButtonElement>) => void;
  selectedTabKey: TabKey;
};

const TabsContext = createContext<TabsContextType<any>>(undefined as never);

export const Tabs = <TabKey extends string = string>({
  tabKey,
  handleTabChange,
  children,
}: {
  tabKey?: TabKey;
  handleTabChange: (tabKey: TabKey) => void;
  children: ReactElement<{ tabKey: TabKey }>[];
}) => {
  const [refs, setRefs] = useState<{
    [key in TabKey]?: RefObject<HTMLButtonElement>;
  }>({});
  const container = useRef<HTMLDivElement>(null);

  const registerTab = useCallback(
    (key: TabKey, ref: RefObject<HTMLButtonElement>) => {
      setRefs((refs) => ({ ...refs, [key]: ref }));
    },
    []
  );

  const selected = tabKey ? refs[tabKey] : undefined;
  const style = useSpring({
    x:
      (selected?.current?.offsetLeft ?? 0) -
      (container?.current?.offsetLeft ?? 0),
    width: selected?.current?.offsetWidth ?? 0,
  });

  return (
    <TabsContext.Provider
      value={{ selectedTabKey: tabKey, handleTabChange, registerTab }}
    >
      <div ref={container}>
        <div className="flex">{children}</div>
        <animated.div style={style} className="h-1 rounded bg-cyan-700" />
      </div>
    </TabsContext.Provider>
  );
};

type TabProps<Key extends string> = {
  tabKey: Key;
  label: string;
};

export const Tab = <Key extends string>({ tabKey, label }: TabProps<Key>) => {
  const {
    handleTabChange,
    selectedTabKey: selected,
    registerTab,
  } = useContext<TabsContextType<Key>>(TabsContext);
  const ref = useRef<HTMLButtonElement>(null);

  useEffect(() => {
    registerTab(tabKey, ref);
  }, [tabKey, registerTab]);

  return (
    <button
      ref={ref}
      className={clsx(
        "p-2 outline-none font-bold transition-colors duration-300",
        {
          ["text-cyan-600"]: tabKey === selected,
          ["text-gray-300 hover:text-gray-200"]: tabKey !== selected,
        }
      )}
      onClick={() => handleTabChange(tabKey)}
    >
      {label}
    </button>
  );
};

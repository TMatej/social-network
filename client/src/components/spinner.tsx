import { useSpring, animated } from "react-spring";

export const Spinner = () => {
  const style = useSpring({
    from: { rotate: 0 },
    to: { rotate: 360 },
    loop: true,
  });

  return (
    <animated.div
      style={style}
      className="w-10 h-10 border-b-white border-8 rounded-full border-r-white border-t-white border-l-transparent border-opacity-75"
    />
  );
};

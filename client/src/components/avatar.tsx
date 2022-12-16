import { User } from "models";

export const Avatar = ({ user }: { user: User }) => {
  return (
    <div className="border-2 border-white border-opacity-5 bg-cyan-900 w-10 h-10 rounded-full overflow-hidden">
      <img
        className="w-full h-full object-contain"
        src="https://picsum.photos/200"
        alt=""
      />
    </div>
  );
};

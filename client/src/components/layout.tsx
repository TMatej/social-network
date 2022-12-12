import { Outlet } from "react-router-dom";
import { Header } from "./header";

export const Layout = () => {
  return (
    <div className="h-full flex flex-col">
      <Header />
      <div className="flex-grow">
        <Outlet />
      </div>
    </div>
  );
};

import { Route, BrowserRouter, Routes, Navigate } from "react-router-dom";
import { useStore } from "store";
import { Login } from "./login/login";
import { Signup } from "./signup/signup";
import { Profile } from "./profile/profile";
import { useQuery } from "@tanstack/react-query";
import { axios } from "api/axios";
import { Spinner } from "components/spinner";
import { Layout } from "components/layout";
import { User } from "models";
import { Info } from "./profile/info";
import { Galleries } from "./profile/galleries";
import { Gallery } from "./profile/gallery";
import { Wall } from "./profile/wall";
import { Friends } from "./profile/friends";

export const Router = () => {
  const user = useStore((state) => state.user);
  const setUser = useStore((state) => state.setUser);
  const { isLoading, isInitialLoading } = useQuery(
    ["user"],
    () => axios.get<User>("/sessions"),
    {
      onSuccess: ({ data }) => {
        setUser(data);
      },
      retry: false,
      suspense: false,
    }
  );

  if (isLoading && isInitialLoading) {
    return (
      <div className="fixed w-full h-full top-0 left-0">
        <div className="w-full h-full flex justify-center items-center">
          <Spinner />
        </div>
      </div>
    );
  }

  return (
    <BrowserRouter>
      <Routes>
        {user ? (
          <Route element={<Layout />}>
            <Route path="profile/:id" element={<Profile />}>
              <Route path="info" element={<Info />} />
              <Route path="galleries" element={<Galleries />} />
              <Route path="galleries/:galleryId" element={<Gallery />} />
              <Route path="wall" element={<Wall />} />
              <Route path="friends" element={<Friends />} />
            </Route>
          </Route>
        ) : (
          <>
            <Route path="login" element={<Login />} />
            <Route path="signup" element={<Signup />} />
            <Route path="*" element={<Navigate to="/login" />} />
          </>
        )}
      </Routes>
    </BrowserRouter>
  );
};

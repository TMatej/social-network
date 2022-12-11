import { Route, BrowserRouter, Routes, Navigate } from "react-router-dom";
import { User, useStore } from "store";

import { Login } from "./login/login";
import { Signup } from "./signup/signup";
import { Profile } from "./profile/profile";
import { useQuery } from "@tanstack/react-query";
import { axios } from "api/axios";
import { Spinner } from "components/spinner";
import { Layout } from "components/layout";

export const Router = () => {
  const user = useStore((state) => state.user);
  const setUser = useStore((state) => state.setUser);
  const { isLoading } = useQuery(["user"], () => axios.get<User>("/sessions"), {
    onSuccess: ({ data }) => {
      setUser(data);
    },
    retry: false,
  });

  if (isLoading) {
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
          <Route path="/" element={<Layout />}>
            <Route path="/profile/:id" element={<Profile />} />
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

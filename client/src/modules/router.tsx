import { Route, BrowserRouter, Routes, Navigate } from "react-router-dom";
import { useStore } from "store";

import { Login } from "./login/login";
import { Signup } from "./signup/signup";

export const Router = () => {
  const user = useStore((state) => state.user);

  console.log({ user });

  return (
    <BrowserRouter>
      <Routes>
        {user ? (
          <Route path="/" element={<h1>Home</h1>} />
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

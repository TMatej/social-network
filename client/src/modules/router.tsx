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
import { Search } from "./search";
import { ProfileFollowing } from "./profile/following";
import { Chat } from "./chat";
import { Following } from "./following";

export const Router = () => {
  const user = useStore((state) => state.user);
  const setUser = useStore((state) => state.setUser);
  const setFollowing = useStore((state) => state.setFollowing);
  const { isLoading, isFetchedAfterMount } = useQuery(
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

  useQuery(
    ["contacts", user?.id],
    () => axios.get<User[]>(`/contacts/${user?.id}`),
    {
      onSuccess: ({ data }) => {
        setFollowing(data);
      },
      retry: false,
      suspense: false,
      enabled: !!user?.id,
    }
  );

  if (isLoading && !isFetchedAfterMount) {
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
            <Route path="search" element={<Search />} />

            <Route path="chat" element={<Chat />} />
            <Route path="chat/:id" element={<Chat />} />

            <Route path="following" element={<Following />} />

            <Route path="profile/:id" element={<Profile />}>
              <Route index path="" element={<Wall />} />
              <Route path="info" element={<Info />} />
              <Route path="galleries" element={<Galleries />} />
              <Route path="galleries/:galleryId" element={<Gallery />} />
              <Route path="following" element={<ProfileFollowing />} />
            </Route>

            <Route
              path="login"
              element={<Navigate to={`/profile/${user.id}`} />}
            />
            <Route
              path="signup"
              element={<Navigate to={`/profile/${user.id}`} />}
            />
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

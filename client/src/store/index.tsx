import create, { StateCreator } from "zustand";

type User = {
  email: string;
  username: string;
};

type AuthSlice = {
  user?: User;
  setUser: (user: User) => void;
};

const createAuthSlice: StateCreator<AuthSlice> = (set) => ({
  user: undefined,
  setUser: (user) => set({ user }),
});

export const useStore = create<AuthSlice>((...a) => ({
  ...createAuthSlice(...a),
}));

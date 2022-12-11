import create, { StateCreator } from "zustand";

export type User = {
  id: number;
  email: string;
  roles: string[];
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

import { v4 } from "uuid";
import create, { StateCreator } from "zustand";

export type User = {
  id: number;
  email: string;
  roles: string[];
};

type AuthSlice = {
  user?: User;
  setUser: (user: User | undefined) => void;
};

const createAuthSlice: StateCreator<AuthSlice> = (set) => ({
  user: undefined,
  setUser: (user) => set({ user }),
});

export type NotificationType = {
  id: string;
  message: string;
  type: "success" | "warning" | "error";
};

type NotificationSlice = {
  notifications: NotificationType[];
  showNotification: (notification: Omit<NotificationType, "id">) => void;
};

const createNotificationSlice: StateCreator<NotificationSlice> = (set) => ({
  notifications: [],
  showNotification: (notification) => {
    const id = v4();
    set((prev) => ({
      notifications: [...prev.notifications, { ...notification, id }],
    }));
    setTimeout(
      () =>
        set((prev) => ({
          notifications: prev.notifications.filter((n) => n.id !== id),
        })),
      5000
    );
  },
});

type Store = AuthSlice & NotificationSlice;

export const useStore = create<Store>((...a) => ({
  ...createAuthSlice(...a),
  ...createNotificationSlice(...a),
}));

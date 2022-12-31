import { Dialog } from "components/dialog";
import { User } from "models";
import { v4 } from "uuid";
import create, { StateCreator } from "zustand";

type AuthSlice = {
  user?: User;
  setUser: (user: User | undefined) => void;
  following?: User[];
  setFollowing: (following: User[] | undefined) => void;
};

const createAuthSlice: StateCreator<AuthSlice> = (set) => ({
  user: undefined,
  setUser: (user) => set({ user }),
  following: undefined,
  setFollowing: (following) => set({ following }),
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

type DialogSlice = {
  dialogs: Dialog<any>[];
  openDialog: <TProps extends {}>(dialog: Omit<Dialog<TProps>, "id">) => void;
  closeDialog: (id: string) => void;
};

const createDialogSlice: StateCreator<DialogSlice> = (set) => ({
  dialogs: [],
  openDialog: (dialog) =>
    set((prev) => ({ dialogs: [...prev.dialogs, { ...dialog, id: v4() }] })),
  closeDialog: (id) =>
    set((prev) => ({ dialogs: prev.dialogs.filter((d) => d.id !== id) })),
});

type Store = AuthSlice & NotificationSlice & DialogSlice;

export const useStore = create<Store>((...a) => ({
  ...createAuthSlice(...a),
  ...createNotificationSlice(...a),
  ...createDialogSlice(...a),
}));

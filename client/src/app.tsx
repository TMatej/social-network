import { QueryClientProvider, QueryClient } from "@tanstack/react-query";
import { library } from "@fortawesome/fontawesome-svg-core";
import { fas } from "@fortawesome/free-solid-svg-icons";

import { Router } from "modules/router";
import { Notifications } from "components/notification/notification";
import { Dialog } from "components/dialog";

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      suspense: true,
    },
  },
});

library.add(fas);

export const App = () => {
  return (
    <QueryClientProvider client={queryClient}>
      <Notifications>
        <Router />
        <Dialog />
      </Notifications>
    </QueryClientProvider>
  );
};

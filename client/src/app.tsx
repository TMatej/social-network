import { QueryClientProvider, QueryClient } from "@tanstack/react-query";
import { library } from "@fortawesome/fontawesome-svg-core";
import { fas } from "@fortawesome/free-solid-svg-icons";

import { Router } from "modules/router";

const queryClient = new QueryClient();

library.add(fas);

export const App = () => {
  return (
    <QueryClientProvider client={queryClient}>
      <Router />
    </QueryClientProvider>
  );
};

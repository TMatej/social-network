import { faBug } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { QueryErrorResetBoundary } from "@tanstack/react-query";
import { AxiosError } from "axios";
import { Suspense } from "react";
import { ErrorBoundary, FallbackProps } from "react-error-boundary";
import { Outlet } from "react-router-dom";
import { Button } from "./button";
import { Header } from "./header";
import { Paper } from "./paper";
import { Sidebar } from "./sidebar";
import { Spinner } from "./spinner";

const fallbackRender = ({ error, resetErrorBoundary }: FallbackProps) => {
  const [message, code] =
    error instanceof AxiosError
      ? [error.message, error.code]
      : ["Unknown Error", 500];

  return (
    <div className="h-full w-full flex flex-col items-center justify-center">
      <Paper className="p-4 max-w-lg">
        <p className="text-2xl font-bold mb-4">Something went wrong ...</p>
        <div className="flex items-center gap-4">
          <FontAwesomeIcon size="4x" icon={faBug} />
          <div>
            <p>
              Error: <span className="font-bold">{message}</span>
            </p>
            <p>
              Code: status <span className="font-bold">{code}</span>
            </p>
          </div>
        </div>
        <Button className="w-full mt-4" onClick={resetErrorBoundary}>
          Try again
        </Button>
      </Paper>
    </div>
  );
};

export const Layout = () => {
  return (
    <div className="h-full flex flex-col">
      <Header />
      <div className="mt-16 flex">
        <Sidebar />
        <div className="flex-grow">
          <QueryErrorResetBoundary>
            {({ reset }) => (
              <ErrorBoundary onReset={reset} fallbackRender={fallbackRender}>
                <Suspense
                  fallback={
                    <div className="h-full w-full flex items-center justify-center">
                      <Spinner />
                    </div>
                  }
                >
                  <Outlet />
                </Suspense>
              </ErrorBoundary>
            )}
          </QueryErrorResetBoundary>
        </div>
      </div>
    </div>
  );
};

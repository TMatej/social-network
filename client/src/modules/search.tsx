import { useInfiniteQuery } from "@tanstack/react-query";
import { axios } from "api/axios";
import { Avatar } from "components/avatar";
import { Button } from "components/button";
import { Container } from "components/container";
import { LabeledItem } from "components/labeled-item";
import { Paper } from "components/paper";
import { Paginated, SearchResult } from "models";
import { Fragment } from "react";
import { NavLink, useSearchParams } from "react-router-dom";

export const Search = () => {
  const [search] = useSearchParams();
  const {
    data: results,
    fetchNextPage,
    hasNextPage,
  } = useInfiniteQuery({
    queryKey: ["search", search.get("q")],
    queryFn: ({ pageParam = 1 }) =>
      axios
        .get<Paginated<SearchResult>>(
          `/search?name=${search.get("q") ?? ""}&page=${pageParam}`
        )
        .then((res) => res.data),
    getNextPageParam: (data) => (data.items.length ? data.page + 1 : undefined),
    suspense: false,
  });

  return (
    <Container className="p-3">
      <div className="flex flex-col gap-3">
        <span className="text-2xl font-bold">
          Search results for{" "}
          <span className="text-slate-300">"{search.get("q")}"</span>:
        </span>
        {results?.pages.map((page, index) => (
          <Fragment key={index}>
            {page.items.map((result) => (
              <Paper key={result.id} className="p-3 flex gap-4 items-center">
                {result.type === "User" && (
                  <Avatar
                    user={{
                      username: result.name,
                      avatar: result.image,
                      id: result.id,
                    }}
                  />
                )}
                <div>
                  <NavLink
                    to={
                      result.type === "User"
                        ? `/profile/${result.id}`
                        : result.type === "Group"
                        ? `/groups/${result.id}`
                        : "."
                    }
                    className="text-xl font-bold mb-2"
                  >
                    {result.name}
                  </NavLink>
                  <div className="flex gap-4">
                    <LabeledItem label="Type" item={result.type} />
                    <LabeledItem
                      label="Description"
                      item={result.description}
                    />
                  </div>
                </div>
              </Paper>
            ))}
          </Fragment>
        ))}
        {!hasNextPage && (
          <p className="text-center text-slate-300">No more results</p>
        )}
        {hasNextPage && (
          <Button
            className="mx-auto"
            variant="clear"
            onClick={() => fetchNextPage()}
          >
            Load more
          </Button>
        )}
      </div>
    </Container>
  );
};

import { faThumbsUp, faTrash } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  useInfiniteQuery,
  useMutation,
  useQueryClient,
} from "@tanstack/react-query";
import axios from "axios";
import { Paginated, Comment } from "models";
import { Fragment } from "react";
import { Button } from "./button";
import { useStore } from "store";
import { Paper } from "./paper";

export const Comments = ({
  entityId,
  level = 0,
  recurseToLevel = Number.MAX_SAFE_INTEGER,
}: {
  entityId: number;
  level?: number;
  recurseToLevel?: number;
}) => {
  const showNotification = useStore((state) => state.showNotification);
  const queryClient = useQueryClient();
  const user = useStore((state) => state.user);
  const {
    data: comments,
    fetchNextPage,
    hasNextPage,
  } = useInfiniteQuery({
    queryKey: ["comments", entityId],
    queryFn: ({ pageParam = 1 }) =>
      axios
        .get<Paginated<Comment>>(
          `/comments?entityId=${entityId}&page=${pageParam}&size=5`
        )
        .then((res) => res.data),
    getNextPageParam: (data) => (data.items.length ? data.page + 1 : undefined),
    suspense: false,
  });

  const { mutate: deleteComment } = useMutation(
    (id: number) => axios.delete(`/comments/${id}`),
    {
      onSuccess: () => {
        queryClient.invalidateQueries(["comments", entityId]);
        showNotification({
          message: "comment deleted",
          type: "success",
        });
      },
      onError: () => {
        showNotification({
          message: "failed to delete comment",
          type: "error",
        });
      },
    }
  );

  return (
    <div style={{ marginLeft: (level + 1) * 20 }}>
      {comments?.pages.map((page, index) => (
        <Fragment key={index}>
          {page.items.map((comment) => (
            <Fragment key={comment.id}>
              <div className="py-2 flex justify-between items-center">
                <Paper className="p-2">{comment.content}</Paper>
                <div className="flex gap-2 items-center">
                  {user?.id === comment.userId && (
                    <Button
                      onClick={() => deleteComment(comment.id)}
                      className="bg-red-500"
                    >
                      <FontAwesomeIcon icon={faTrash} />
                    </Button>
                  )}
                </div>
              </div>
              {recurseToLevel < level && (
                <Comments entityId={comment.id} level={level + 1} />
              )}
            </Fragment>
          ))}
        </Fragment>
      ))}
      {hasNextPage ? (
        <Button
          variant="clear"
          className="mx-auto"
          onClick={() => fetchNextPage()}
        >
          load more comments
        </Button>
      ) : (
        level === 0 && <p className="text-center mt-2">no more comments</p>
      )}
    </div>
  );
};

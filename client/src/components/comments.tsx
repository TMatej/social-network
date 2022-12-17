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
import { Avatar } from "./avatar";
import { format } from "date-fns";

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
              <div className="py-2 flex gap-2">
                <Avatar user={comment.user} />
                <div className="flex-grow">
                  <Paper className="px-2 py-1 inline-block">
                    {comment.content}
                  </Paper>
                  <p className="text-xs text-slate-300">
                    {format(
                      new Date(comment.createdAt),
                      "dd. MMMM yyyy 'at' HH:mm"
                    )}
                  </p>
                </div>
                {user?.id === comment.userId && (
                  <Button
                    variant="outlined"
                    onClick={() => deleteComment(comment.id)}
                    className="self-end"
                  >
                    <FontAwesomeIcon icon={faTrash} />
                  </Button>
                )}
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

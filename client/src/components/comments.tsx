import { faThumbsUp, faTrash } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useInfiniteQuery } from "@tanstack/react-query";
import axios from "axios";
import { Paginated, Comment } from "models";
import { Fragment } from "react";
import { Button } from "./button";

export const Comments = ({
  entityId,
  level = 0,
}: {
  entityId: number;
  level?: number;
}) => {
  const { data: comments } = useInfiniteQuery({
    queryKey: ["comments", entityId],
    queryFn: ({ pageParam = 1 }) =>
      axios
        .get<Paginated<Comment>>(
          `/comments?entityId=${entityId}&page=${pageParam}&size=5`
        )
        .then((res) => res.data),
    getNextPageParam: (data) => data.page + 1,
    suspense: false,
  });

  return (
    <div
      style={{ marginLeft: (level + 1) * 20 }}
      className="border-t border-t-gray-600 mt-2"
    >
      {comments?.pages.map((page, index) => (
        <Fragment key={index}>
          {page.items.length === 0 && (
            <p className="text-center mt-2">
              {index === 0 ? "no comments" : "no more comments"}
            </p>
          )}
          {page.items.map((comment) => (
            <div key={comment.id} className="flex items-center">
              <p className="flex-grow">{comment.content}</p>
              <div className="flex gap-2 items-center">
                <Button className="bg-red-500">
                  <FontAwesomeIcon icon={faThumbsUp} />
                </Button>
                <Button className="bg-red-500">
                  <FontAwesomeIcon icon={faTrash} />
                </Button>
              </div>
              <Comments entityId={comment.id} level={level + 1} />
            </div>
          ))}
        </Fragment>
      ))}
    </div>
  );
};

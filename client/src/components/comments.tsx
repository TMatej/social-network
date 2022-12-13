import { faTrash } from "@fortawesome/free-solid-svg-icons";
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
    queryFn: ({ pageParam }) =>
      axios
        .get<Paginated<Comment>>(
          `/comments?entityId=${entityId}&page=${pageParam}&size=5`
        )
        .then((res) => res.data),
    getNextPageParam: (data) => data.page + 1,
  });

  return (
    <div style={{ marginLeft: level * 5 }}>
      {comments?.pages.map((page, index) => (
        <Fragment key={index}>
          {page.items.map((comment) => (
            <div key={comment.id} className="flex items-end">
              <p className="flex-grow">{comment.content}</p>
              <Button className="bg-red-500">
                <FontAwesomeIcon icon={faTrash} />
              </Button>
            </div>
          ))}
        </Fragment>
      ))}
    </div>
  );
};

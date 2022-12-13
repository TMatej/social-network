import {
  faImage,
  faThumbsUp,
  faTrash,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useInfiniteQuery } from "@tanstack/react-query";
import { Button } from "components/button";
import { FormTextField } from "components/input/text-field";
import { Paper } from "components/paper";
import { Form, Formik } from "formik";
import { axios } from "api/axios";
import { Paginated, Post as PostType, Profile, Comment } from "models";
import { useOutletContext } from "react-router-dom";
import { Fragment } from "react";
import { useStore } from "store";

const Post = ({ post }: { post: PostType }) => {
  const user = useStore((state) => state.user);
  const { data: comments } = useInfiniteQuery({
    queryKey: ["comments", post.id],
    queryFn: ({ pageParam }) =>
      axios
        .get<Paginated<Comment>>(
          `/posts/${post.id}/comments?page=${pageParam}&size=5`
        )
        .then((res) => res.data),
    getNextPageParam: (data) => data.page + 1,
  });

  const byLoggedInUser = user?.id === post.user.id;

  return (
    <>
      <Paper className="p-4">
        <p className="text-lg font-bold">{post.title}</p>
        <p>{post.content}</p>
        <div className="flex justify-end">
          <Button>
            <FontAwesomeIcon icon={faThumbsUp} />
          </Button>
          {byLoggedInUser && (
            <Button className="bg-red-500">
              <FontAwesomeIcon icon={faTrash} />
            </Button>
          )}
        </div>
        <span className="my-2 border-t border-t-white border-opacity-25 w-full" />
      </Paper>

      <Button className="mx-auto">view comments</Button>
    </>
  );
};

type NewPostData = {
  content: string;
};

export const Wall = () => {
  const { profile } = useOutletContext<{ profile: Profile }>();
  const { data: posts } = useInfiniteQuery({
    queryKey: ["posts"],
    queryFn: ({ pageParam }) =>
      axios
        .get<Paginated<PostType>>(
          `/profiles/${profile.id}/posts?page=${pageParam}`
        )
        .then((res) => res.data),
    getNextPageParam: (data) => data.page + 1,
  });

  return (
    <Paper className="p-4 mt-4">
      <Formik<NewPostData> initialValues={{ content: "" }} onSubmit={() => {}}>
        <Form>
          <FormTextField
            name="content"
            label="New post"
            placeholder="type some message"
            rows={3}
          />
          <div className="flex justify-between mt-2">
            <Button>
              <FontAwesomeIcon icon={faImage} />
            </Button>
            <Button type="submit">Submit</Button>
          </div>
        </Form>
      </Formik>

      <div>
        {posts?.pages.map((page, index) => (
          <Fragment key={index}>
            {page.items.map((post) => (
              <Post key={post.id} post={post} />
            ))}
          </Fragment>
        ))}
      </div>
    </Paper>
  );
};

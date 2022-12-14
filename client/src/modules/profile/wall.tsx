import {
  faImage,
  faThumbsUp,
  faTrash,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  useInfiniteQuery,
  useMutation,
  useQueryClient,
} from "@tanstack/react-query";
import { Button } from "components/button";
import { FormTextField } from "components/input/text-field";
import { Paper } from "components/paper";
import { Form, Formik } from "formik";
import { axios } from "api/axios";
import { Paginated, Post as PostType, Profile } from "models";
import { useOutletContext } from "react-router-dom";
import { Fragment, useEffect } from "react";
import { useStore } from "store";
import { Comments } from "components/comments";

const Post = ({ post }: { post: PostType }) => {
  const user = useStore((state) => state.user);
  const queryClient = useQueryClient();
  const showNotification = useStore((state) => state.showNotification);
  const { mutate: deletePost } = useMutation(
    () => axios.delete(`/posts/${post.id}`),
    {
      onSuccess: () => {
        showNotification({
          message: "post deleted",
          type: "success",
        });
        queryClient.invalidateQueries(["profiles"]);
      },
      onError: () => {
        showNotification({
          message: "failed to delete post",
          type: "error",
        });
      },
    }
  );

  const { mutate: likePost } = useMutation(
    () => axios.put(`/posts/${post.id}/like`),
    {
      onSuccess: () => {
        showNotification({
          message: "post liked",
          type: "success",
        });
        queryClient.invalidateQueries(["profiles"]);
      },
      onError: () => {
        showNotification({
          message: "failed to like post",
          type: "error",
        });
      },
    }
  );

  const byLoggedInUser = user?.id === post.userId;
  return (
    <Paper className="my-4 p-4">
      <p className="text-lg font-bold">{post.title}</p>
      <p>{post.content}</p>
      <div className="flex justify-end gap-2">
        <Button
          onClick={() => likePost()}
          leftIcon={<FontAwesomeIcon icon={faThumbsUp} />}
        >
          Like
        </Button>
        {byLoggedInUser && (
          <Button
            className="bg-red-500"
            leftIcon={<FontAwesomeIcon icon={faTrash} />}
            onClick={() => deletePost()}
          >
            Delete
          </Button>
        )}
      </div>
      <Comments entityId={post.id} />
    </Paper>
  );
};

type NewPostData = {
  content: string;
  title: string;
};

export const Wall = () => {
  const { profile } = useOutletContext<{ profile: Profile }>();
  const queryClient = useQueryClient();
  const showNotification = useStore((state) => state.showNotification);
  const user = useStore((state) => state.user);
  const { mutate: addPost } = useMutation(
    (data: NewPostData) =>
      axios.post<PostType>(`/profiles/${profile.id}/posts`, {
        ...data,
        postableId: profile?.id,
        userId: user?.id,
      }),
    {
      onSuccess: () =>
        showNotification({
          message: "Post added successfully",
          type: "success",
        }),
      onError: () =>
        showNotification({ message: "Adding post failed", type: "error" }),
      onSettled: () =>
        queryClient.invalidateQueries(["profiles", profile.id, "posts"]),
    }
  );
  const {
    data: posts,
    isLoading,
    fetchNextPage,
    hasNextPage,
  } = useInfiniteQuery({
    queryKey: ["profiles", profile.id, "posts"],
    queryFn: ({ pageParam = 1 }) =>
      axios
        .get<Paginated<PostType>>(
          `/profiles/${profile.id}/posts?page=${pageParam}&size=5`
        )
        .then((res) => res.data),
    getNextPageParam: (data) => (data.items.length ? data.page + 1 : undefined),
    suspense: false,
  });
  console.log({ isLoading, posts });

  useEffect(() => {
    const listener = (e: Event) => {
      if (
        window.innerHeight + window.scrollY >=
          document.body.offsetHeight - 100 &&
        hasNextPage
      ) {
        fetchNextPage();
      }
    };

    addEventListener("scroll", listener);
    return () => removeEventListener("scroll", listener);
  }, [hasNextPage, fetchNextPage]);

  return (
    <Paper className="p-4 mt-4">
      <p className="font-bold text-xl mb-2">Add post:</p>
      <Formik<NewPostData>
        initialValues={{ content: "", title: "" }}
        onSubmit={(data, { resetForm }) => {
          addPost(data);
          resetForm();
        }}
      >
        <Form>
          <FormTextField name="title" label="Title" placeholder="type title" />
          <FormTextField
            name="content"
            label="Content"
            placeholder="type some message"
            rows={3}
            className="mt-2"
          />
          <div className="flex justify-between mt-2">
            <Button>
              <FontAwesomeIcon icon={faImage} />
            </Button>
            <Button type="submit">Submit</Button>
          </div>
        </Form>
      </Formik>

      <p className="mt-8 font-bold text-xl mb-2">Posts:</p>
      {posts?.pages.map((page, index) => (
        <Fragment key={index}>
          {page.items.length === 0 && (
            <p className="mt-2 text-center text-gray-300">no more posts</p>
          )}
          {page.items.map((post) => (
            <Post key={post.id} post={post} />
          ))}
        </Fragment>
      ))}
    </Paper>
  );
};

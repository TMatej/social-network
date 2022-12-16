import {
  faImage,
  faPaperPlane,
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
import { Avatar } from "components/avatar";
import { format } from "date-fns";

type AddCommentData = {
  content: string;
};

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

  const { mutate: addComment } = useMutation(
    (data: AddCommentData) => axios.post(`/comments?entityId=${post.id}`, data),
    {
      onSuccess: () => {
        showNotification({
          message: "comment added",
          type: "success",
        });
        queryClient.invalidateQueries(["comments", post.id]);
      },
      onError: () => {
        showNotification({
          message: "failed to add comment",
          type: "error",
        });
      },
    }
  );

  const byLoggedInUser = user?.id === post.userId;
  return (
    <div>
      <div className="flex gap-4 items-center mb-3">
        <Avatar user={post.user} />
        <div>
          <p className="text-lg font-bold">{post.user.username}</p>
          <p className="text-sm">
            {format(new Date(post.createdAt), "dd. MMMM yyyy 'at' HH:mm")}
          </p>
        </div>
      </div>
      <p className="text-lg font-bold">{post.title}</p>
      <p className="whitespace-pre">{post.content}</p>
      <div className="flex gap-2 justify-end">
        <Button
          variant="outlined"
          onClick={() => likePost()}
          leftIcon={<FontAwesomeIcon icon={faThumbsUp} />}
        >
          Like
        </Button>
        {byLoggedInUser && (
          <Button
            variant="outlined"
            leftIcon={<FontAwesomeIcon icon={faTrash} />}
            onClick={() => deletePost()}
          >
            Delete
          </Button>
        )}
      </div>
      <div className="pt-2" />
      <div style={{ marginLeft: 20 }}>
        <div className="border-t border-t-gray-600" />
        <Formik<AddCommentData>
          initialValues={{ content: "" }}
          onSubmit={(data, { resetForm }) => {
            addComment(data);
            resetForm();
          }}
        >
          <Form className="flex items-end gap-2 py-2">
            <FormTextField
              name="content"
              className="flex-grow"
              placeholder="type a comment..."
            />
            <Button
              type="submit"
              leftIcon={<FontAwesomeIcon icon={faPaperPlane} />}
            >
              Send
            </Button>
          </Form>
        </Formik>
      </div>
      <Comments entityId={post.id} />
    </div>
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
    isFetchingNextPage,
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

  useEffect(() => {
    const listener = () => {
      if (
        window.innerHeight + window.scrollY >=
          document.body.offsetHeight - 100 &&
        !isFetchingNextPage &&
        hasNextPage
      ) {
        fetchNextPage();
      }
    };

    addEventListener("scroll", listener);
    return () => removeEventListener("scroll", listener);
  }, [hasNextPage, fetchNextPage, isFetchingNextPage]);

  return (
    <>
      <Paper className="p-4 mt-4">
        <Formik<NewPostData>
          initialValues={{ content: "", title: "" }}
          onSubmit={(data, { resetForm }) => {
            addPost(data);
            resetForm();
          }}
        >
          <Form>
            <FormTextField
              name="title"
              label="Title"
              placeholder="enter a title..."
            />
            <FormTextField
              name="content"
              label="Content"
              placeholder="enter some content..."
              rows={3}
              className="mt-2"
            />
            <div className="flex justify-between mt-4">
              <Button>
                <FontAwesomeIcon icon={faImage} />
              </Button>
              <Button
                type="submit"
                leftIcon={<FontAwesomeIcon icon={faPaperPlane} />}
              >
                Submit
              </Button>
            </div>
          </Form>
        </Formik>
      </Paper>

      <Paper className="p-4 mt-4">
        <p className="font-bold text-xl">Posts</p>
      </Paper>

      {posts?.pages.map((page, index) => (
        <Fragment key={index}>
          {page.items.length === 0 && (
            <p className="text-center text-gray-300 my-4">no more posts</p>
          )}
          {page.items.map((post) => (
            <Paper key={index} className="p-4 mt-4">
              <Post key={post.id} post={post} />
            </Paper>
          ))}
        </Fragment>
      ))}
    </>
  );
};

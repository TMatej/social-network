import { faPaperPlane, faTrash } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { Button } from "components/button";
import { FormTextField } from "components/input/text-field";
import { Form, Formik } from "formik";
import { axios } from "api/axios";
import { Post as PostType } from "models";
import { useStore } from "store";
import { Comments } from "components/comments";
import { Avatar } from "components/avatar";
import { format } from "date-fns";
import * as yup from "yup";

type AddCommentData = {
  content: string;
};

const schema = yup.object().shape({
  content: yup.string().min(3).required(),
});

export const Post = ({ post }: { post: PostType }) => {
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
        queryClient.invalidateQueries(["groups"]);
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
        {
          // <Button
          //   variant="outlined"
          //   onClick={() => likePost()}
          //   leftIcon={<FontAwesomeIcon icon={faThumbsUp} />}
          // >
          //   Like
          // </Button>
        }
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
          validationSchema={schema}
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

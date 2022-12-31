import { faPaperPlane } from "@fortawesome/free-solid-svg-icons";
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
import { Group, Paginated, Post as PostType } from "models";
import { useOutletContext } from "react-router-dom";
import { Fragment } from "react";
import { useStore } from "store";
import { Post } from "components/post";
import * as yup from "yup";
import { useViewIntersection } from "hooks/use-view-intersection";

type NewPostData = {
  content: string;
  title: string;
};

const schema = yup.object().shape({
  content: yup.string().min(3).max(524).required(),
  title: yup.string().min(3).max(20).required(),
});

export const Wall = () => {
  const { group } = useOutletContext<{ group: Group }>();
  const queryClient = useQueryClient();
  const showNotification = useStore((state) => state.showNotification);
  const user = useStore((state) => state.user);
  const { mutate: addPost } = useMutation(
    (data: NewPostData) =>
      axios.post<PostType>(`/groups/${group.id}/posts`, {
        ...data,
        postableId: group?.id,
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
      onSettled: () => queryClient.invalidateQueries(["groups", group.id]),
    }
  );

  const {
    data: posts,
    isFetching,
    fetchNextPage,
    hasNextPage,
  } = useInfiniteQuery({
    queryKey: ["groups", group.id, "posts"],
    queryFn: ({ pageParam = 1 }) =>
      axios
        .get<Paginated<PostType>>(
          `/groups/${group.id}/posts?page=${pageParam}&size=5`
        )
        .then((res) => res.data),
    getNextPageParam: (data) => (data.items.length ? data.page + 1 : undefined),
    suspense: false,
  });

  const { ref: bottomTarget } = useViewIntersection<HTMLDivElement>({
    enabled: !isFetching && hasNextPage,
    onInView: () => fetchNextPage(),
  });

  return (
    <>
      <Paper className="p-4 mt-4">
        <Formik<NewPostData>
          validationSchema={schema}
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
            <div className="flex justify-end mt-4">
              {
                // <Button>
                //   <FontAwesomeIcon icon={faImage} />
                // </Button>
              }
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
            <Paper key={post.id} className="p-4 mt-4">
              <Post post={post} />
            </Paper>
          ))}
        </Fragment>
      ))}

      <div ref={bottomTarget} />
    </>
  );
};

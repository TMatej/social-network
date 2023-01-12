import {
  useInfiniteQuery,
  useMutation,
  useQuery,
  useQueryClient,
} from "@tanstack/react-query";
import { axios } from "api/axios";
import clsx from "clsx";
import { Avatar } from "components/avatar";
import { Button } from "components/button";
import { Container } from "components/container";
import { FormTextField } from "components/input/text-field";
import { Paper } from "components/paper";
import { Formik, Form } from "formik";
import { useViewIntersection } from "hooks/use-view-intersection";
import { Message, Paginated, User } from "models";
import { Fragment, useMemo, useRef } from "react";
import { NavLink, useParams } from "react-router-dom";
import { useStore } from "store";
import * as yup from "yup";

type SendMessageData = {
  content: string;
};

const schema = yup.object().shape({
  content: yup.string().required(),
});

export const Chat = () => {
  const user = useStore((store) => store.user);
  const queryClient = useQueryClient();
  const { id } = useParams<{ id: string }>();
  const container = useRef<HTMLDivElement>(null);
  const bottomTarget = useRef<HTMLDivElement>(null);

  const {
    data: messages,
    fetchNextPage,
    isFetching,
    hasNextPage,
  } = useInfiniteQuery({
    queryKey: ["chat", "messages", id],
    queryFn: ({ pageParam = 1 }) =>
      axios
        .get<Paginated<Message>>(
          `/chat/messages?user1Id=${user?.id}&user2Id=${id}&page=${pageParam}`
        )
        .then((res) => res.data),
    getNextPageParam: (data) => (data.items.length ? data.page + 1 : undefined),
    suspense: false,
    refetchInterval: id !== undefined ? 5000 : false,
    enabled: user?.id !== undefined && id !== undefined,
  });

  useQuery(
    ["contacts", user?.id],
    () => axios.get<User[]>(`/contacts/${user?.id}`),
    {
      retry: false,
      suspense: false,
      enabled: user?.id !== undefined,
    }
  );

  const { ref: topTarget } = useViewIntersection<HTMLDivElement>({
    enabled: !isFetching && hasNextPage,
    onInView: () => fetchNextPage(),
  });

  const { mutate: sendMessage } = useMutation(
    ({ content }: SendMessageData) =>
      axios.post("/chat/messages", {
        content,
        receiverId: id,
        authorId: user?.id,
      }),
    {
      onSuccess: async () => {
        await queryClient.invalidateQueries(["chat", "messages", id]);
        bottomTarget.current?.scrollIntoView({ behavior: "smooth" });
      },
    }
  );

  return (
    <Container className="p-4">
      <div className="h-full flex flex-col gap-4">
        <Paper className="p-4">
          <p className="text-xl font-bold">Chat</p>
        </Paper>
        <div className="h-full flex gap-4 items-stretch">
          <Paper className="p-3 flex flex-col">
            <div className="flex flex-col overflow-y-auto flex-shrink flex-grow h-0">
              <Contacts />
            </div>
          </Paper>
          <Paper className="p-4 flex-grow flex flex-col justify-end">
            <div
              ref={container}
              className="pr-2 flex flex-col-reverse gap-2 overflow-y-auto flex-shrink flex-grow h-0"
            >
              <div ref={bottomTarget} />
              <div className="mt-auto" />
              {messages?.pages.map((page, index) => (
                <Fragment key={index}>
                  {page.items.map((message) => (
                    <Paper
                      className={clsx("p-2 !bg-opacity-10", {
                        "self-end !bg-cyan-500": message.authorId === user?.id,
                        "self-start !bg-emerald-500":
                          message.authorId !== user?.id,
                      })}
                    >
                      <p className="max-w-xs md:max-w-sm lg:max-w-md xl:max-w-lg 2xl:max-w-xl break-words">
                        {message.content}
                      </p>
                    </Paper>
                  ))}
                </Fragment>
              ))}
              {!hasNextPage && (
                <p className="text-center text-slate-300">No more messages</p>
              )}
              <div ref={topTarget} />
            </div>
            <Formik<SendMessageData>
              validationSchema={schema}
              initialValues={{ content: "" }}
              onSubmit={(data, { resetForm }) => {
                sendMessage(data);
                resetForm();
              }}
            >
              <Form className="mt-4 flex gap-2 items-center">
                <FormTextField
                  className="flex-grow"
                  disabled={!id}
                  name="content"
                  placeholder="type message"
                  errorVariant="outline"
                />
                <Button disabled={!id} variant="outlined" type="submit">
                  Submit
                </Button>
              </Form>
            </Formik>
          </Paper>
        </div>
      </div>
    </Container>
  );
};

const ChatItem = ({ user }: { user: User }) => {
  return (
    <NavLink
      to={`/chat/${user.id}`}
      className={({ isActive }) =>
        clsx(
          "rounded text-white flex gap-2 p-2 hover:bg-slate-600 items-center",
          {
            "text-cyan-500": isActive,
          }
        )
      }
    >
      <Avatar user={user} />
      <p className="text-ellipsis">{user.username}</p>
    </NavLink>
  );
};

const Contacts = () => {
  const user = useStore((store) => store.user);
  const following = useStore((store) => store.following);
  const {
    data: users,
    fetchNextPage,
    isFetching,
    hasNextPage,
  } = useInfiniteQuery({
    queryKey: ["chat", "conversations", user?.id],
    queryFn: ({ pageParam = 1 }) =>
      axios
        .get<Paginated<User>>(
          `/chat/conversations/${user?.id}?page=${pageParam}`
        )
        .then((res) => res.data),
    getNextPageParam: (data) => (data.items.length ? data.page + 1 : undefined),
    suspense: false,
  });

  const { ref: bottomTarget } = useViewIntersection<HTMLDivElement>({
    enabled: !isFetching && hasNextPage,
    onInView: () => fetchNextPage(),
  });

  const userIdToIsInContacts = useMemo<Record<number, boolean>>(() => {
    return (
      users?.pages.reduce(
        (acc, page) => ({
          ...acc,
          ...page.items.reduce<Record<number, boolean>>(
            (acc, user) => ({ ...acc, [user.id]: true }),
            {}
          ),
        }),
        {}
      ) ?? {}
    );
  }, [users]);

  const filteredFollowing = useMemo(
    () => following?.filter(({ id }) => !userIdToIsInContacts[id]),
    [following, userIdToIsInContacts]
  );

  return (
    <Fragment>
      <p className="text-lg mb-3 pr-4 font-semibold">Direct messages</p>
      {users?.pages.map((pages, index) => (
        <Fragment key={index}>
          {pages.items.map((user) => (
            <ChatItem user={user} />
          ))}
        </Fragment>
      ))}
      <div ref={bottomTarget} />
      <p className="text-lg my-3 pr-4 font-semibold">Following</p>
      {filteredFollowing?.length === 0 && <p>No more users</p>}
      {filteredFollowing?.map((user) => (
        <ChatItem user={user} />
      ))}
    </Fragment>
  );
};

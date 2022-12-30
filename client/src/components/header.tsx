import { Button } from "./button";
import { Paper } from "./paper";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSearch, faSignOut } from "@fortawesome/free-solid-svg-icons";
import { useMutation } from "@tanstack/react-query";
import { axios } from "api/axios";
import { useStore } from "store";
import { FormTextField } from "./input/text-field";
import { Formik, Form } from "formik";
import { Avatar } from "./avatar";
import { useNavigate } from "react-router-dom";

export const Header = () => {
  const user = useStore((store) => store.user);
  const setUser = useStore((store) => store.setUser);
  const setFollowing = useStore((state) => state.setFollowing);
  const navigate = useNavigate();
  const { mutate } = useMutation(() => axios.delete("/sessions"), {
    onSuccess: () => {
      setUser(undefined);
      setFollowing(undefined);
    },
  });

  return (
    <Paper className="fixed h-16 !rounded-none !bg-slate-800 z-10 p-2 flex w-full justify-between items-center">
      <Formik
        initialValues={{ search: "" }}
        onSubmit={({ search }, { resetForm }) => {
          navigate(`/search?q=${search}`);
          resetForm();
        }}
      >
        <Form>
          <FormTextField
            className="flex-grow"
            name="search"
            after={
              <Button className="flex-grow-0 self-stretch" type="submit">
                <FontAwesomeIcon icon={faSearch} />
              </Button>
            }
          />
        </Form>
      </Formik>
      <div className="flex items-center gap-3">
        <Avatar user={user} />
        <Button
          leftIcon={<FontAwesomeIcon icon={faSignOut} />}
          onClick={mutate}
        >
          log out
        </Button>
      </div>
    </Paper>
  );
};

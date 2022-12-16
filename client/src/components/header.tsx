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

export const Header = () => {
  const user = useStore((store) => store.user);
  const setUser = useStore((store) => store.setUser);
  const { mutate } = useMutation(() => axios.delete("/sessions"), {
    onSuccess: () => {
      setUser(undefined);
    },
  });

  return (
    <Paper className="!bg-slate-800 sticky top-0 p-2 flex w-full justify-between items-center">
      <Formik initialValues={{ search: "" }} onSubmit={() => {}}>
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

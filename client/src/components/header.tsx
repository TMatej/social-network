import { Button } from "./button";
import { Paper } from "./paper";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSearch, faSignOut } from "@fortawesome/free-solid-svg-icons";
import { useMutation } from "@tanstack/react-query";
import { axios } from "api/axios";
import { useStore } from "store";
import { FormTextField } from "./input/text-field";
import { Formik, Form } from "formik";

export const Header = () => {
  const setUser = useStore((state) => state.setUser);
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
        <div className="border-2 border-white border-opacity-5 bg-cyan-900 w-10 h-10 rounded-full overflow-hidden">
          <img
            className="w-full h-full object-contain"
            src="https://picsum.photos/200"
            alt=""
          />
        </div>
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

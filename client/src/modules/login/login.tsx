import { useMutation } from "@tanstack/react-query";
import { Form, Formik } from "formik";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSignIn } from "@fortawesome/free-solid-svg-icons";

import { TextField } from "components/input/text-field";
import { Button } from "components/button";
import { Paper } from "components/paper";
import { NavLink } from "react-router-dom";

type LoginFormData = {
  email: string;
  password: string;
};

const login = async (data: LoginFormData) => {};

export const Login = () => {
  const { mutate } = useMutation(login, {
    onSuccess: () => {},
    onError: () => {},
  });

  return (
    <Formik<LoginFormData>
      initialValues={{
        email: "",
        password: "",
      }}
      onSubmit={(data) => mutate(data)}
    >
      <Form className="h-full">
        <div className="h-full flex flex-col justify-center items-center">
          <Paper className="md:min-w-[350px]">
            <h1 className="text-xl font-bold mb-6">Login</h1>
            <TextField
              className="mb-4"
              label="Email"
              placeholder="john@gmail.com"
            />
            <TextField
              className="mb-6"
              label="Password"
              placeholder="*******"
            />
            <Button leftIcon={<FontAwesomeIcon icon={faSignIn} />}>
              login
            </Button>
          </Paper>
          <span className="mt-2">
            Don't have an account yet? <NavLink to="/signup">Sign up</NavLink>
          </span>
        </div>
      </Form>
    </Formik>
  );
};

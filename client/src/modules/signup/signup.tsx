import { useMutation } from "@tanstack/react-query";
import { Form, Formik } from "formik";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faFileEdit } from "@fortawesome/free-solid-svg-icons";

import { TextField } from "components/input/text-field";
import { Button } from "components/button";
import { Paper } from "components/paper";
import { NavLink } from "react-router-dom";

type SignupFormData = {
  email: string;
  password: string;
  repeatPassword: string;
};

const signup = async (data: SignupFormData) => {};

export const Signup = () => {
  const { mutate } = useMutation(signup, {
    onSuccess: () => {},
    onError: () => {},
  });

  return (
    <Formik<SignupFormData>
      initialValues={{
        email: "",
        password: "",
        repeatPassword: "",
      }}
      onSubmit={(data) => mutate(data)}
    >
      <Form className="h-full">
        <div className="h-full flex flex-col justify-center items-center">
          <Paper className="md:min-w-[350px]">
            <h1 className="text-xl font-bold mb-6">Sign up</h1>
            <TextField
              className="mb-4"
              label="Email"
              placeholder="john@gmail.com"
            />
            <TextField
              className="mb-4"
              label="Password"
              placeholder="*******"
            />
            <TextField
              className="mb-6"
              label="Repeat password"
              placeholder="*******"
            />
            <Button leftIcon={<FontAwesomeIcon icon={faFileEdit} />}>
              sign up
            </Button>
          </Paper>
          <span className="mt-2">
            Don't have an account yet? <NavLink to="/login">Login</NavLink>
          </span>
        </div>
      </Form>
    </Formik>
  );
};

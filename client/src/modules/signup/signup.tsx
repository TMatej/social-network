import { useMutation } from "@tanstack/react-query";
import { Form, Formik } from "formik";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faFileEdit } from "@fortawesome/free-solid-svg-icons";

import { FormTextField, TextField } from "components/input/text-field";
import { Button } from "components/button";
import { Paper } from "components/paper";
import { NavLink, useNavigate } from "react-router-dom";
import { axios } from "api/axios";

type SignupFormData = {
  username: string;
  email: string;
  password: string;
  repeatPassword: string;
};

export const Signup = () => {
  const navigate = useNavigate();
  const { mutate } = useMutation(
    (data: SignupFormData) => axios.post("/users", data),
    {
      onSuccess: () => {
        navigate("/login");
      },
      onError: () => {},
    }
  );

  return (
    <Formik<SignupFormData>
      initialValues={{
        username: "johndoe",
        email: "johndoe@gmail.com",
        password: "johndoe",
        repeatPassword: "johndoe",
      }}
      onSubmit={(data) => mutate(data)}
    >
      <Form className="h-full">
        <div className="h-full flex flex-col justify-center items-center">
          <Paper className="md:min-w-[350px]">
            <h1 className="text-xl font-bold mb-6">Sign up</h1>
            <FormTextField
              name="username"
              className="mb-4"
              label="Username"
              placeholder="johndoe"
            />
            <FormTextField
              name="email"
              className="mb-4"
              label="Email"
              placeholder="john@gmail.com"
            />
            <FormTextField
              name="password"
              className="mb-4"
              label="Password"
              placeholder="*******"
            />
            <FormTextField
              name="repeatPassword"
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

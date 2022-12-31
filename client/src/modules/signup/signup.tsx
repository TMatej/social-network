import { useMutation } from "@tanstack/react-query";
import { Form, Formik } from "formik";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faFileEdit } from "@fortawesome/free-solid-svg-icons";

import { FormTextField } from "components/input/text-field";
import { Button } from "components/button";
import { Paper } from "components/paper";
import { NavLink, useNavigate } from "react-router-dom";
import { axios } from "api/axios";
import { useStore } from "store";
import * as yup from "yup";

type SignupFormData = {
  username: string;
  email: string;
  password: string;
  repeatPassword: string;
};

const schema = yup.object().shape({
  username: yup.string().min(3).required(),
  email: yup.string().email().min(3).required(),
  password: yup.string().min(4).required(),
  repeatPassword: yup
    .string()
    .oneOf([yup.ref("password"), null], "passwords must match")
    .min(4),
});

export const Signup = () => {
  const navigate = useNavigate();
  const showNotification = useStore((state) => state.showNotification);
  const { mutate, isLoading } = useMutation(
    (data: SignupFormData) => axios.post("/users", data),
    {
      onSuccess: () => {
        navigate("/login");
        showNotification({
          message: "successfully signed up",
          type: "success",
        });
      },
      onError: () => {
        showNotification({
          message: "signup failed",
          type: "error",
        });
      },
    }
  );

  return (
    <Formik<SignupFormData>
      initialValues={{
        username: "",
        email: "",
        password: "",
        repeatPassword: "",
      }}
      validationSchema={schema}
      onSubmit={(data) => mutate(data)}
    >
      <Form className="h-full">
        <div className="h-full flex flex-col justify-center items-center">
          <Paper className="p-3 md:min-w-[350px]">
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
              type="password"
            />
            <FormTextField
              name="repeatPassword"
              className="mb-6"
              label="Repeat password"
              placeholder="*******"
              type="password"
            />
            <Button
              className="w-full"
              disabled={isLoading}
              leftIcon={<FontAwesomeIcon icon={faFileEdit} />}
              type="submit"
            >
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

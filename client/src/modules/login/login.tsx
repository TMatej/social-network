import { useMutation } from "@tanstack/react-query";
import { Form, Formik } from "formik";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSignIn } from "@fortawesome/free-solid-svg-icons";
import { NavLink, useNavigate } from "react-router-dom";
import { FormTextField } from "components/input/text-field";
import { Button } from "components/button";
import { Paper } from "components/paper";
import { axios } from "api/axios";
import { User, useStore } from "store";

type LoginFormData = {
  email: string;
  password: string;
};

export const Login = () => {
  const setUser = useStore((state) => state.setUser);
  const navigate = useNavigate();
  const { mutate } = useMutation(
    (data: LoginFormData) => axios.post<User>("/sessions", data),
    {
      onSuccess: ({ data }) => {
        setUser(data);
        navigate("/");
      },
      onError: () => {},
    }
  );

  return (
    <Formik<LoginFormData>
      initialValues={{
        email: "ciza@gmail.com",
        password: "ciza",
      }}
      onSubmit={(data) => mutate(data)}
    >
      <Form className="h-full">
        <div className="h-full flex flex-col justify-center items-center">
          <Paper className="md:min-w-[350px]">
            <h1 className="text-xl font-bold mb-6">Login</h1>
            <FormTextField
              name="email"
              className="mb-4"
              label="Email"
              placeholder="john@gmail.com"
            />
            <FormTextField
              name="password"
              className="mb-6"
              label="Password"
              type="password"
              placeholder="*******"
            />
            <Button
              type="submit"
              leftIcon={<FontAwesomeIcon icon={faSignIn} />}
            >
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

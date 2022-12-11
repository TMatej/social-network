import clsx from "clsx";
import { useField } from "formik";
import { ReactNode } from "react";

import { WithRequiredProps } from "types/utility-types";

type TextFieldProps = {
  className?: string;
  value?: string;
  error?: string;
  name?: string;
  label?: string;
  placeholder?: string;
  onChange?: (value: string) => void;
  type?: "text" | "password";
  after?: ReactNode;
};

export const TextField = ({
  className,
  value,
  name,
  error,
  label,
  placeholder,
  onChange,
  type = "text",
  after,
}: TextFieldProps) => {
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    onChange?.(e.target.value);
  };

  return (
    <div>
      <label className={clsx("block", className)}>
        {label && (
          <span className="block mb-1 font-semibold text-gray-300">
            {label}:
          </span>
        )}
        <div className="rounded bg-white bg-opacity-5 p-1 w-full flex">
          <input
            className="px-1 bg-transparent outline-none"
            name={name}
            type={type}
            value={value}
            onChange={handleChange}
            placeholder={placeholder}
          />
          {after}
        </div>
      </label>
      {error && <span className="text-sm text-red-500">{error}</span>}
    </div>
  );
};

type FormTextFieldProps = WithRequiredProps<
  Omit<TextFieldProps, "value" | "onChange" | "error">,
  "name"
>;

export const FormTextField = ({ name, ...props }: FormTextFieldProps) => {
  const [field, meta, helper] = useField<string>(name);

  return (
    <TextField
      {...props}
      value={field.value}
      error={meta.error}
      onChange={helper.setValue}
    />
  );
};

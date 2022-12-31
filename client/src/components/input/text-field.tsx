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
  rows?: number;
  disabled?: boolean;
  errorVariant?: "text" | "outline";
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
  rows,
  disabled,
  errorVariant = "text",
}: TextFieldProps) => {
  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    onChange?.(e.target.value);
  };

  return (
    <div className={className}>
      <label className="block">
        {label && (
          <span className="block mb-1 font-semibold text-gray-300">
            {label}:
          </span>
        )}
        <div
          className={clsx(
            "rounded border border-transparent box-border bg-white bg-opacity-5 p-1 w-full flex",
            {
              "!border-red-500 border-opacity-50":
                errorVariant === "outline" && error,
            }
          )}
        >
          {rows ? (
            <textarea
              className="px-1 bg-transparent outline-none w-full"
              name={name}
              value={value}
              onChange={handleChange}
              placeholder={placeholder}
              disabled={disabled}
              rows={rows}
            />
          ) : (
            <input
              className="px-1 bg-transparent outline-none w-full"
              name={name}
              type={type}
              value={value}
              disabled={disabled}
              onChange={handleChange}
              placeholder={placeholder}
            />
          )}
          {after}
        </div>
      </label>
      {error && errorVariant === "text" && (
        <span className="text-sm text-red-500">{error}</span>
      )}
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

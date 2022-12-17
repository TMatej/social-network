import { faClose, faUpload, faX } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Button } from "components/button";
import { useField } from "formik";
import { useRef } from "react";

import { WithRequiredProps } from "types/utility-types";

type FileFieldProps = {
  className?: string;
  error?: string;
  name?: string;
  label?: string;
  type?: "text" | "password";
  accept?: string;
  onClear?: () => void;
} & (
  | {
      multiple: true;
      value?: File[];
      onChange?: (value?: File[]) => void;
    }
  | {
      multiple?: false;
      value?: File;
      onChange?: (value?: File) => void;
    }
);

export const FileField = ({
  className,
  value,
  name,
  error,
  label,
  multiple,
  onChange,
  accept,
  onClear,
}: FileFieldProps) => {
  const ref = useRef<HTMLInputElement | null>(null);
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    multiple
      ? onChange?.([...(e?.target?.files ?? [])])
      : onChange?.(e.target.files?.[0]);
  };

  const handleClear = () => {
    onClear?.();
    if (!ref.current) return;
    ref.current.value = "";
  };

  return (
    <div className={className}>
      <div>
        <label className="inline-block cursor-pointer font-bold rounded border py-1 px-2 border-slate-600 hover:bg-slate-600">
          <FontAwesomeIcon icon={faUpload} className="mr-2" />
          {label ?? !multiple ? "Choose a file" : "Choose files"}
          <input
            ref={ref}
            className="hidden"
            accept={accept}
            type="file"
            name={name}
            onChange={handleChange}
          />
        </label>
      </div>
      <span className="flex items-center text-sm text-slate-300">
        {!multiple
          ? value?.name ?? "No file chosen"
          : value?.map((file) => file.name).join(", ") ?? "No files chosen"}
        {value && (
          <Button
            variant="clear"
            className="!inline-block"
            onClick={handleClear}
          >
            <FontAwesomeIcon className="text-red-500" icon={faClose} />
          </Button>
        )}
      </span>
      {error && <span className="text-sm text-red-500">{error}</span>}
    </div>
  );
};

type FormFileFiledProps = WithRequiredProps<
  Omit<FileFieldProps, "value" | "onChange" | "onClear" | "error">,
  "name"
>;

export const FormFileField = ({ name, ...props }: FormFileFiledProps) => {
  const [field, meta, helper] = useField<File | File[] | undefined>(name);

  return (
    <FileField
      {...props}
      value={field.value as any}
      multiple={props.multiple}
      error={meta.error}
      onChange={helper.setValue}
      onClear={() => helper.setValue(undefined)}
    />
  );
};

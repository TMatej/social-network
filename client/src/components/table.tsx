import {
  faChevronLeft,
  faChevronRight,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { ReactNode } from "react";
import { Button } from "./button";
import { Container } from "./container";
import { Paper } from "./paper";

export type Column<TData> = {
  key: string;
  label: string;
  render: (data: TData) => React.ReactNode;
};

type PaginationProps = {
  page: number;
  size: number;
  total: number;
  onChange: (index: number) => void;
};

const Pagination = ({ page, size, total, onChange }: PaginationProps) => {
  return (
    <div className="p-4 flex items-center justify-end gap-2">
      <Button
        disabled={page <= 1}
        variant="outlined"
        onClick={() => onChange(page - 1)}
      >
        <FontAwesomeIcon icon={faChevronLeft} />
      </Button>
      <Button
        disabled={page * size >= total}
        variant="outlined"
        onClick={() => onChange(page + 1)}
      >
        <FontAwesomeIcon icon={faChevronRight} />
      </Button>
    </div>
  );
};

export const Table = <TData,>({
  title,
  columns,
  data,
  pagination,
}: {
  title?: ReactNode;
  columns: Column<TData>[];
  data: TData[];
  pagination?: PaginationProps;
}) => {
  return (
    <Container className="p-4">
      <Paper>
        <div className="flex justify-between p-4">{title}</div>
        <table className="table-auto w-full">
          <thead>
            {columns.map((column) => (
              <th
                align="left"
                className="p-4 border-b-2 border-b-slate-600"
                key={column.key}
              >
                {column.label}
              </th>
            ))}
          </thead>
          <tbody>
            {data.length === 0 && (
              <tr className="w-full p-4 border-b border-b-slate-600 font-bold">
                <td
                  colSpan={columns.length}
                  className="p-4 text-slate-300 text-center"
                >
                  No more data ...
                </td>
              </tr>
            )}
            {data.map((item, index) => (
              <tr key={index}>
                {columns.map((column) => (
                  <td
                    className="py-2 px-4 border-b border-b-slate-600"
                    key={column.key}
                  >
                    {column.render(item)}
                  </td>
                ))}
              </tr>
            ))}
          </tbody>
        </table>
        {pagination && <Pagination {...pagination} />}
      </Paper>
    </Container>
  );
};

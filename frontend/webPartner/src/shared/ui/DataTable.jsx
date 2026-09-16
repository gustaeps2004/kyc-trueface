import {
  flexRender,
  getCoreRowModel,
  getPaginationRowModel,
  getSortedRowModel,
  useReactTable,
} from '@tanstack/react-table';
import { ChevronLeft, ChevronRight } from 'lucide-react';
import { useTranslation } from 'react-i18next';
import { IconButton } from '@/shared/ui/IconButton';

export function DataTable({
  columns,
  data,
  getRowId,
  isLoading = false,
  emptyMessage,
  enableSorting = false,
  enablePagination = false,
  pageSize = 10,
}) {
  const { t } = useTranslation();

  const table = useReactTable({
    data,
    columns,
    getRowId: getRowId ?? ((row) => row.code ?? row.id),
    getCoreRowModel: getCoreRowModel(),
    getSortedRowModel: enableSorting ? getSortedRowModel() : undefined,
    getPaginationRowModel: enablePagination ? getPaginationRowModel() : undefined,
    enableSorting,
    initialState: { pagination: { pageSize } },
  });

  const rows = table.getRowModel().rows;

  if (isLoading) {
    return (
      <div className="mt-6 py-8 text-center text-sm text-fg-subtle">
        {t('notifications.loading')}
      </div>
    );
  }

  if (rows.length === 0) {
    return (
      <div className="mt-6 py-8 text-center text-sm text-fg-subtle">
        {emptyMessage}
      </div>
    );
  }

  return (
    <div className="h-full flex flex-col">
      <div className="flex-1 min-h-0 overflow-auto mt-6">
        <div className="hidden md:block relative rounded-lg">
          <table className="w-full text-sm text-center text-fg-muted">
            <thead>
              {table.getHeaderGroups().map((headerGroup) => (
                <tr key={headerGroup.id} className="bg-surface border-b border-divider/30">
                  {headerGroup.headers.map((header) => (
                    <th
                      key={header.id}
                      className="
                        sticky
                        top-0
                        z-10
                        bg-surface
                        px-6
                        py-3
                        text-xs
                        font-medium
                        text-fg-subtle
                        uppercase
                        tracking-wide
                      "
                    >
                      {header.isPlaceholder
                        ? null
                        : flexRender(header.column.columnDef.header, header.getContext())}
                    </th>
                  ))}
                </tr>
              ))}
            </thead>
            <tbody>
              {rows.map((row) => (
                <tr
                  key={row.id}
                  className="
                    border-b
                    border-divider/15
                    transition-colors
                    duration-150
                    hover:bg-surface/50
                  "
                >
                  {row.getVisibleCells().map((cell) => (
                    <td key={cell.id} className="px-6 py-2">
                      {flexRender(cell.column.columnDef.cell, cell.getContext())}
                    </td>
                  ))}
                </tr>
              ))}
            </tbody>
          </table>
        </div>

        <div className="md:hidden space-y-3">
          {rows.map((row) => {
            const dataCells = [];
            const actionCells = [];

            row.getVisibleCells().forEach((cell) => {
              if (cell.column.columnDef.accessorKey) {
                dataCells.push(cell);
              } else {
                actionCells.push(cell);
              }
            });

            return (
              <div
                key={row.id}
                className="rounded-lg border border-divider/30 bg-surface p-4 space-y-2"
              >
                {dataCells.map((cell) => (
                  <div key={cell.id} className="flex items-center justify-between gap-3">
                    <span className="text-xs text-fg-subtle uppercase tracking-wide shrink-0">
                      {flexRender(cell.column.columnDef.header, cell.getContext())}
                    </span>
                    <span className="text-fg-muted text-right">
                      {flexRender(cell.column.columnDef.cell, cell.getContext())}
                    </span>
                  </div>
                ))}

                {actionCells.length > 0 && (
                  <div className="flex items-center justify-end gap-1 pt-2 border-t border-divider/15">
                    {actionCells.map((cell) => (
                      <span key={cell.id}>
                        {flexRender(cell.column.columnDef.cell, cell.getContext())}
                      </span>
                    ))}
                  </div>
                )}
              </div>
            );
          })}
        </div>
      </div>

      {enablePagination && table.getPageCount() > 1 && (
        <div className="shrink-0 flex items-center justify-between pt-3 px-1 border-t border-divider/15">
          <p className="text-xs text-fg-subtle">
            {t('dataTable.pageInfo', {
              current: table.getState().pagination.pageIndex + 1,
              total: table.getPageCount(),
            })}
          </p>

          <div className="flex items-center gap-1">
            <IconButton
              onClick={() => table.previousPage()}
              disabled={!table.getCanPreviousPage()}
              label={t('dataTable.previousPage')}
              className="rounded-md text-fg-subtle hover:text-fg hover:bg-raised disabled:opacity-40 disabled:cursor-not-allowed disabled:hover:bg-transparent"
            >
              <ChevronLeft size={18} />
            </IconButton>

            <IconButton
              onClick={() => table.nextPage()}
              disabled={!table.getCanNextPage()}
              label={t('dataTable.nextPage')}
              className="rounded-md text-fg-subtle hover:text-fg hover:bg-raised disabled:opacity-40 disabled:cursor-not-allowed disabled:hover:bg-transparent"
            >
              <ChevronRight size={18} />
            </IconButton>
          </div>
        </div>
      )}
    </div>
  );
}

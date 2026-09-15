import {
  flexRender,
  getCoreRowModel,
  getPaginationRowModel,
  getSortedRowModel,
  useReactTable,
} from '@tanstack/react-table';
import { useTranslation } from 'react-i18next';

export function DataTable({
  columns,
  data,
  getRowId,
  isLoading = false,
  emptyMessage,
  enableSorting = false,
  enablePagination = false,
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
    <div>
      {/* Desktop/tablet table, md and up. */}
      <div className="hidden md:block relative overflow-x-auto mt-6 rounded-lg">
        <table className="w-full text-sm text-center text-fg-muted">
          <thead>
            {table.getHeaderGroups().map((headerGroup) => (
              <tr key={headerGroup.id} className="bg-surface border-b border-divider/30">
                {headerGroup.headers.map((header) => (
                  <th
                    key={header.id}
                    className="
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
                  <td key={cell.id} className="px-6 py-4">
                    {flexRender(cell.column.columnDef.cell, cell.getContext())}
                  </td>
                ))}
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {/* Stacked cards below md, where a horizontally-scrolled table would
          be hard to read and act on. Reuses the same column defs as the
          table above so the two views can't drift apart: data columns
          (accessorKey) render as label/value pairs, action columns (id
          only, no accessorKey) render together in a button row at the
          bottom of the card. */}
      <div className="md:hidden mt-6 space-y-3">
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
  );
}

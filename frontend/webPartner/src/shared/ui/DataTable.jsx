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

  const columnCount = table.getAllLeafColumns().length;

  return (
    <div className="relative overflow-x-auto mt-6 rounded-lg">
      {/* Desktop/tablet table view. A stacked-card view for small screens
          lands here (md:hidden) reusing the same column defs, see PR5. */}
      <table className="hidden md:table w-full text-sm text-center text-fg-muted">
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
          {isLoading ? (
            <tr>
              <td colSpan={columnCount} className="py-8 text-center text-fg-subtle">
                {t('notifications.loading')}
              </td>
            </tr>
          ) : table.getRowModel().rows.length === 0 ? (
            <tr>
              <td colSpan={columnCount} className="py-8 text-center text-fg-subtle">
                {emptyMessage}
              </td>
            </tr>
          ) : (
            table.getRowModel().rows.map((row) => (
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
            ))
          )}
        </tbody>
      </table>
    </div>
  );
}

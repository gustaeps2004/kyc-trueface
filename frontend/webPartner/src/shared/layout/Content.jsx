import { CirclePlus, FileSpreadsheet } from 'lucide-react';
import { InputFilter } from '@/shared/ui/InputFilter';
import { IconButton } from '@/shared/ui/IconButton';

export function Content(props) {
  return(
    <div className="
      bg-surface/60
      border
      border-divider/30
      h-full
      rounded-xl
      p-4
      flex
      flex-col
    ">
      <div className="
        shrink-0
        flex
        flex-col
        sm:flex-row
        items-start
        sm:items-center
        justify-between
        gap-2
        mb-2
      ">
        <div className="flex items-center gap-2">
          {
            props.isShowAdd
            ? <IconButton
                onClick={props.openModal}
                label="Add new"
                className="rounded-full text-brand hover:bg-brand/15"
              >
                <CirclePlus size={28} />
              </IconButton>
            : null
          }

          {
            props.isShowReport
            ? <IconButton
                onClick={props.openReportModal}
                label={props.reportLabel}
                className="rounded-full text-brand hover:bg-brand/15"
              >
                <FileSpreadsheet size={24} />
              </IconButton>
            : null
          }
        </div>
        <div className="flex justify-end w-full sm:w-auto sm:max-w-xs md:max-w-sm lg:max-w-md">
          { props.isShowFilter ? <InputFilter placeholder={props.placeholderFilter} value={props.filterValue} onChange={props.onFilter} /> : null}
        </div>
      </div>
      <div className="flex-1 min-h-0">
        {props.children}
      </div>
    </div>
  )
}

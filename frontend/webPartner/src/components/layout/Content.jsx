import { CirclePlus } from 'lucide-react';
import { InputFilter } from '@/components/ui/InputFilter';

export function Content(props) {
  return(
    <div className="
      bg-surface/60
      border
      border-divider/30
      h-full
      rounded-xl
      p-4
    ">
      <div className="
        flex
        flex-col
        sm:flex-row
        items-start
        sm:items-center
        justify-between
        gap-2
        mb-2
      ">
        <div>
          {
            props.isShowAdd
            ? <button
                onClick={props.openModal}
                aria-label="Add new"
                className="
                  flex
                  items-center
                  justify-center
                  text-brand
                  hover:bg-brand/15
                  rounded-full
                  p-1
                  transition-all
                  duration-150
                  cursor-pointer
                "
              >
                <CirclePlus size={28} />
              </button>
            : <div></div>
          }
        </div>
        <div className="flex justify-end w-full sm:w-auto sm:max-w-xs md:max-w-sm lg:max-w-md">
          { props.isShowFilter ? <InputFilter placeholder={props.placeholderFilter} value={props.filterValue} onChange={props.onFilter} /> : null}
        </div>
      </div>
      {props.children}
    </div>
  )
}

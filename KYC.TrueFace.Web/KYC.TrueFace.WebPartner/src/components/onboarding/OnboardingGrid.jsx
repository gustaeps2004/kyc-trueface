import { Eye, SquareCheck } from 'lucide-react';
import { ModalImages } from './ModalImages';
import { useState } from 'react';
import MamisMito from '../../../../../../../mamis_mito.webp';
import Gusta from '../../../../../../../gusta.png';
import { 
  IdNumberFormat, 
  DateFormat 
} from "../../utils/functions/Formats";

export function OnboardingGrid(props) {
  const [openModalImages, setOpenModalImages] = useState(false)
  const [onboardingData, setOnboardingData] = useState(null)
  const [openAnalysis, setAnalysis] = useState(false)

  const handlerOpenModalImagens = (onboarding) => {
    const response = [
      {
        linkImage: MamisMito,
        nameImage: 'mamis_mito.webp'
      },
      {
        linkImage: Gusta,
        nameImage: 'gusta.png'
      }
    ]

    setOnboardingData(response)
    setOpenModalImages(true)
  }

  const handlerOpenAnalysis = (onboarding) => {
    setAnalysis(true)
  }

  return(
    <div className="relative overflow-x-auto mt-10">
      <table className="w-full text-sm text-center text-body text-title">
        <thead className="text-sm ">
          <tr className='bg-secondary'>
            {
              props.columns.map((column, index) => (
                <th key={index} className="px-6 py-3 rounded-s-base font-medium">
                  {column}
                </th>
              ))
            }
          </tr>
        </thead>
          <tbody>
            {
              props.onboardings.map((onboarding, index) => (
                <tr key={index}>
                  <th className="px-6 py-4">
                    {IdNumberFormat(onboarding.idNumber)}
                  </th>
                  <th className="px-6 py-4">
                    {onboarding.name}
                  </th>
                  <th className="px-6 py-4">
                    {onboarding.reason}
                  </th>
                  { 
                    props.isHistory ?
                      <th className="px-6 py-4">
                        {DateFormat(onboarding.inclusionDate)}
                      </th>
                    : null
                  }
                  <th>
                    <button 
                      onClick={() => handlerOpenModalImagens(onboarding)}
                      className="
                        cursor-pointer  
                        text-slate-300 
                        hover:text-title 
                        transition
                        hover:scale-105
                        ml-3"
                    >
                      <Eye />
                    </button>
                  </th>
                  <th>
                    <button 
                      onClick={() => handlerOpenAnalysis(onboarding)}
                      className="
                        cursor-pointer  
                        text-slate-300 
                        hover:text-title 
                        transition
                        hover:scale-105
                        ml-3"
                    >
                      <SquareCheck />
                    </button>
                  </th>
                </tr>
              ))
            }
            </tbody>
            {/* <tfoot>
              <tr className="font-semibold text-heading">
                <th scope="row" className="px-6 py-3 text-base">Total</th>
                <td className="px-6 py-3">3</td>
                <td className="px-6 py-3">21,000</td>
              </tr>
          </tfoot> */}
      </table>

      {
        openModalImages
        ? <ModalImages closeModal={() => setOpenModalImages(false)} onboardingData={onboardingData}/>
        : null
      }
    </div>
  )
}
import { Eye, SquareCheck } from 'lucide-react';
import { ModalImages } from './ModalImages';
import { OnboardingAnalyse } from '../../pages/onboarding/OnboardingAnalyse';
import { OnboardingAnalysed } from '../../pages/history/OnboardingAnalysed';
import { useState } from 'react';
import { Situation } from '../../utils/Arrays';
import { 
  IdNumberFormat, 
  DateFormat 
} from "../../utils/functions/Formats";

export function OnboardingGrid(props) {
  const [openModalImages, setOpenModalImages] = useState(false)
  const [openModalAnalyse, setOpenModalAnalyse] = useState(false)
  const [onboardingData, setOnboardingData] = useState(null)

  const handlerOpenModalImagens = (onboarding) => {
    const response = [
      {
        linkImage: null,
        nameImage: 'mamis_mito.webp'
      },
      {
        linkImage: null,
        nameImage: 'gusta.png'
      }
    ]

    setOnboardingData(response)
    setOpenModalImages(true)
  }

  const handlerOpenAnalysis = (onboarding) => {
    setOnboardingData(onboarding)
    setOpenModalAnalyse(true)
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
                    { 
                      !props.isHistory ? onboarding.reason
                      : Situation.find(x => x.value == onboarding.situation)?.label 
                    }
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
        ? <ModalImages closeModal={() => setOpenModalImages(false)} onboardingData={onboardingData} />
        : null
      }

      {
        openModalAnalyse && !props.isHistory
        ? <OnboardingAnalyse closeModal={() => setOpenModalAnalyse(false) } onboardingData={onboardingData} />
        : openModalAnalyse && props.isHistory
        ? <OnboardingAnalysed closeModal={() => setOpenModalAnalyse(false) } onboardingData={onboardingData} />
        : null
      }
    </div>
  )
}
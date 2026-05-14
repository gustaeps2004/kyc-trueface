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

function SituationBadge({ situationValue }) {
  const situation = Situation.find(x => x.value == situationValue);
  if (!situation) return null;

  const label = situation.label?.toLowerCase() || "";

  const styles = label.includes("approv")
    ? "bg-success/15 text-success-light"
    : label.includes("denied") || label.includes("reproved")
    ? "bg-danger/15 text-danger-light"
    : "bg-warning/15 text-warning-light";

  return (
    <span className={`
      inline-block
      text-xs
      font-medium
      px-3
      py-1
      rounded-full
      ${styles}
    `}>
      {situation.label}
    </span>
  );
}

export function OnboardingGrid(props) {
  const [openModalImages, setOpenModalImages] = useState(false)
  const [openModalAnalyse, setOpenModalAnalyse] = useState(false)
  const [onboardingData, setOnboardingData] = useState(null)

  const handlerOpenModalImagens = () => {
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
    <div className="relative overflow-x-auto mt-6 rounded-lg">
      <table className="w-full text-sm text-center text-fg-muted">
        <thead>
          <tr className="bg-surface border-b border-divider/30">
            {props.columns.map((column, index) => (
              <th
                key={index}
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
                {column}
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          {props.onboardings.map((onboarding, index) => (
            <tr
              key={index}
              className="
                border-b
                border-divider/15
                transition-colors
                duration-150
                hover:bg-surface/50
              "
            >
              <td className="px-6 py-4 font-mono text-xs text-fg-muted">
                {IdNumberFormat(onboarding.idNumber)}
              </td>
              <td className="px-6 py-4 text-fg font-medium">
                {onboarding.name}
              </td>
              <td className="px-6 py-4">
                {!props.isHistory
                  ? <span className="text-warning-light">{onboarding.reason}</span>
                  : <SituationBadge situationValue={onboarding.situation} />
                }
              </td>
              {props.isHistory && (
                <td className="px-6 py-4 text-fg-muted">
                  {DateFormat(onboarding.inclusionDate)}
                </td>
              )}
              <td className="px-6 py-4">
                <button
                  onClick={() => handlerOpenModalImagens(onboarding)}
                  aria-label="View images"
                  className="
                    inline-flex
                    items-center
                    justify-center
                    text-fg-subtle
                    hover:text-accent-light
                    hover:bg-accent/10
                    rounded-md
                    p-1.5
                    transition-all
                    duration-150
                    cursor-pointer
                  "
                >
                  <Eye size={18} />
                </button>
              </td>
              <td className="px-6 py-4">
                <button
                  onClick={() => handlerOpenAnalysis(onboarding)}
                  aria-label="Open analysis"
                  className="
                    inline-flex
                    items-center
                    justify-center
                    text-fg-subtle
                    hover:text-brand-soft
                    hover:bg-brand/10
                    rounded-md
                    p-1.5
                    transition-all
                    duration-150
                    cursor-pointer
                  "
                >
                  <SquareCheck size={18} />
                </button>
              </td>
            </tr>
          ))}
        </tbody>
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

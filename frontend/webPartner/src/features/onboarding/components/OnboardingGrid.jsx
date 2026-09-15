import { Eye, SquareCheck } from 'lucide-react';
import { ModalImages } from './ModalImages';
import { OnboardingAnalyse } from './OnboardingAnalyse';
import { OnboardingAnalysed } from './OnboardingAnalysed';
import { DataTable } from '@/shared/ui/DataTable';
import { useMemo, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { Situation } from '@/shared/utils/arrays';
import { CanWrite } from '@/shared/utils/permissions';
import { SituationBadge } from '@/shared/ui/SituationBadge';
import {
  IdNumberFormat,
  DateFormat
} from "@/shared/utils/formats";

const actionButtonClass = `
  inline-flex
  items-center
  justify-center
  text-fg-subtle
  rounded-md
  p-1.5
  transition-all
  duration-150
  cursor-pointer
`;

export function OnboardingGrid(props) {
  const [openModalImages, setOpenModalImages] = useState(false)
  const [openModalAnalyse, setOpenModalAnalyse] = useState(false)
  const [onboardingData, setOnboardingData] = useState(null)
  const { t } = useTranslation();

  const canAnalyse = props.isHistory || CanWrite();
  const namespace = props.isHistory ? 'history' : 'onboarding';

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
    if (!canAnalyse) return

    setOnboardingData(onboarding)
    setOpenModalAnalyse(true)
  }

  const columns = useMemo(() => [
    {
      accessorKey: 'idNumber',
      header: t(`${namespace}.idNumber`),
      cell: ({ getValue }) => (
        <span className="font-mono text-fg-muted">{IdNumberFormat(getValue())}</span>
      ),
    },
    {
      accessorKey: 'name',
      header: t(`${namespace}.name`),
      cell: ({ getValue }) => (
        <span className="text-fg font-medium">{getValue()}</span>
      ),
    },
    props.isHistory
      ? {
          accessorKey: 'situation',
          header: t('history.situation'),
          cell: ({ getValue }) => (
            <SituationBadge situationValue={getValue()} array={Situation} />
          ),
        }
      : {
          accessorKey: 'reason',
          header: t('onboarding.reason'),
          cell: ({ getValue }) => (
            <span className="text-warning-light">{getValue()}</span>
          ),
        },
    props.isHistory && {
      accessorKey: 'inclusionDate',
      header: t('history.date'),
      cell: ({ getValue }) => (
        <span className="text-fg-muted">{DateFormat(getValue())}</span>
      ),
    },
    {
      id: 'viewImages',
      header: t(`${namespace}.viewImages`),
      cell: ({ row }) => (
        <button
          onClick={() => handlerOpenModalImagens(row.original)}
          aria-label={t('onboarding.viewImages')}
          className={`${actionButtonClass} hover:text-accent-light hover:bg-accent/10`}
        >
          <Eye size={18} />
        </button>
      ),
    },
    canAnalyse && {
      id: 'analyse',
      header: t(`${namespace}.analysis`),
      cell: ({ row }) => (
        <button
          onClick={() => handlerOpenAnalysis(row.original)}
          aria-label={t('onboarding.analysis')}
          className={`${actionButtonClass} hover:text-brand-soft hover:bg-brand/10`}
        >
          <SquareCheck size={18} />
        </button>
      ),
    },
  ].filter(Boolean), [props.isHistory, canAnalyse, namespace, t])

  return(
    <div>
      <DataTable
        columns={columns}
        data={props.onboardings}
        getRowId={(onboarding) => onboarding.code}
        emptyMessage={t(`${namespace}.noResults`)}
      />

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

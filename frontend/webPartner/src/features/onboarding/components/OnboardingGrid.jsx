import { Eye, SquareCheck } from 'lucide-react';
import { ModalImages } from './ModalImages';
import { OnboardingAnalyse } from './OnboardingAnalyse';
import { OnboardingAnalysed } from './OnboardingAnalysed';
import { DataTable } from '@/shared/ui/DataTable';
import { IconButton } from '@/shared/ui/IconButton';
import { useMemo, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { OnboardingSituation } from '@/shared/utils/arrays';
import { CanWrite } from '@/shared/utils/permissions';
import { SituationBadge } from '@/shared/ui/SituationBadge';
import {
  IdNumberFormat,
  DateFormat
} from "@/shared/utils/formats";

const actionButtonClass = "rounded-md text-fg-subtle";

export function OnboardingGrid(props) {
  const [imagesCode, setImagesCode] = useState(null)
  const [openModalAnalyse, setOpenModalAnalyse] = useState(false)
  const [onboardingData, setOnboardingData] = useState(null)
  const { t } = useTranslation();

  const canAnalyse = props.isHistory || CanWrite();
  const namespace = props.isHistory ? 'history' : 'onboarding';

  const handlerOpenModalImagens = (onboarding) => {
    setImagesCode(onboarding.code)
  }

  const handlerOpenAnalysis = (onboarding) => {
    if (!canAnalyse) return

    setOnboardingData(onboarding)
    setOpenModalAnalyse(true)
  }

  const handlerCloseAnalysis = () => {
    setOpenModalAnalyse(false)
    setOnboardingData(null)
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
            <SituationBadge situationValue={getValue()} array={OnboardingSituation} />
          ),
        }
      : {
          accessorKey: 'situationMessage',
          header: t('onboarding.reason'),
          cell: ({ getValue }) => (
            <span className="text-warning-light">{getValue()}</span>
          ),
        },
    props.isHistory && {
      accessorKey: 'situationDt',
      header: t('history.date'),
      cell: ({ getValue }) => (
        <span className="text-fg-muted">{DateFormat(getValue())}</span>
      ),
    },
    {
      id: 'viewImages',
      header: t(`${namespace}.viewImages`),
      cell: ({ row }) => (
        <IconButton
          onClick={() => handlerOpenModalImagens(row.original)}
          label={t('onboarding.viewImages')}
          className={`${actionButtonClass} hover:text-accent-light hover:bg-accent/10`}
        >
          <Eye size={18} />
        </IconButton>
      ),
    },
    canAnalyse && {
      id: 'analyse',
      header: t(`${namespace}.analysis`),
      cell: ({ row }) => (
        <IconButton
          onClick={() => handlerOpenAnalysis(row.original)}
          label={t('onboarding.analysis')}
          className={`${actionButtonClass} hover:text-brand-soft hover:bg-brand/10`}
        >
          <SquareCheck size={18} />
        </IconButton>
      ),
    },
  ].filter(Boolean), [props.isHistory, canAnalyse, namespace, t])

  return(
    <div className="h-full">
      <DataTable
        columns={columns}
        data={props.onboardings}
        getRowId={(onboarding) => onboarding.code}
        isLoading={props.isLoading}
        emptyMessage={t(`${namespace}.noResults`)}
        enablePagination={true}
      />

      {
        imagesCode
        ? <ModalImages closeModal={() => setImagesCode(null)} code={imagesCode} />
        : null
      }

      {
        openModalAnalyse && !props.isHistory
        ? <OnboardingAnalyse
            closeModal={handlerCloseAnalysis}
            onboardingData={onboardingData}
            onSuccess={props.onReviewed}
          />
        : openModalAnalyse && props.isHistory
        ? <OnboardingAnalysed closeModal={handlerCloseAnalysis} onboardingData={onboardingData} />
        : null
      }
    </div>
  )
}

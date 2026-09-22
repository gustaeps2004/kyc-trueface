import Layout from "@/shared/layout/Layout";
import { Content } from "@/shared/layout/Content";
import { OnboardingGrid } from "../components/OnboardingGrid";
import { OnboardingUpload } from "../components/OnboardingUpload";
import { useCallback, useEffect, useMemo, useState } from "react";
import { useTranslation } from 'react-i18next';
import { useApi } from "@/shared/hooks/useApi";
import { CanWrite } from "@/shared/utils/permissions";
import { onboardingService } from "../api/onboardingService";
import { matchesText } from "../utils/filter";

export function Onboarding() {
  const [onboardings, setOnboardings] = useState([])
  const [filterValue, setFilterValue] = useState("")
  const [openUploadModal, setOpenUploadModal] = useState(false)
  const { execute, isLoading } = useApi();
  const { t } = useTranslation();
  const canWrite = CanWrite();

  const handlerList = useCallback(async (signal) => {
    await execute(
      () => onboardingService.listPendingManualReview(signal),
      { onSuccess: (response) => setOnboardings(response.data) }
    );
  }, [execute]);

  useEffect(() => {
    const controller = new AbortController()
    handlerList(controller.signal)
    return () => controller.abort()
  }, [handlerList]);

  // The endpoint returns the partner's whole manual-review queue, so it is filtered here.
  const filtered = useMemo(
    () => onboardings.filter(o => matchesText(o, filterValue)),
    [onboardings, filterValue]
  );

  const handlerOpenUploadModal = () => {
    if (!canWrite) return

    setOpenUploadModal(true)
  }

  return(
    <div>
      <Layout name={t('onboarding.pageTitle')}>
        <Content
          placeholderFilter={t('onboarding.searchPlaceholder')}
          isShowFilter={true}
          isShowAdd={canWrite}
          isShowRefresh={true}
          onRefresh={() => handlerList()}
          refreshLabel={t('onboarding.refresh')}
          isRefreshing={isLoading}
          openModal={handlerOpenUploadModal}
          filterValue={filterValue}
          onFilter={setFilterValue}
        >
          <OnboardingGrid
            onboardings={filtered}
            isHistory={false}
            isLoading={isLoading}
            onReviewed={() => handlerList()}
          />
        </Content>
      </Layout>

      {
        openUploadModal
        ? <OnboardingUpload
            closeModal={() => setOpenUploadModal(false)}
            onSuccess={() => handlerList()}
          />
        : null
      }
    </div>
  )
}

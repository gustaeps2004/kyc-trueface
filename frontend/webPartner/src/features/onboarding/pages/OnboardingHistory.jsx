import Layout from "@/shared/layout/Layout";
import { Content } from "@/shared/layout/Content";
import { OnboardingGrid } from "../components/OnboardingGrid";
import { Select } from "@/shared/ui/Select";
import { useCallback, useEffect, useMemo, useState } from "react";
import { useTranslation } from 'react-i18next';
import { useApi } from "@/shared/hooks/useApi";
import { OnboardingSituationFilter } from "@/shared/utils/arrays";
import { onboardingService } from "../api/onboardingService";
import { matchesText } from "../utils/filter";

export function OnboardingHistory() {
  const [onboardings, setOnboardings] = useState([])
  const [filterValue, setFilterValue] = useState("")
  const [situation, setSituation] = useState(0)
  const { execute, isLoading } = useApi();
  const { t } = useTranslation();

  const handlerList = useCallback(async (signal) => {
    await execute(
      () => onboardingService.listReviewed(situation || null, signal),
      { onSuccess: (response) => setOnboardings(response.data) }
    );
  }, [execute, situation]);

  useEffect(() => {
    const controller = new AbortController()
    handlerList(controller.signal)
    return () => controller.abort()
  }, [handlerList]);

  const filtered = useMemo(
    () => onboardings.filter(o => matchesText(o, filterValue)),
    [onboardings, filterValue]
  );

  return(
    <Layout name={t('history.pageTitle')}>
      <Content
        placeholderFilter={t('history.searchPlaceholder')}
        isShowFilter={true}
        isShowRefresh={true}
        onRefresh={() => handlerList()}
        refreshLabel={t('history.refresh')}
        isRefreshing={isLoading}
        filterValue={filterValue}
        onFilter={setFilterValue}
        filterExtra={
          <Select
            placeholder={t('history.situationAll')}
            options={OnboardingSituationFilter}
            value={situation}
            onChange={setSituation}
          />
        }
      >
        <OnboardingGrid
          onboardings={filtered}
          isHistory={true}
          isLoading={isLoading}
        />
      </Content>
    </Layout>
  )
}

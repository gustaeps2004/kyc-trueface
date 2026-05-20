import Layout from "../../components/base/Layout";
import { Content } from "../../components/base/Content";
import { OnboardingGrid } from "../../components/onboarding/OnboardingGrid";
import { useTranslation } from 'react-i18next';

export function OnboardingHistory() {
  const { t } = useTranslation();

  const columns = [
    t('history.idNumber'),
    t('history.name'),
    t('history.situation'),
    t('history.date'),
    t('history.viewImages'),
    t('history.analysis'),
  ]

  const onboardings = [
    {
      code: "3d3b1f50-01df-4248-8eff-2ef575d6bbc5",
      idNumber: "11122233344",
      inclusionDate: "2026-04-02",
      name: "Gustavo Do Espirito Santo",
      situation: 2,
      observation: "Low resolution on self"
    },
    {
      code: "3d3b1f50-01df-4248-8eff-2ef575d6bbc2",
      idNumber: "55566677788",
      inclusionDate: "2026-05-02",
      name: "Gustavo Do Espirito Santo",
      situation: 1,
      observation: "Approved"
    }
  ]

  return(
    <Layout name={t('history.pageTitle')}>
      <Content
        placeholderFilter={t('history.searchPlaceholder')}
        isShowFilter={true}
      >
        <OnboardingGrid
          columns={columns}
          onboardings={onboardings}
          isHistory={true}
        />
      </Content>
    </Layout>
  )
}

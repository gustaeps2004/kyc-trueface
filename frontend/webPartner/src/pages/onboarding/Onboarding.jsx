import Layout from "@/components/layout/Layout";
import { Content } from "@/components/layout/Content";
import { OnboardingGrid } from "@/components/onboarding/OnboardingGrid";
import { useTranslation } from 'react-i18next';

export function Onboarding() {
  const { t } = useTranslation();

  const columns = [
    t('onboarding.idNumber'),
    t('onboarding.name'),
    t('onboarding.reason'),
    t('onboarding.viewImages'),
    t('onboarding.analysis'),
  ]

  const onboardings = [
    {
      code: "3d3b1f50-01df-4248-8eff-2ef575d6bbc5",
      idNumber: "11122233344",
      name: "Gustavo Do Espirito Santo",
      reason: "Invalid self"
    }
  ]

  return(
    <Layout name={t('onboarding.pageTitle')}>
      <Content
        placeholderFilter={t('onboarding.searchPlaceholder')}
        isShowFilter={true}
      >
        <OnboardingGrid
          columns={columns}
          onboardings={onboardings}
          isHistory={false}
        />
      </Content>
    </Layout>
  )
}

import Layout from "@/shared/layout/Layout";
import { Content } from "@/shared/layout/Content";
import { OnboardingGrid } from "../components/OnboardingGrid";
import { useTranslation } from 'react-i18next';

export function Onboarding() {
  const { t } = useTranslation();

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
          onboardings={onboardings}
          isHistory={false}
        />
      </Content>
    </Layout>
  )
}

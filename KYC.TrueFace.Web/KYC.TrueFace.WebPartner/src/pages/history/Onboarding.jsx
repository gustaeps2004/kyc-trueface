import Layout from "../../components/base/Layout";
import { Content } from "../../components/base/Content";
import { OnboardingGrid } from "../../components/onboarding/OnboardingGrid";

export function OnboardingHistory() {
  
  const columns = [
    "Id number",
    "Name",
    "Reason",
    "Date",
    "View images",
    "Analysis",
  ]

  const onboardings = [
    {
      code: "3d3b1f50-01df-4248-8eff-2ef575d6bbc5",
      idNumber: "11122233344",
      inclusionDate: "2026-04-02",
      name: "Gustavo Do Espirito Santo",
      reason: "Invalid self"
    }
  ]

  
  return(
    <Layout name="History">
      <Content 
        placeholderFilter="ID or name"
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
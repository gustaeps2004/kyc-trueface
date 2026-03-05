import Layout from "../../components/base/Layout";
import { Content } from "../../components/base/Content";

export function OnboardingHistory() {
  return(
    <Layout name="History">
      <Content 
        placeholderFilter="ID, name or e-mail"
        isShowFilter={true}
      >
        <h2>CONTENT</h2>
      </Content>
    </Layout>
  )
}
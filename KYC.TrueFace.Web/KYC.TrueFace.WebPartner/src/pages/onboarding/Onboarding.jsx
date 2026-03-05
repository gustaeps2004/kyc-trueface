import Layout from "../../components/base/Layout";
import { Content } from "../../components/base/Content";

export function Onboarding() {
  return(
    <Layout name="Onboarding">
      <Content 
        placeholderFilter="ID or name"
        isShowFilter={true}
      >
        <h2>CONTENT</h2>
      </Content>
    </Layout>
  )
}
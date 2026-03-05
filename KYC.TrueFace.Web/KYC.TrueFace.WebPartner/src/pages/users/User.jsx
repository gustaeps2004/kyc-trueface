import Layout from "../../components/base/Layout";
import { Content } from "../../components/base/Content";

export function User() {
  const handlerOpenModal = () => {
    //open modal add or edit
  }

  return(
    <Layout name="Users">
      <Content 
        placeholderFilter="ID, name or e-mail"
        isShowAdd={true}
        isShowFilter={true}
        openModal={handlerOpenModal}
      >
        <h2>CONTENT</h2>
      </Content>
    </Layout>
  )
}
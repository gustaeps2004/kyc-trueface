import Layout from "../../components/base/Layout";
import { Content } from "../../components/base/Content";
import { UserAddEdit } from "./UserAddEdit";
import { useState } from "react";

export function User() {
  const [openModal, setOpenModal] = useState(false)

  const handlerOpenModal = () => {
    setOpenModal(true)
  }

  const handlerCloseModal = () => {
    setOpenModal(false)
  }

  return(
    <div>
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

      { 
        openModal 
        ? <UserAddEdit closeModal={handlerCloseModal}/>
        : ""
      }
    </div>
    
  )
}
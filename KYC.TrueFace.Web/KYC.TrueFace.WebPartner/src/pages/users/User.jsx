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

  const columns = [
    "Id number",
    "Name",
    "E-mail",
    "Edit",
  ]

  return(
    <div>
      <Layout name="Users">
        <Content 
          placeholderFilter="ID, name or e-mail"
          isShowAdd={true}
          isShowFilter={true}
          openModal={handlerOpenModal}
        >
          
          <div className="relative overflow-x-auto">
            <table className="w-full text-sm text-left rtl:text-right text-body text-title">
              <thead className="text-sm text-body">
                <tr>
                  {
                    columns.map(column => (
                      <th scope="col" className="px-6 py-3 rounded-s-base font-medium">
                        {column}
                      </th>
                    ))
                  }
                </tr>
              </thead>
                <tbody>
                  <tr>
                    <th scope="row" className="px-6 py-4 font-medium text-heading whitespace-nowrap">
                      Apple MacBook Pro 17"
                    </th>
                    <td className="px-6 py-4">
                      1
                    </td>
                    <td className="px-6 py-4">
                      $2999
                    </td>
                  </tr>
                </tbody>
                  <tfoot>
                    <tr className="font-semibold text-heading">
                      <th scope="row" className="px-6 py-3 text-base">Total</th>
                      <td className="px-6 py-3">3</td>
                      <td className="px-6 py-3">21,000</td>
                    </tr>
                  </tfoot>
            </table>
          </div>

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
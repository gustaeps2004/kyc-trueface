import Layout from "../../components/base/Layout";
import { Content } from "../../components/base/Content";
import { UserAddEdit } from "./UserAddEdit";
import { useState } from "react";
import { UserRoundPen } from 'lucide-react';
import { IdNumberFormat } from "../../utils/functions/Formats";

export function User() {
  const [openModal, setOpenModal] = useState(false)

  const handlerOpenModal = () => {
    setOpenModal(true)
  }

  const handlerCloseModal = () => {
    setOpenModal(false)
  }

  const columns = [
    "Name",
    "Id number",
    "E-mail",
    "Inclusion date",
    "Edit",
  ]

  const users = [
    {
      name: "Gustavo Do Espirito Santo",
      idNumber: "11122233344",
      email: "gustavo.santo@teste.com.br",
      inclusionDate: "30/03/2024",
    }
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
          
          <div className="relative overflow-x-auto mt-10">
            <table className="w-full text-sm text-center text-body text-title">
              <thead className="text-sm ">
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
                  {
                    users.map(user => (
                      <tr>
                        <th className="px-6 py-4">
                          {user.name}
                        </th>
                        <th className="px-6 py-4">
                          {IdNumberFormat(user.idNumber)}
                        </th>
                        <th className="px-6 py-4">
                          {user.email}
                        </th>
                        <th className="px-6 py-4">
                          {user.inclusionDate}
                        </th>
                        <th>
                          <button 
                            className="
                              cursor-pointer  
                              text-slate-300 
                              hover:text-title 
                              transition
                              hover:scale-105
                              ml-3"
                          >
                            <UserRoundPen  />
                          </button>
                        </th>
                      </tr>
                    ))
                  }
                 </tbody>
                 {/* <tfoot>
                    <tr className="font-semibold text-heading">
                      <th scope="row" className="px-6 py-3 text-base">Total</th>
                      <td className="px-6 py-3">3</td>
                      <td className="px-6 py-3">21,000</td>
                    </tr>
                </tfoot> */}
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
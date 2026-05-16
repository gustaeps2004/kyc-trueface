import Layout from "../../components/base/Layout";
import { Content } from "../../components/base/Content";
import { UserAddEdit } from "./UserAddEdit";
import { useState } from "react";
import { UserRoundPen } from 'lucide-react';
import {
  IdNumberFormat,
  DateFormat
} from "../../utils/functions/Formats";

export function User() {
  const [openModal, setOpenModal] = useState(false)
  const [isEdit, setIsEdit] = useState(false)
  const [userEdit, setUserEdit] = useState(null)

  const handlerOpenModal = (isEdit, user) => {
    setIsEdit(isEdit)
    setOpenModal(true)

    if (user) setUserEdit(user)
  }

  const handlerCloseModal = () => {
    setOpenModal(false)
    setUserEdit(null)
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
      inclusionDate: "2026-03-30",
      motherName: "Marlene dal pra",
      permission: 2,
      birthDate: "2004-08-18",
      code: "3d3b1f50-01df-4248-8eff-2ef575d6bbc5"
    }
  ]

  return(
    <div>
      <Layout name="Users">
        <Content
          placeholderFilter="ID, name or e-mail"
          isShowAdd={true}
          isShowFilter={true}
          openModal={() => handlerOpenModal(false, null)}
        >

          <div className="relative overflow-x-auto mt-6 rounded-lg">
            <table className="w-full text-sm text-center text-fg-muted">
              <thead>
                <tr className="bg-surface border-b border-divider/30">
                  {columns.map((column, index) => (
                    <th
                      key={index}
                      className="
                        px-6
                        py-3
                        text-xs
                        font-medium
                        text-fg-subtle
                        uppercase
                        tracking-wide
                      "
                    >
                      {column}
                    </th>
                  ))}
                </tr>
              </thead>
              <tbody>
                {users.map((user, index) => (
                  <tr
                    key={index}
                    className="
                      border-b
                      border-divider/15
                      transition-colors
                      duration-150
                      hover:bg-surface/50"
                  >
                    <td className="px-6 py-4 font-mono text-fg-muted">
                      {IdNumberFormat(user.idNumber)}
                    </td>
                    <td className="px-6 py-4 text-fg font-medium">
                      {user.name}
                    </td>
                    <td className="px-6 py-4 text-fg-muted">
                      {user.email}
                    </td>
                    <td className="px-6 py-4 text-fg-muted">
                      {DateFormat(user.inclusionDate)}
                    </td>
                    <td className="px-6 py-4">
                      <button
                        onClick={() => handlerOpenModal(true, user)}
                        aria-label="Edit user"
                        className="
                          inline-flex
                          items-center
                          justify-center
                          text-fg-subtle
                          hover:text-brand-soft
                          hover:bg-brand/10
                          rounded-md
                          p-1.5
                          transition-all
                          duration-150
                          cursor-pointer
                        "
                      >
                        <UserRoundPen size={18} />
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

        </Content>
      </Layout>

      {
        openModal
        ? <UserAddEdit closeModal={handlerCloseModal} userEdit={userEdit} isEdit={isEdit}/>
        : null
      }
    </div>
  )
}

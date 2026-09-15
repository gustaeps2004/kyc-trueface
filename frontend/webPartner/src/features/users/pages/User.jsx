import Layout from "@/shared/layout/Layout";
import { Content } from "@/shared/layout/Content";
import { UserAddEdit } from "../components/UserAddEdit";
import { UserReport } from "../components/UserReport";
import { useState, useEffect } from "react";
import { useTranslation } from 'react-i18next';
import { UserRoundPen } from 'lucide-react';
import { UserSituation } from "@/shared/utils/arrays";
import { SituationBadge } from "@/shared/ui/SituationBadge";
import { userService } from "../api/userService";
import { useApi } from "@/shared/hooks/useApi";
import { CanWrite } from "@/shared/utils/permissions";
import {
  IdNumberFormat,
  DateFormat
} from "@/shared/utils/formats";

export function User() {
  const [openModal, setOpenModal] = useState(false)
  const [openReportModal, setOpenReportModal] = useState(false)
  const [isEdit, setIsEdit] = useState(false)
  const [userEdit, setUserEdit] = useState(null)
  const [users, setUsers] = useState([])
  const [filterValue, setFilterValue] = useState("")
  const { execute, isLoading } = useApi();
  const { t } = useTranslation();
  const canWrite = CanWrite();

  useEffect(() => {
    const controller = new AbortController()
    const timer = setTimeout(() => {
      handlerListUsers(filterValue, controller.signal)
    }, 400)
    return () => {
      clearTimeout(timer)
      controller.abort()
    }
  }, [filterValue]);

  const handlerListUsers = async (filter, signal) => {
    await execute(
      () => userService.listByPartner(filter, signal),
      {
        onSuccess: (response) => {
          setUsers(response.data.map(u => ({
            ...u,
            inclusionDate: u.inclusionDt,
          })));
        },
      }
    );
  }

  const handlerOpenModal = (isEdit, user) => {
    if (!canWrite) return

    setIsEdit(isEdit)
    setOpenModal(true)

    if (user) setUserEdit(user)
  }

  const handlerCloseModal = () => {
    setOpenModal(false)
    setUserEdit(null)
  }

  const handlerOpenReportModal = () => {
    if (!canWrite) return

    setOpenReportModal(true)
  }

  const columns = [
    t('users.gridColumns.idNumber'),
    t('users.gridColumns.name'),
    t('users.gridColumns.email'),
    t('users.gridColumns.situation'),
    t('users.gridColumns.inclusionDate'),
    t('users.gridColumns.edit')
  ]

  return(
    <div>
      <Layout name={t('users.pageTitle')}>
        <Content
          placeholderFilter={t('users.searchPlaceholder')}
          isShowAdd={canWrite}
          isShowFilter={true}
          openModal={() => handlerOpenModal(false, null)}
          isShowReport={canWrite}
          openReportModal={() => handlerOpenReportModal()}
          reportLabel={t('users.report.button')}
          filterValue={filterValue}
          onFilter={setFilterValue}
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
                {isLoading ? (
                  <tr>
                    <td colSpan={columns.length} className="py-8 text-center text-fg-subtle">
                      {t('notifications.loading')}
                    </td>
                  </tr>
                ) : (
                  users.map((user) => (
                  <tr
                    key={user.code}
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
                      <SituationBadge array={UserSituation} situationValue={user.situation} isUser={true} />
                    </td>
                    <td className="px-6 py-4 text-fg-muted">
                      {DateFormat(user.inclusionDate)}
                    </td>
                    <td className="px-6 py-4">
                      {canWrite && (
                        <button
                          onClick={() => handlerOpenModal(true, user)}
                          aria-label={t('users.edit')}
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
                      )}
                    </td>
                  </tr>
                ))
                )}
              </tbody>
            </table>
          </div>

        </Content>
      </Layout>

      {
        openModal
        ? <UserAddEdit closeModal={handlerCloseModal} userEdit={userEdit} isEdit={isEdit} onSuccess={() => handlerListUsers(filterValue)}/>
        : null
      }

      {
        openReportModal
        ? <UserReport closeModal={() => setOpenReportModal(false)} filterValue={filterValue} />
        : null
      }
    </div>
  )
}

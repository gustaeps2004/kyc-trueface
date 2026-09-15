import Layout from "@/shared/layout/Layout";
import { Content } from "@/shared/layout/Content";
import { DataTable } from "@/shared/ui/DataTable";
import { UserAddEdit } from "../components/UserAddEdit";
import { UserReport } from "../components/UserReport";
import { useState, useEffect, useMemo } from "react";
import { useTranslation } from 'react-i18next';
import { UserRoundPen } from 'lucide-react';
import { UserSituation } from "@/shared/utils/arrays";
import { SituationBadge } from "@/shared/ui/SituationBadge";
import { IconButton } from "@/shared/ui/IconButton";
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

  const columns = useMemo(() => [
    {
      accessorKey: 'idNumber',
      header: t('users.gridColumns.idNumber'),
      cell: ({ getValue }) => (
        <span className="font-mono text-fg-muted">{IdNumberFormat(getValue())}</span>
      ),
    },
    {
      accessorKey: 'name',
      header: t('users.gridColumns.name'),
      cell: ({ getValue }) => (
        <span className="text-fg font-medium">{getValue()}</span>
      ),
    },
    {
      accessorKey: 'email',
      header: t('users.gridColumns.email'),
      cell: ({ getValue }) => (
        <span className="text-fg-muted">{getValue()}</span>
      ),
    },
    {
      accessorKey: 'situation',
      header: t('users.gridColumns.situation'),
      cell: ({ getValue }) => (
        <SituationBadge array={UserSituation} situationValue={getValue()} isUser={true} />
      ),
    },
    {
      accessorKey: 'inclusionDate',
      header: t('users.gridColumns.inclusionDate'),
      cell: ({ getValue }) => (
        <span className="text-fg-muted">{DateFormat(getValue())}</span>
      ),
    },
    canWrite && {
      id: 'edit',
      header: t('users.gridColumns.edit'),
      cell: ({ row }) => (
        <IconButton
          onClick={() => handlerOpenModal(true, row.original)}
          label={t('users.edit')}
          className="rounded-md text-fg-subtle hover:text-brand-soft hover:bg-brand/10"
        >
          <UserRoundPen size={18} />
        </IconButton>
      ),
    },
  ].filter(Boolean), [canWrite, t])

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

          <DataTable
            columns={columns}
            data={users}
            getRowId={(user) => user.code}
            isLoading={isLoading}
            emptyMessage={t('users.noResults')}
            enablePagination={true}
          />

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

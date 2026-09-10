import { Modal } from "../../components/modal/Modal"
import { Input } from "@/components/ui/Input";
import { Select } from "@/components/ui/Select";
import { useState } from "react";
import { useTranslation } from 'react-i18next';
import { UserSituationFilter } from "@/utils/arrays";
import { userService } from "@/api/services/userService";
import { useApi } from "@/hooks/useApi";
import { useNotification } from "@/context/NotificationContext";
import { CanWrite } from "@/utils/permissions";
import { ConvertDateToIso } from "@/utils/formats";

export function UserReport(props) {
  const { execute, isLoading } = useApi();
  const { notify } = useNotification();
  const [filter, setFilter] = useState(props.filterValue ?? "")
  const [situation, setSituation] = useState(0)
  const [startDate, setStartDate] = useState("")
  const [endDate, setEndDate] = useState("")
  const { t } = useTranslation();

  if (!CanWrite()) return null;

  const isDateIncomplete = (value) => value.length > 0 && value.length !== 10

  const handlerRequest = async (e) => {
    if (e && e.preventDefault)
      e.preventDefault();

    if (isDateIncomplete(startDate) || isDateIncomplete(endDate)) {
      notify({ message: t('users.report.validation.dateIncomplete'), type: 'warning' });
      return
    }

    const request = {
      filter: filter || null,
      situation: situation ? situation : null,
      startDt: ConvertDateToIso(startDate),
      endDt: ConvertDateToIso(endDate)
    }

    await execute(
      () => userService.requestReport(request),
      {
        onSuccess: () => props.closeModal(),
        showSuccessPopup: true,
        successMessage: t('users.report.requested')
      }
    );
  }

  return(
    <Modal
      title={t('users.report.title')}
      closeModal={props.closeModal}
      showGreenButton={true}
      titleGreenButton={t('users.report.generate')}
      handlerGreenAction={(e) => handlerRequest(e)}
      greenVariant="brand"
      disabled={isLoading}
    >
      <Input type="text" name="reportFilter" value={filter} onChange={setFilter}>
        {t('users.report.filter')}
      </Input>

      <Select
        placeholder={t('users.report.situation')}
        options={UserSituationFilter}
        value={situation}
        onChange={setSituation}
      />

      <Input
        type="text"
        name="startDate"
        value={startDate}
        mask="##/##/####"
        onChange={setStartDate}
      >
        {t('users.report.startDate')}
      </Input>

      <Input
        type="text"
        name="endDate"
        value={endDate}
        mask="##/##/####"
        onChange={setEndDate}
      >
        {t('users.report.endDate')}
      </Input>
    </Modal>
  )
}

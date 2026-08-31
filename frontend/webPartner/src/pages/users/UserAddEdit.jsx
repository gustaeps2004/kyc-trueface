import { Modal } from "../../components/modal/Modal"
import { Input } from "@/components/ui/Input";
import { Select } from "@/components/ui/Select";
import { useState, useEffect } from "react";
import { useTranslation } from 'react-i18next';
import { Permission, UserSituation } from "@/utils/arrays";
import { userService } from "@/api/services/userService";
import { useApi } from "@/hooks/useApi";
import {
  IdNumberFormat,
  DateFormat,
  ConvertDate
} from "@/utils/formats";

export function UserAddEdit(props) {
  const { execute, isLoading } = useApi();
  const [permission, setPermission] = useState()
  const [situation, setSituation] = useState()
  const [name, setName] = useState("")
  const [motherName, setMotherName] = useState("")
  const [email, setEmail] = useState("")
  const [idNumber, setIdNumber] = useState("")
  const [bithDate, setBithDate] = useState("")
  const [code, setCode] = useState("")
  const { t } = useTranslation();

  useEffect(() => {
    if (!props.isEdit) return
    
    setCode(props.userEdit.code)
    setName(props.userEdit.name)
    setMotherName(props.userEdit.motherName)
    setEmail(props.userEdit.email)
    setPermission(props.userEdit.permission)
    setSituation(props.userEdit.situation)
    setIdNumber(IdNumberFormat(props.userEdit.idNumber))
    setBithDate(DateFormat(props.userEdit.birthDate))
  }, []);

  const handlerCleanFields = () => {
    setCode("")
    setName("")
    setMotherName("")
    setEmail("")
    setPermission()
    setSituation()
    setIdNumber("")
    setBithDate("")
  }

  const handlerPersist = async (e) => {
    if (e && e.preventDefault)
      e.preventDefault();

    const request = {
      name,
      motherName,
      email,
      permission,
      situation,
      idNumber,
      bithDate: ConvertDate(bithDate),
      code
    }
    
    await execute(
      () => props.isEdit ? userService.update(request) : userService.insert(request),
      {
        onSuccess: (response) => {
          handlerCleanFields()
          props.closeModal()
          props.onSuccess()
        },
        showSuccessPopup: true
      }
    );
  }

  return(
    <Modal
      title={props.isEdit ? t('users.editUser') : t('users.createUser')}
      closeModal={props.closeModal}
      showGreenButton={true}
      titleGreenButton={props.isEdit ? t('users.save') : t('users.create')}
      handlerGreenAction={(e) => handlerPersist(e)}
      greenVariant="brand"
      disabled={isLoading}
    >
      <Input type="name" name="name" value={name} onChange={setName}>
        {t('users.modal.name')}
      </Input>
      <Input
        disabled={props.isEdit}
        type="idNumber"
        name="idNumber"
        value={idNumber}
        mask="###.###.###-##"
        onChange={setIdNumber}
      >
        {t('users.modal.idNumber')}
      </Input>
      <Input
        type="bith"
        name="bithDate"
        value={bithDate}
        mask="##/##/####"
        onChange={setBithDate}
      >
        {t('users.modal.birthDate')}
      </Input>
      <Input type="motherName" name="motherName" value={motherName} onChange={setMotherName}>
        {t('users.modal.motherName')}
      </Input>
      <Input disabled={props.isEdit} type="email" name="email" value={email} onChange={setEmail}>
        {t('users.modal.email')}
      </Input>

      { props.isEdit 
        ? <Select
            placeholder={t('users.modal.situation')}
            options={UserSituation}
            value={situation}
            onChange={setSituation}
          /> 
        : null
      }

      <Select
        placeholder={t('users.modal.permission')}
        options={Permission}
        value={permission}
        onChange={setPermission}
      />
    </Modal>
  )
}

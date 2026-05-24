import { Modal } from "../../components/modal/Modal"
import { Input } from "@/components/ui/Input";
import { Select } from "@/components/ui/Select";
import { useState, useEffect } from "react";
import { useTranslation } from 'react-i18next';
import { Permission } from "@/utils/arrays";
import {
  IdNumberFormat,
  DateFormat
} from "@/utils/formats";

export function UserAddEdit(props) {
  const [permission, setPermission] = useState()
  const [idNumber, setIdNumber] = useState("")
  const [bithDate, setBithDate] = useState("")
  const { t } = useTranslation();

  useEffect(() => {
    if (!props.isEdit) return

    document.getElementById('motherName').value = props.userEdit.motherName
    document.getElementById('email').value = props.userEdit.email
    document.getElementById('name').value = props.userEdit.name
    setPermission(props.userEdit.permission)
    setIdNumber(IdNumberFormat(props.userEdit.idNumber))
    setBithDate(DateFormat(props.userEdit.birthDate))
  }, []);

  const handlerCreate = () => {
    const dto = {
      motherName: document.getElementById('motherName').value,
      email: document.getElementById('email').value,
      name: document.getElementById('name').value,
      permission: permission,
      idNumber: idNumber,
      bithDate: bithDate,
    }

    console.log(dto)
  }

  return(
    <Modal
      title={props.isEdit ? t('users.editUser') : t('users.createUser')}
      closeModal={props.closeModal}
      showGreenButton={true}
      titleGreenButton={props.isEdit ? t('users.save') : t('users.create')}
      handlerGreenAction={handlerCreate}
      greenVariant="brand"
    >
      <Input type="name" name="name">
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
      <Input type="motherName" name="motherName">
        {t('users.modal.motherName')}
      </Input>
      <Input disabled={props.isEdit} type="email" name="email">
        {t('users.modal.email')}
      </Input>

      <Select
        placeholder={t('users.modal.permission')}
        options={Permission}
        value={permission}
        onChange={setPermission}
      />
    </Modal>
  )
}

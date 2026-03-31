import { Modal } from "../../components/modal/Modal"
import { Input } from "../../components/Input";
import { Select } from "../../components/Select"; 
import { useState, useEffect } from "react";
import { Permission } from "../../utils/Arrays";
import { 
  IdNumberFormat, 
  DateFormat 
} from "../../utils/functions/Formats";

export function UserAddEdit(props) {
  const [permission, setPermission] = useState()
  const [idNumber, setIdNumber] = useState("")
  const [bithDate, setBithDate] = useState("")

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
      title={(props.isEdit ? "Edit" : "Create") + " user"}
      closeModal={props.closeModal}
      showGreenButton={true}
      titleGreenButton="Create"
      handlerGreenAction={handlerCreate}
    >
      <Input type="name" name="name">
        Name
      </Input>
      <Input 
        disabled={props.isEdit}
        type="idNumber" 
        name="idNumber" 
        value={idNumber}
        mask="###.###.###-##"
        onChange={setIdNumber}
      >
        Id number
      </Input>
      <Input 
        type="bith" 
        name="bithDate"
        value={bithDate}
        mask="##/##/####"
        onChange={setBithDate}
      >
        Birth date
      </Input>
      <Input type="motherName" name="motherName">
        Mother name
      </Input>
      <Input disabled={props.isEdit} type="email" name="email">
        E-mail
      </Input>

      <Select 
        placeholder="Permission"
        options={Permission}
        value={permission}
        onChange={setPermission}
      />
    </Modal>
  )
}
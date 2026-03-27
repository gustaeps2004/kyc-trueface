import { Modal } from "../../components/modal/Modal"
import { Input } from "../../components/Input";
import { Select } from "../../components/Select"; 
import { useState } from "react";
import { Permission } from "../../utils/Arrays";

export function UserAddEdit(props) {
  const [permission, setPermission] = useState()
  const [idNumber, setIdNumber] = useState("")
  const [bithDate, setBithDate] = useState("")

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
      title="Create user"
      closeModal={props.closeModal}
      showGreenButton={true}
      titleGreenButton="Create"
      handlerGreenAction={handlerCreate}
    >
      <Input type="name" name="name">
        Name
      </Input>
      <Input 
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
      <Input type="email" name="email">
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
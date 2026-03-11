import { Modal } from "../../components/modal/Modal"
import { Input } from "../../components/Input";
import { Select } from "../../components/Select"; 
import { useState } from "react";
import { Permission } from "../../utils/Arrays";

export function UserAddEdit(props) {
  const [permission, setPermission] = useState()

  const handlerCreate = () => {
    console.log("create user method")
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
      <Input type="idNumber" name="idNumber">
        Id number
      </Input>
      <Input type="bith" name="bith">
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
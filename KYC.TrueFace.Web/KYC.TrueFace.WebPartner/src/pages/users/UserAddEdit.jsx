import { ModalSteps } from "../../components/modal/ModalSteps"

import { Input } from "../../components/Input";

export function UserAddEdit(props) {

  const handlerCreate = () => {
    console.log("create user method")
  }

  return(
    <ModalSteps 
      title="Create user"
      closeModal={props.closeModal}
      titleButton="Create"
      handlerAction={handlerCreate}
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



    </ModalSteps>
  )
}
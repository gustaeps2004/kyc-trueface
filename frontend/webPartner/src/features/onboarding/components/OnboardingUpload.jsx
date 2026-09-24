import { useEffect, useRef, useState } from "react";
import { useTranslation } from 'react-i18next';
import { FileText, ImageUp, X } from 'lucide-react';
import { Modal } from "@/shared/modal/Modal";
import { Input } from "@/shared/ui/Input";
import { IconButton } from "@/shared/ui/IconButton";
import { useApi } from "@/shared/hooks/useApi";
import { useNotification } from "@/shared/context/NotificationContext";
import { CanWrite } from "@/shared/utils/permissions";
import { onboardingService } from "../api/onboardingService";
import { PDF_TYPE } from "../utils/pdfToImage";

const ACCEPTED_TYPES = ["image/jpeg", "image/png"];
// The document may also be a PDF (e.g. the CNH-e); the API stores it as uploaded.
const DOCUMENT_ACCEPTED_TYPES = [...ACCEPTED_TYPES, PDF_TYPE];
const MAX_SIZE_BYTES = 5 * 1024 * 1024;

function ImagePicker({ label, accept = ACCEPTED_TYPES, file, onSelect, onClear }) {
  const inputRef = useRef(null);
  const [preview, setPreview] = useState(null);
  const isPdf = file?.type === PDF_TYPE;

  useEffect(() => {
    if (!file || file.type === PDF_TYPE) {
      setPreview(null);
      return;
    }

    const url = URL.createObjectURL(file);
    setPreview(url);

    return () => URL.revokeObjectURL(url);
  }, [file]);

  return (
    <div>
      <label className="block text-xs text-fg-subtle font-medium mb-1.5">
        {label}
      </label>

      <input
        ref={inputRef}
        type="file"
        accept={accept.join(",")}
        className="hidden"
        onChange={(e) => {
          onSelect(e.target.files?.[0] ?? null);
          e.target.value = "";
        }}
      />

      {file ? (
        <div className="
          flex
          items-center
          gap-3
          rounded-lg
          border
          border-divider/60
          bg-surface
          p-2
        ">
          {isPdf ? (
            <div className="flex h-14 w-14 shrink-0 items-center justify-center rounded-md border border-divider/30 text-fg-subtle">
              <FileText size={24} />
            </div>
          ) : (
            <img
              src={preview}
              alt={file.name}
              className="h-14 w-14 shrink-0 rounded-md object-cover border border-divider/30"
            />
          )}
          <span className="flex-1 min-w-0 truncate text-sm text-fg-muted">
            {file.name}
          </span>
          <IconButton
            onClick={onClear}
            label={label}
            className="h-8 w-8 rounded-full text-fg-subtle hover:text-fg hover:bg-raised"
          >
            <X size={16} />
          </IconButton>
        </div>
      ) : (
        <button
          type="button"
          onClick={() => inputRef.current?.click()}
          className="
            flex
            w-full
            items-center
            justify-center
            gap-2
            rounded-lg
            border
            border-dashed
            border-divider/60
            bg-surface
            px-4
            py-5
            text-sm
            text-fg-subtle
            cursor-pointer
            transition-all
            duration-200
            hover:border-brand/50
            hover:text-fg
            focus:outline-none
            focus:border-brand
            focus:ring-2
            focus:ring-brand/30
          "
        >
          <ImageUp size={18} />
          {label}
        </button>
      )}
    </div>
  );
}

export function OnboardingUpload(props) {
  const { execute, isLoading } = useApi();
  const { notify } = useNotification();
  const { t } = useTranslation();
  const [idNumber, setIdNumber] = useState("");
  const [name, setName] = useState("");
  const [document, setDocument] = useState(null);
  const [selfie, setSelfie] = useState(null);

  if (!CanWrite()) return null;

  const isFileInvalid = (file, acceptedTypes, typeMessage) => {
    if (!acceptedTypes.includes(file.type)) {
      notify({ message: t(typeMessage), type: 'warning' });
      return true;
    }

    if (file.size > MAX_SIZE_BYTES) {
      notify({ message: t('onboarding.validation.imageTooLarge'), type: 'warning' });
      return true;
    }

    return false;
  }

  const handlerSelect = (setter, acceptedTypes, typeMessage) => (file) => {
    if (file && isFileInvalid(file, acceptedTypes, typeMessage)) return;

    setter(file);
  }

  const handlerUpload = async (e) => {
    if (e && e.preventDefault)
      e.preventDefault();

    if (!name.trim()) {
      notify({ message: t('onboarding.validation.nameEmpty'), type: 'warning' });
      return
    }

    if (idNumber.length !== 14) {
      notify({ message: t('onboarding.validation.idNumber'), type: 'warning' });
      return
    }

    if (!document || !selfie) {
      notify({
        message: t(!document ? 'onboarding.validation.documentRequired' : 'onboarding.validation.selfieRequired'),
        type: 'warning'
      });
      return
    }

    const formData = new FormData();
    formData.append('idNumber', idNumber);
    formData.append('name', name.trim());
    formData.append('document', document);
    formData.append('selfie', selfie);

    await execute(
      () => onboardingService.upload(formData),
      {
        onSuccess: () => {
          props.onSuccess?.();
          props.closeModal();
        },
        showSuccessPopup: true,
        successMessage: t('onboarding.upload.queued')
      }
    );
  }

  return (
    <Modal
      title={t('onboarding.upload.title')}
      closeModal={props.closeModal}
      showGreenButton={true}
      titleGreenButton={t('onboarding.upload.send')}
      handlerGreenAction={(e) => handlerUpload(e)}
      greenVariant="brand"
      disabled={isLoading}
    >
      <Input type="text" name="onboardingName" value={name} onChange={setName}>
        {t('onboarding.name')}
      </Input>

      <Input
        type="text"
        name="onboardingIdNumber"
        value={idNumber}
        mask="###.###.###-##"
        onChange={setIdNumber}
      >
        {t('onboarding.idNumber')}
      </Input>

      <ImagePicker
        label={t('onboarding.upload.document')}
        accept={DOCUMENT_ACCEPTED_TYPES}
        file={document}
        onSelect={handlerSelect(setDocument, DOCUMENT_ACCEPTED_TYPES, 'onboarding.validation.documentContentType')}
        onClear={() => setDocument(null)}
      />

      <ImagePicker
        label={t('onboarding.upload.selfie')}
        file={selfie}
        onSelect={handlerSelect(setSelfie, ACCEPTED_TYPES, 'onboarding.validation.imageContentType')}
        onClear={() => setSelfie(null)}
      />

      <p className="text-xs text-fg-faint">
        {t('onboarding.upload.hint')}
      </p>
    </Modal>
  )
}

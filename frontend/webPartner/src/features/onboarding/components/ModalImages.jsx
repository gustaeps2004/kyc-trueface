import { useEffect, useState } from "react";
import {
  MoveRight,
  RotateCw,
  Download,
  X,
} from 'lucide-react';
import { useTranslation } from 'react-i18next';
import { IconButton } from "@/shared/ui/IconButton";
import { useApi } from "@/shared/hooks/useApi";
import { onboardingService } from "../api/onboardingService";
import { pdfToImage, PDF_TYPE } from "../utils/pdfToImage";

const KINDS = ['document', 'selfie'];

const EXTENSIONS = {
  'image/jpeg': '.jpg',
  'image/png': '.png',
  [PDF_TYPE]: '.pdf',
};

export function ModalImages(props) {
  const { execute } = useApi();
  const { t } = useTranslation();
  const [images, setImages] = useState([]);
  const [index, setIndex] = useState(0);
  const [rotation, setRotation] = useState(0);

  useEffect(() => {
    const handleEsc = (e) => {
      if (e.key === "Escape") props.closeModal();
    };

    window.addEventListener("keydown", handleEsc);

    return () => window.removeEventListener("keydown", handleEsc);
  }, [props.closeModal]);

  useEffect(() => {
    const controller = new AbortController();
    const urls = [];

    const track = (blob) => {
      const url = URL.createObjectURL(blob);
      urls.push(url);
      return url;
    };

    (async () => {
      const responses = await Promise.all(
        KINDS.map(kind =>
          execute(() => onboardingService.getImage(props.code, kind, controller.signal))
        )
      );

      const loaded = await Promise.all(
        responses.map(async (response, i) => {
          if (!response) return null;

          const file = response.data;
          const isPdf = file.type === PDF_TYPE;
          const fileUrl = track(file);

          return {
            kind: KINDS[i],
            isPdf,
            // A PDF document is kept as uploaded: show its first page, download the original.
            url: isPdf ? await pdfToImage(file).then(track).catch(() => null) : fileUrl,
            fileUrl,
            fileName: `${props.code}-${KINDS[i]}${EXTENSIONS[file.type] ?? ''}`,
          };
        })
      );

      if (controller.signal.aborted) {
        urls.forEach(url => URL.revokeObjectURL(url));
        return;
      }

      setImages(loaded.filter(Boolean));
    })();

    return () => {
      controller.abort();
      urls.forEach(url => URL.revokeObjectURL(url));
    };
  }, [props.code, execute]);

  const current = images[index];

  const downloadImage = () => {
    if (!current) return;

    const link = window.document.createElement('a');
    link.href = current.fileUrl;
    link.download = current.fileName;

    window.document.body.appendChild(link);
    link.click();
    window.document.body.removeChild(link);
  }

  const nextImage = () => {
    if (images.length === 0) return;

    setRotation(0);
    setIndex((index + 1) % images.length);
  }

  const listIcon = [
    { icon: <RotateCw size={18} />, actionAtr: () => setRotation(r => r + 90), label: t('onboarding.images.rotate') },
    { icon: <Download size={18} />, actionAtr: downloadImage, label: t('onboarding.images.download') },
    { icon: <MoveRight size={18} />, actionAtr: nextImage, label: t('onboarding.images.next') },
  ]

  return(
    <div className="
      fixed
      inset-0
      flex
      items-center
      justify-center
      bg-black/60
      backdrop-blur-sm
      z-50
      p-0
      sm:p-4
    ">
      <div className="
        relative
        bg-surface
        border-0
        sm:border
        border-divider/40
        p-6
        rounded-none
        sm:rounded-2xl
        shadow-2xl
        w-full
        h-full
        sm:h-auto
        sm:w-120
        flex
        flex-col
      ">
        <IconButton
          onClick={props.closeModal}
          label={t('onboarding.images.close')}
          className="absolute right-4 top-4 z-10 rounded-full text-fg-subtle hover:text-fg hover:bg-raised"
        >
          <X size={18} />
        </IconButton>

        <div className="mb-4">
          <p className="text-xs text-fg-subtle uppercase tracking-wide mb-1">
            {t('onboarding.images.title')}
          </p>
          {current ? (
            <p className="text-sm text-fg font-medium">
              {t(`onboarding.images.${current.kind}`)}
              {current.isPdf ? ' · PDF' : null}
              <span className="text-fg-faint font-normal"> ({index + 1}/{images.length})</span>
            </p>
          ) : null}
        </div>

        <div className="
          flex
          items-center
          justify-center
          flex-1
          sm:flex-none
          sm:h-110
          overflow-hidden
          rounded-lg
          bg-base
          border
          border-divider/30
        ">
          {current?.url ? (
            <img
              src={current.url}
              alt={t(`onboarding.images.${current.kind}`)}
              style={{ transform: `rotate(${rotation}deg)` }}
              className="w-full h-full object-contain transition-transform duration-500"
            />
          ) : current ? (
            <span className="px-6 text-center text-sm text-fg-subtle">
              {t('onboarding.images.previewUnavailable')}
            </span>
          ) : (
            <span className="text-sm text-fg-subtle">
              {t('notifications.loading')}
            </span>
          )}
        </div>

        <div className="
          mt-4
          flex
          justify-center
          gap-2
          bg-base
          border
          border-divider/30
          rounded-full
          p-1.5
        ">
          {listIcon.map((iconObj, i) => (
            <IconButton
              key={i}
              onClick={() => iconObj.actionAtr()}
              label={iconObj.label}
              disabled={!current}
              className="rounded-full text-fg-subtle hover:text-fg hover:bg-raised disabled:opacity-40 disabled:cursor-not-allowed"
            >
              {iconObj.icon}
            </IconButton>
          ))}
        </div>
      </div>
    </div>
  )
}

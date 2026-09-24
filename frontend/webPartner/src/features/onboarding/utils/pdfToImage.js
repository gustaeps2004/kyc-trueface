// The API keeps a PDF document (e.g. the CNH-e) exactly as uploaded, so the viewer renders its
// first page to show it like the other images. pdf.js is imported on demand to keep it out
// of the main bundle.
const MAX_RENDER_DIMENSION = 2400;
const JPEG_QUALITY = 0.92;

export const PDF_TYPE = "application/pdf";

export async function pdfToImage(blob) {
  const [pdfjs, { default: workerSrc }] = await Promise.all([
    import("pdfjs-dist"),
    import("pdfjs-dist/build/pdf.worker.min.mjs?url"),
  ]);

  pdfjs.GlobalWorkerOptions.workerSrc = workerSrc;

  const loadingTask = pdfjs.getDocument({ data: new Uint8Array(await blob.arrayBuffer()) });

  try {
    const pdf = await loadingTask.promise;
    const page = await pdf.getPage(1);
    const { width, height } = page.getViewport({ scale: 1 });
    const viewport = page.getViewport({ scale: MAX_RENDER_DIMENSION / Math.max(width, height) });

    const canvas = document.createElement("canvas");
    canvas.width = Math.floor(viewport.width);
    canvas.height = Math.floor(viewport.height);

    await page.render({ canvas, viewport }).promise;

    return await new Promise((resolve, reject) =>
      canvas.toBlob(
        (result) => (result ? resolve(result) : reject(new Error("Could not export the PDF page."))),
        "image/jpeg",
        JPEG_QUALITY
      )
    );
  } finally {
    await loadingTask.destroy();
  }
}

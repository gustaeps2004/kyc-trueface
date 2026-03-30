export function ApplyMask(value, mask) {
  if (!mask) return value

  const cleanValue = value.replace(/\D/g, "")
  let masked = ""
  let valueIndex = 0

  for (let i = 0; i < mask.length; i++) {
    if (valueIndex >= cleanValue.length) break

    if (mask[i] === "#") {
      masked += cleanValue[valueIndex]
      valueIndex++
    } else {
      masked += mask[i]
    }
  }

  return masked
}
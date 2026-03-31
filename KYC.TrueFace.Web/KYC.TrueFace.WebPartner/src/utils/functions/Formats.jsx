export function IdNumberFormat(idNumber) {
  idNumber = idNumber.replace(/\D/g, '');
  
  idNumber = idNumber.replace(/(\d{3})(\d)/, '$1.$2');
  idNumber = idNumber.replace(/(\d{3})(\d)/, '$1.$2');
  idNumber = idNumber.replace(/(\d{3})(\d{1,2})$/, '$1-$2');
  
  return idNumber;
}

export function DateFormat(date) {
  const d = new Date(date);

  const day = parseInt(String(d.getDate()).padStart(2, '0')) + 1;
  const month = String(d.getMonth() + 1).padStart(2, '0');
  const year = d.getFullYear();

  return `${day}/${month}/${year}`;
}
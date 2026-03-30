export function IdNumberFormat(idNumber) {
  idNumber = idNumber.replace(/\D/g, '');
  
  idNumber = idNumber.replace(/(\d{3})(\d)/, '$1.$2');
  idNumber = idNumber.replace(/(\d{3})(\d)/, '$1.$2');
  idNumber = idNumber.replace(/(\d{3})(\d{1,2})$/, '$1-$2');
  
  return idNumber;
}
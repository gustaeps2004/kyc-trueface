export function IdNumberFormat(idNumber) {
  idNumber = idNumber.replace(/\D/g, '');

  idNumber = idNumber.replace(/(\d{3})(\d)/, '$1.$2');
  idNumber = idNumber.replace(/(\d{3})(\d)/, '$1.$2');
  idNumber = idNumber.replace(/(\d{3})(\d{1,2})$/, '$1-$2');

  return idNumber;
}

export function DateFormat(date) {
  const [year, month, day] = date.split('-');

  const d = new Date(year, month - 1, day);

  const formattedDay = String(d.getDate()).padStart(2, '0');
  const formattedMonth = String(d.getMonth() + 1).padStart(2, '0');
  const formattedYear = d.getFullYear();

  return `${formattedDay}/${formattedMonth}/${formattedYear}`;
}

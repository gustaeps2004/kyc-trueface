// Matches the free-text filter against the fields shown in the grid.
// Digits-only comparison lets "111222" find a masked "111.222.333-44".
export function matchesText(onboarding, filter) {
  const term = filter?.trim().toLowerCase();
  if (!term) return true;

  if (onboarding.name?.toLowerCase().includes(term)) return true;

  const digits = term.replace(/\D/g, '');

  return digits.length > 0
    && onboarding.idNumber?.replace(/\D/g, '').includes(digits);
}

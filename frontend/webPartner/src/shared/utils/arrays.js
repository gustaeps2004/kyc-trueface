export const Permission = [
  { value: 1, labelKey: "permission.common" },
  { value: 2, labelKey: "permission.administrator" }
]

// Mirrors OnboardingSituation on the backend; only the settled states are listed,
// since Pending/Processing/ManualReview never reach a badge.
export const OnboardingSituation = [
  { value: 2, labelKey: "situation.approved", tone: "success" },
  { value: 3, labelKey: "situation.denied", tone: "danger" }
]

export const OnboardingSituationFilter = [
  { value: 0, labelKey: "history.situationAll" },
  ...OnboardingSituation
]

export const UserSituation = [
  { value: 1, labelKey: "users.situation.enabled" },
  { value: 2, labelKey: "users.situation.disabled" },
]

export const UserSituationFilter = [
  { value: 0, labelKey: "users.report.situationAll" },
  ...UserSituation
]
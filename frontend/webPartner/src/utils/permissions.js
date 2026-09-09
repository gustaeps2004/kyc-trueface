import { GetTokenData } from './getTokenData';

export const Roles = {
  commun: 'COMMUN',
  administrator: 'ADMINISTRATOR',
  master: 'MASTER',
};

export const AllRoles = [Roles.commun, Roles.administrator, Roles.master];
export const AdminRoles = [Roles.administrator, Roles.master];

export function GetRole() {
  return GetTokenData()?.role ?? null;
}

export function CanWrite() {
  return AdminRoles.includes(GetRole());
}

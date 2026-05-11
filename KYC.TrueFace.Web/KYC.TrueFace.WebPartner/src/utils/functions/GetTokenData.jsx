import { jwtDecode } from 'jwt-decode';

export function GetTokenData() {
  const token = localStorage.getItem('token');
  return jwtDecode(token);
}
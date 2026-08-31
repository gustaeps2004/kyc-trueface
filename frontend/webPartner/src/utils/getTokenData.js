import { jwtDecode } from 'jwt-decode';

export function GetTokenData() {
  const token = localStorage.getItem('token');
  if (!token) return null;

  try {
    return jwtDecode(token);
  } catch {
    return null;
  }
}

import { useContext } from 'react';
import { createContext } from 'react';

export interface AuthContextType {
  token: string | null;
  login: (jwt: string) => void;
  logout: () => void;
}

export const AuthContext = createContext<AuthContextType>({
  token: null,
  login: () => {},
  logout: () => {},
})

export const useAuth = () => useContext(AuthContext);
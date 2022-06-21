import { createContext } from "react";

export const AuthContext = createContext({
  isLoaded: false,
  user: null,
  setUser: () => { },
  setIsLoaded: () => {},
  logOut: () => { }
});
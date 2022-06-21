import { useCallback, useEffect, useMemo, useState } from "react";
import { AuthContext } from "../context";


function AuthProvider(props) {
    const [isLoaded, setIsLoaded] = useState(false);
    const [user, setUser] = useState(null);

    const logOut = useCallback(() => {
        setUser(null);
        setIsLoaded(false);
        localStorage.removeItem("user");
    }, [setUser, setIsLoaded]);

    const loadData = useCallback(async () => {
        const data = localStorage.getItem("user");
        if (data) {
            setUser(data);
            setIsLoaded(true);
            localStorage.setItem("user", data);
        }

        
    }, [user, isLoaded])
    
    useEffect(() => {
        loadData();
    }, [loadData]);
    
    const contextValue = useMemo(
       () => ({
          isLoaded,
          user,
          setUser,
          setIsLoaded,
          logOut
        }),
        [isLoaded, user, setUser, setIsLoaded, logOut]
    );

    return (
        <AuthContext.Provider value={contextValue}>
          {props.children}
        </AuthContext.Provider>
      );
}
export default AuthProvider
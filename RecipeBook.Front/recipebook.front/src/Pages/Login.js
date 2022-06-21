import React, { useState } from "react";
import { Button } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import useAuth from "../hooks";

function Login() {
    const [password, setPassword] = useState("");
    const [login, setLogin] = useState("");
    const [error, setError] = useState("");
    const auth = useAuth()

    let navigate = useNavigate();

    const handleValidation = (event) => {
        let formIsValid = true;

        if (login.length === 0) {
            formIsValid = false;
            setError("Login Not Valid");
            return false;
        } else {
            setError("");
            formIsValid = true;
        }

        if (!(password.length === 0)) {
            setError("");
            formIsValid = true;
        } else {
            formIsValid = false;
            setError(
                "Password should contain letters and length must be more than 0"
            );
        }

        return formIsValid;
    }

    const loginSubmit = async (e) => {
        console.log(login, password.length)
        if (handleValidation()) {
            const response = await fetch(`https://localhost:7073/api/Users/login`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    "login": login,
                    "password": password
                })
            })

            const data = await response.json();
            if (data.status === 500) {
                setError(data.name)

            } else {

                console.log(data);
                auth.setUser(data);
                auth.setIsLoaded(true);
                localStorage.setItem("user", JSON.stringify(data));
                navigate(`/`);

            }
        }
    }


    return (
        <div className="App">
            <div className="container">
                <div className="row d-flex justify-content-center">
                    <div className="col-md-4">
                        <form id="loginform" onSubmit={loginSubmit}>
                            <div className="form-group">
                                <label>Логин</label>
                                <input
                                    type="login"
                                    className="form-control"
                                    id="LoginInput"
                                    name="LoginInput"
                                    aria-describedby="loginHelp"
                                    placeholder="Введите логин"
                                    value={login}
                                    onChange={(event) => setLogin(event.target.value)}
                                />
                            </div>
                            <div className="form-group-password">
                                <label>Пароль</label>
                                <input
                                    type="password"
                                    className="form-control"
                                    id="exampleInputPassword1"
                                    placeholder="Пароль"
                                    value={password}
                                    onChange={(event) => setPassword(event.target.value)}
                                />
                            </div>
                            <Button variant="outline-success" style={{ marginTop: "5%" }} onClick={() => loginSubmit()}>
                                Войти
                            </Button>
                            <div>
                                <small id="error" className="text-danger form-text">
                                    {error}
                                </small>
                            </div>
                        </form>
                        <Link to="/register">Создать аккаунт</Link>
                    </div>
                </div>
            </div>
        </div>
    );
} export default Login;

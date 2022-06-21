import { Button } from "react-bootstrap";
import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import useAuth from "../hooks";

function Register() {
    const [name, setName] = useState("");
    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    const auth = useAuth();

    let navigate = useNavigate();

    const handleValidation = (event) => {
        let formIsValid = true;

        setError("");

        if (name.length === 0) {
            formIsValid = false;
            setError("Name is not valid");
            return formIsValid;
        }

        if (login.length === 0) {
            formIsValid = false;
            setError("Login is not valid");
            return formIsValid;
        }

        if (password.match(/.{8,22}/)) {
            setError("");
            formIsValid = true;
        } else {
            formIsValid = false;
            setError("Password should contain letters and length must best min 8 characters");
        }

        return formIsValid;
    }

    const registrationSubmit = async (e) => {
        console.log(name, login, password)
        if (await handleValidation()) {
            const response = await fetch(`https://localhost:7073/api/Users/register`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    "name": name,
                    "login": login,
                    "password": password
                })
            })

            const data = await response.json();
            if (data.status === 200) {
                const userResponse = await fetch(`https://localhost:7073/api/Users/login`, {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({
                        "login": login,
                        "password": password
                    })
                })
                
                const userData = await userResponse.json();
                console.log(userData);
                auth.setUser(userData);
                auth.setIsLoaded(true);
                localStorage.setItem("user", JSON.stringify(userData));
                navigate(`/`);
            } else{
                setError(data.message)
            }
        }
    }

    return (
        <div className="App">
            <div className="container">
                <div className="row d-flex justify-content-center">
                    <div className="col-md-4">
                        <form id="loginform" onSubmit={registrationSubmit}>
                            <div className="form-group">
                                <label>Имя</label>
                                <input
                                    type="Name"
                                    className="form-control"
                                    id="NameInput"
                                    name="NameInput"
                                    aria-describedby="nameHelp"
                                    placeholder="Введите имя"
                                    value={name}
                                    onChange={(event) => setName(event.target.value)}
                                />
                            </div>
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
                            <div className="form-group-password" style={{ marginTop: "10px" }}>
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
                            <Button variant="outline-success" style={{ marginTop: "5%" }} onClick={()=>registrationSubmit()}>
                                Зарегистрироваться
                            </Button>
                            <div>
                                <small id="error" className="text-danger form-text">
                                    {error}
                                </small>
                            </div>
                        </form>
                        <Link to="/login">Уже есть аккаунт?</Link>
                    </div>
                </div>
            </div>
        </div>
    );
} export default Register
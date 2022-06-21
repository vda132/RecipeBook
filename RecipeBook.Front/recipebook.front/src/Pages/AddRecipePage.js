import { useDropzone } from 'react-dropzone';
import React, { useCallback, useState } from "react";
import { Button } from "react-bootstrap";
import {  useNavigate } from "react-router-dom";

function AddRecipePage() {
    const [name, setName] = useState("");
    const [files, setFiles] = useState([]);
    const [text, setText] = useState(""); 
    const [error, setError] = useState();

    let navigate = useNavigate();

    const onDrop = useCallback((acceptedFiles) => {
        const mappedAcc = acceptedFiles.map((file) => (file));
        setFiles((curr) => [...curr, ...mappedAcc]);
    }, []);

    const removeFile = (file) => () => {
        acceptedFiles.splice(acceptedFiles.indexOf(file), 1)
        console.log(acceptedFiles)
        setFiles([...acceptedFiles]);
    }

    const { getRootProps, getInputProps, open, acceptedFiles } = useDropzone({
        onDrop,
        noClick: true,
        noKeyboard: true,
        maxFiles: 3,
        accept: {
            'image/*': ['.jpeg', '.png']
        }
    });

    const filesDisp = files.map(file => (
        <li id={file.path}>{file.path}
            <button onClick={removeFile(file)}>Remove File</button></li>
    ));

    const validate = (event) => {
        let isValid = true;
        setError("");
        if(name.length === 0) {
            setError("Enter recipe name");
            return false;
        }

        if (files.length === 0) {
            setError("Select image");
            return false;
        }

        if(text.length === 0) {
            setError("Input recipe");
            return false;
        }
        return isValid;
    }

    const addRecipe = async (event) => {
        console.log(JSON.parse(localStorage.getItem("user")).id)
        
        if (validate()) {
           const response = await fetch(`https://localhost:7073/api/Recipes`,{
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                "image": files[0].path,
                "recipeName": name,
                "description": text, 
                "userId": JSON.parse(localStorage.getItem("user")).id
            })
           }) 
           const data = await response.json();
           if(data.status === 200) {
            navigate('/');
           } else {
            console.log(data.message)
            setError(data.message);
           }
        }
    }

    return (
        <div>
            <div className="container">
                <div {...getRootProps({ className: 'dropzone' })}>
                    <input {...getInputProps()} />
                    <p>Загрузить фото</p>
                    <button type="button" onClick={open} disabled={files.length === 1}>
                        Открыть
                    </button>
                    <aside>
                        <h4>Картинка</h4>
                        <ul>{filesDisp}</ul>
                    </aside>
                </div>
            </div>
            <label>Введите название</label>
            <div>
                <input
                value={name}
                onChange={(event) => setName(event.target.value)}></input>
            </div>
            <label> Напишите рецепт </label>
            <div className="container">
                <textarea style={{ width: "80%", height: "500px" }}
                value={text}
                onChange={(event) => setText(event.target.value)}>
                </textarea>
            </div>
            <Button variant="outline-primary" onClick={() => addRecipe()}>Добавить рецепт</Button>
            <div>
                <small id="error" className="text-danger form-text">
                    {error}
                </small>
            </div>
        </div>
    )
} export default AddRecipePage
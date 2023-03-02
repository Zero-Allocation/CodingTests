import React, {useState} from "react";
import './LoginForm.css';

const LoginForm = (props) => {
    const [localState, setLocalState] = useState({
        login: "",
        password: ""
    });

    const handleChange = (event) => {
        const value = event.target.value;
        setLocalState({...localState, [event.target.name]: value});
    }

    const handleSubmit = (event) => {
        event.preventDefault();

        props.onSubmit({
            login: localState.login,
            password: localState.password,
            currentDateTime: Date().toLocaleString()
        });
    }

    return (
        <form className="form">
            <h1>Login</h1>
            <label htmlFor="name">Name</label>
            <input type="text" id="login" name="login" value={localState.login} onChange={handleChange}/>
            <label htmlFor="password">Password</label>
            <input type="password" id="password" name="password" value={localState.password} onChange={handleChange}/>
            <button type="submit" onClick={handleSubmit}>Continue</button>
        </form>
    )
}

export default LoginForm;
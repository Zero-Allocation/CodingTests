import React, {useState} from "react";
import './App.css';
import LoginForm from './LoginForm';
import LoginAttemptList from './LoginAttemptList';

const App = () => {
    const [loginAttempts, setLoginAttempts] = useState([]);

    const handleOnSubmit = ({login, password, currentDateTime}) => {
        console.log(login);
        console.log(password);
        console.log(currentDateTime);
        setLoginAttempts(prevState => [...prevState, {login, password, currentDateTime}]);
    }

    return (<div className="App">
        <LoginForm onSubmit={({login, password, currentDateTime}) => handleOnSubmit({login, password, currentDateTime})}/>
        <LoginAttemptList attempts={loginAttempts}/>
    </div>);
};

export default App;
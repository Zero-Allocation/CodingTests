import React, {useState} from "react";
import "./LoginAttemptList.css";

const LoginAttemptList = (props) => {
    const [localState, setLocalState] = useState({
        filter: ""
    });

    const handleChange = (event) => {
        const value = event.target.value;
        setLocalState({...localState, [event.target.name]: value});
    }

    const loginAttempts = props.attempts.map((attempt) => {
        const filterIsNullOrEmpty = !localState.filter;
        const filterMatchesLogin = attempt.login.includes(localState.filter);
        const filterMatchesCurrentDateTime = attempt.currentDateTime.includes(localState.filter);

        if (filterIsNullOrEmpty || filterMatchesLogin || filterMatchesCurrentDateTime) {
            return (
                <tr>
                    <td className="Attempt-Table-Login">{attempt.login}</td>
                    <td className="Attempt-Table-CurrentDateTime">{attempt.currentDateTime}</td>
                </tr>
            );
        }
    });

    return (
        <div>
            <p>Recent activity</p>
            <input type="input" id="filter" name="filter" placeholder="Filter..." value={localState.filter} onChange={handleChange} />
            <table className="Attempt-Table">
                <tbody className="Attempt-Table-Body">
                    {loginAttempts}
                </tbody>
            </table>
        </div>
    );
}

export default LoginAttemptList;
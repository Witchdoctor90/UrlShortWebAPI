import React from 'react';
import LoginCard from "../UI/LoginCard/LoginCard";

const Login = (props) => {
    return (
        <div>
            <LoginCard host ="http://localhost:5024/"></LoginCard>
        </div>
    );
};

export default Login;
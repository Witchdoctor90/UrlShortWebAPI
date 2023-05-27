import React, {useState} from 'react';
import styles from './LoginCard.module.css'
import axios from "axios";



const LoginCard = (props) => {
    const [login, setLogin] = useState('');
    const [password, setPassword] = useState('');
    const host = props.host;

    const LogOut = () => {
        sessionStorage.removeItem('token');
    }

    const Login = async (e) => {
        e.preventDefault();
        let body = {Username: login, Password: password};
        let result = await axios.post(host + 'Users/login', body)
            .catch(function (error)
        {
            if(error.response)
            {
                console.log(error)
                console.log(error.response.data);
                console.log(error.response.status);
                return;
            }
        });
        let token = result.data;
        sessionStorage.setItem('token', token);
        navigator.push('/index');
    }

    return (
        <div className={styles.body}>
        <div className={styles.center}>
            <h1>Login</h1>
            <form className={styles.form} onSubmit={Login}>
                <div className={styles.txt_field}>
                    <input value={login} onChange={event => setLogin(event.target.value)} className={styles.input} type="text" required=""/>
                        <span className={styles.span}></span>
                        <label className={styles.username}>Username</label>
                </div>
                <div className={styles.txt_field}>
                    <input value={password} onChange={event => setPassword(event.target.value)} className={styles.input} type="password" required=""/>
                        <span className={styles.span}></span>
                        <label className={styles.username}>Password</label>
                </div>
                <div className={styles.pass}>Forgot Password?</div>
                <input className={styles.submit} type="submit" value="Login"/>
                    <div className={styles.signup_link}>
                        Not a member? <a href="#">Signup</a>
                    </div>
            </form>
        </div>
        </div>
);
};

export default LoginCard;
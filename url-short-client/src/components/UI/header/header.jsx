import React, {useState} from 'react';
import styles from "../header/header.module.css";
import {Link} from "react-router-dom";

const Header = (props) => {
    const[isAuthenticated, setIsAuthenticated] = useState(props.isAuthenticated);
    return (
        <div>
            <header className={styles.header}>
                <div className={styles.headerLeft}>
                    <Link to={"/index"} className={styles.headerLink}>Home</Link>
                    <Link to={"/about"} className={styles.headerLink}>About</Link>
                    <Link to={"/list"} className={styles.headerLink}>All Links</Link></div>
                {isAuthenticated? <Link to={"/myLinks"} className={styles.headerLink}>My Links</Link> : null}

                <div className={styles.headerRight}>
                    <Link to="/login" className={styles.headerLink}>Login</Link>
                </div>
            </header>
        </div>
    );
};

export default Header;
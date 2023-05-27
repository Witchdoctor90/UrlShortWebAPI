import React from 'react';
import styles from './SubmitButton.module.css'

const SubmitButton = () => {
    return (
        <div>
            <input className={styles.button} type="submit"/>
        </div>
    );
};

export default SubmitButton;
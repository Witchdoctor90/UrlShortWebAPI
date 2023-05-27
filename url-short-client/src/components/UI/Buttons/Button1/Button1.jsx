import React from 'react';
import './button1.module.css';
const Button1 = (props) => {
    return (
        <div>
            <button>{props.value}</button>
        </div>
    );
};

export default Button1;
import React from 'react';

const About = () => {
    return (
        <div style={{padding: '15px 20px'}}>
            <h1>About app</h1>
            <p>This app will shortify your long links, so you can send it your friends, or post to your social networks.
                It will look much more beautiful then post it regular view. </p> <br/>
            <p>I Was using next stack:</p> <br/>
            <ul>
                <li>-Asp.net Core minimal API</li>
                <li>-React for client app</li>
                <li>-Identity with JWT Authorization</li>
                <li>-Entity Framework for Storing links and users</li>
            </ul>
        </div>
    );
};

export default About;
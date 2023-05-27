import React, {useState} from 'react';
import UrlInputForm from "../UI/Form/UrlInputForm";
import Header from "../UI/header/header";

const Index = () => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const serverUrl = 'http://localhost:5024/';

    return (
        <div>
            <UrlInputForm serverUrl={serverUrl}></UrlInputForm>
        </div>
    );
};

export default Index;
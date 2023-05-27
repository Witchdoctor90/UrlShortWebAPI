import React, {useState} from 'react';
import axios from 'axios';
import styles from './UrlInputs.module.css'



const UrlInputForm = (props) =>{

    const [url, setUrl] = useState("");
    let [isValid, setValid] = useState(false);
    let[errors, setErrors] = useState([]);
    let [shortUrl, setShortUrl] = useState('');

    const handleOnSubmit = async (e) => {
        e.preventDefault();
        isValid = validateUrl(url);
        if(!isValid) setErrors([..."Invalid Url"]);

        var token = sessionStorage.getItem('token');
        console.log(token)
        const headers = {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        };
        var requestBody = url;
        var response = await axios.post(props.serverUrl + 'Shorturl/shorten',
            requestBody,
            {headers:headers});
        setShortUrl(response.data);
        console.log(response);
    }

    const validateUrl = (url) => {
        const regEx = /^(?:(?:https?|ftp):\/\/)?(?:(?!(?:10|127)(?:\.\d{1,3}){3})(?!(?:169\.254|192\.168)(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})))(?::\d{2,5})?(?:\/\S*)?$/;
        return regEx.test(url);
    }


    return (
        <div className={styles.body}>
            <div className={styles.container}>
                <form className={styles.form} onSubmit={handleOnSubmit}>
                    <label htmlFor="LongUrl">Long Url</label>
                    <input value={url} onChange={event => setUrl(event.target.value)} className={styles.input} type="text" id="LongUrl" name="firstname" placeholder="Enter URL.."/>

                    <label htmlFor="ShortUrl">Short Url</label>
                    <input value={shortUrl} onChange={event => setShortUrl(event.target.value)} className={styles.input} type="text" id="ShortUrl" name="lastname" />
                    <input className={styles.submit} type="submit" value="Shorten!"/>
                </form>
            </div>
        </div>

    );
};

export default UrlInputForm;
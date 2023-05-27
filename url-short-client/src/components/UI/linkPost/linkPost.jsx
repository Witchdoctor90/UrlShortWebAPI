import React from 'react';
import styles from './linkPost.module.css'
import Button1 from "../Buttons/Button1/Button1";

const LinkPost = (props) => {
    const {authorName, id, shortenedUrl, url} = props.link;
    const host = 'http://localhost:5024/';



    return (
        <div className={styles.post}>
            <div className={styles.post__content}>
                    <label htmlFor="longUrl">Long URL</label>
                    <input value={url} id='longUrl' type="text" disabled/>

                    <label htmlFor='shortUrl'>Shortened URL</label>
                    <input value={host + shortenedUrl} id='shortUrl' type="text" disabled/>

                    <strong>Author - {authorName}</strong>
            </div>
            <div className={styles.post__btns}>
                <Button1 value={'Delete'}>Delete</Button1>
                <Button1 value={'Info'}>Info</Button1>
            </div>
        </div>
    );
};
export default LinkPost;
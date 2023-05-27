import React, {useEffect, useState} from 'react';
import LinkPost from "../linkPost/linkPost";
import styles from './linksList.module.css'
import axios from "axios";

const LinksList = (props) => {

    const host = 'http://localhost:5024/';
    const [list, setList] = useState(props.list);
    useEffect(() => {fetchLinks()}, []);

    async function fetchLinks()
    {
        let response = await axios.get(host + 'Shorturl/GetAll');
        setList(response.data);
    }

    return (
        <div className={styles.linksList}>
            {list.map(link => <LinkPost link={link}></LinkPost>)}
        </div>
    );
};

export default LinksList;
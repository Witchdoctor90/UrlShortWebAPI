import React, {useEffect, useState} from 'react';
import LinksList from "../UI/linksList/linksList";
import axios from "axios";



const List = () => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [isAdmin, setIsAdmin] = useState(false);
    const [list, setList] = useState([]);
    return (
        <div>
            <LinksList list={list}></LinksList>
        </div>
    );
};

export default List;
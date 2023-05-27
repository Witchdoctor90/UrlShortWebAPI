import {BrowserRouter, Route, Routes, useNavigate} from "react-router-dom";
import Index from "./components/pages";
import About from "./components/pages/about";
import Login from "./components/pages/login";
import List from "./components/pages/list";
import Header from "./components/UI/header/header";
import React from "react";
import './components/styles/App.css'

function App() {
    const IsAuthenticated = React.createContext(false);
    return (
      <BrowserRouter>
          <Header></Header>
              <Routes>
              <Route path="/index" Component={Index}>
              </Route>
              <Route path="/about" Component={About}>
              </Route>
              <Route path="/list" Component={List}>
              </Route>
              <Route path="/login" Component={Login}>
              </Route>
          </Routes>
      </BrowserRouter>
  );
}

export default App;

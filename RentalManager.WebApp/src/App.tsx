import React, {useEffect} from 'react';
import "./react-app-env.d"
import AppBar from "./Components/AppBar";

function App() {
    useEffect(() => {
        if (!localStorage.getItem("API_URL")) {
            localStorage.setItem('API_URL', process.env.REACT_APP_API_URL)
        }
    }, [])
    return (
        <div>
            <AppBar/>
        </div>
    );
}

export default App;

import React, {useEffect} from 'react';
import "./react-app-env.d"
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import Root from "./Components/Root";
import ErrorPage from "./Components/ErrorPage";
import {createTheme, ThemeProvider} from "@mui/material";

const router = createBrowserRouter([
    {
        path: "/",
        element: <Root/>,
        errorElement: <ErrorPage/>,
        children: [
            {
                path: "App",
                element: <App/>,
            },
        ],
    }
]);

function App() {
    const theme = createTheme({
        palette: {
            mode: localStorage.getItem("DISPLAY_MODE") === 'light' ? 'light' : 'dark',
            ...(localStorage.getItem("DISPLAY_MODE") === 'light'
                ? {
                    background: {
                        paper: '#ffffff'
                    }
                }
                : {
                    background: {
                        paper: '#121212'
                    }
                }),
        },
    });
    useEffect(() => {
        if (!localStorage.getItem("API_URL")) {
            localStorage.setItem('API_URL', process.env.REACT_APP_API_URL)
        }
    }, [])
    return (
        <div>
            <ThemeProvider theme={theme}>
                <RouterProvider router={router}/>
            </ThemeProvider>
        </div>
    );
}

export default App;

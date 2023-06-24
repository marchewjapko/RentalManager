import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import reportWebVitals from './reportWebVitals';

import {
    createBrowserRouter,
    RouterProvider,
} from "react-router-dom";
import ErrorPage from "./Components/ErrorPage";
import Root, { loader as rootLoader } from "./Components/Root";

const router = createBrowserRouter([
    {
        path: "/",
        element: <Root/>,
        errorElement: <ErrorPage/>,
        loader: rootLoader,
        children: [
            {
                path: "App",
                element: <App />,
            },
        ],
    }
]);

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);
root.render(
    <React.StrictMode>
        <RouterProvider router={router}/>
    </React.StrictMode>
);
reportWebVitals();

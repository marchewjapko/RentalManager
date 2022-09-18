import React from 'react';
import ReactDOM from "react-dom/client";
import {Routes, Route, HashRouter } from "react-router-dom";
import * as serviceWorker from "./serviceWorker";
import {createTheme, ThemeProvider} from "@mui/material";
import CssBaseline from '@mui/material/CssBaseline';
import Clients from "./Components/Clients/Clients";
import RentalEquipment from "./Components/RentalEquipment/RentalEquipment";
import Employees from "./Components/Employees/Employees";

const darkTheme = createTheme({
    palette: {
        mode: 'dark',
    },
});


const renderReactDom = () => {
    const root = ReactDOM.createRoot(
        document.getElementById("root")
    );
    root.render(
        <ThemeProvider theme={darkTheme}>
            <CssBaseline />
            <HashRouter>
                <Routes>
                    <Route exact path="/" element={<Clients />} />
                    <Route exact path="/2" element={<RentalEquipment />} />
                    <Route exact path="/3" element={<Employees />} />
                </Routes>
            </HashRouter>
        </ThemeProvider>
    );
};

if (window.cordova) {
  document.addEventListener('deviceready', () => {
    renderReactDom();
  }, false);
} else {
  renderReactDom();
}
serviceWorker.unregister();
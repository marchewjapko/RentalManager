import React from 'react';
import ReactDOM from 'react-dom/client';
import { Routes, Route, HashRouter } from 'react-router-dom';
import * as serviceWorker from './serviceWorker';
import { createTheme, ThemeProvider } from '@mui/material';
import CssBaseline from '@mui/material/CssBaseline';
import RentalEquipment from './Components/RentalEquipment/RentalEquipment';
import Employees from './Components/Employees/Employees';
import RentalAgreement from './Components/RentalAgreement/RentalAgreement';
import AddRentalEquipmentForm from './Components/RentalAgreement/AddRentalEquipmentForm';

const darkTheme = createTheme({
	palette: {
		mode: 'dark',
	},
});

const renderReactDom = () => {
	const root = ReactDOM.createRoot(document.getElementById('root'));
	root.render(
		<ThemeProvider theme={darkTheme}>
			<CssBaseline />
			<HashRouter>
				<Routes>
					<Route
						exact
						path="/rental-equipment"
						element={<RentalEquipment />}
					/>
					<Route exact path="/employees" element={<Employees />} />
					<Route
						exact
						path="/add-rental-agreement"
						element={<AddRentalEquipmentForm />}
					/>
					<Route exact path="/" element={<RentalAgreement />} />
				</Routes>
			</HashRouter>
		</ThemeProvider>
	);
};

if (window.cordova) {
	document.addEventListener(
		'deviceready',
		() => {
			renderReactDom();
		},
		false
	);
} else {
	renderReactDom();
}
serviceWorker.unregister();

import { createBrowserRouter, Outlet, RouterProvider } from 'react-router-dom';
import Dashboard from './Components/Dashboard/Dashboard';
import RentalEquipment from './Components/RentalEquipment/RentalEquipment';
import Employees from './Components/Employees/Employees';
import RentalAgreement from './Components/RentalAgreement/RentalAgreement';
import AddRentalEquipmentForm from './Components/RentalAgreement/AddRentalEquipmentForm';
import React from 'react';
import { createTheme, ThemeProvider } from '@mui/material';
import CssBaseline from '@mui/material/CssBaseline';
import { useCookies } from 'react-cookie';
import Header from './Components/Header/Header';
import i18next from 'i18next';
import pl from './Tanslations/pl.json';
import en from './Tanslations/en.json';
import { I18nextProvider } from 'react-i18next';

const darkTheme = createTheme({
	palette: {
		mode: 'dark',
	},
});

const lightTheme = createTheme({
	palette: {
		mode: 'light',
	},
});

i18next.init({
	interpolation: { escapeValue: false },
	lng: 'en',
	resources: {
		en: {
			translation: en,
		},
		pl: {
			translation: pl,
		},
	},
});

export default function App() {
	const [isLightMode, setIsLightMode] = useCookies(['lightMode']);
	const handleChangeTheme = () => {
		if (isLightMode['lightMode'] === 'true') {
			setIsLightMode('lightMode', false, {
				path: '/',
				expires: new Date(2147483647 * 1000),
				sameSite: 'strict',
			});
		} else {
			setIsLightMode('lightMode', true, {
				path: '/',
				expires: new Date(2147483647 * 1000),
				sameSite: 'strict',
			});
		}
	};

	const AppLayout = () => (
		<>
			<Header handleChangeTheme={handleChangeTheme} />
			<Outlet />
		</>
	);
	function GetRouter() {
		return new createBrowserRouter([
			{
				element: <AppLayout />,
				children: [
					{
						path: '/',
						element: <Dashboard />,
					},
					{
						path: '/rental-agreement',
						element: <RentalAgreement />,
					},
					{
						path: '/add-rental-agreement',
						element: <AddRentalEquipmentForm />,
					},
					{
						path: '/rental-equipment',
						element: <RentalEquipment />,
					},
					{
						path: '/employees',
						element: <Employees />,
					},
				],
			},
		]);
	}

	return (
		<I18nextProvider i18n={i18next}>
			<ThemeProvider
				theme={
					isLightMode['lightMode'] === 'true' ? lightTheme : darkTheme
				}
			>
				<CssBaseline />
				<RouterProvider router={GetRouter()} />
			</ThemeProvider>
		</I18nextProvider>
	);
}

import { RouterProvider } from 'react-router-dom';
import React from 'react';
import { createTheme, ThemeProvider } from '@mui/material';
import CssBaseline from '@mui/material/CssBaseline';
import { useCookies } from 'react-cookie';
import { RecoilRoot } from 'recoil';
import GetRouter from './Components/Router';
import './i18n';

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

export default function App() {
	const [isDarkMode] = useCookies();
	return (
		<ThemeProvider
			theme={
				isDarkMode['isDarkMode'] === 'true' || isDarkMode['isDarkMode'] === undefined
					? darkTheme
					: lightTheme
			}
		>
			<CssBaseline />
			<RecoilRoot>
				<RouterProvider router={GetRouter()} />
			</RecoilRoot>
		</ThemeProvider>
	);
}

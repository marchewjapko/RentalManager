import { RouterProvider } from 'react-router-dom';
import React from 'react';
import { createTheme, ThemeProvider } from '@mui/material';
import CssBaseline from '@mui/material/CssBaseline';
import { useCookies } from 'react-cookie';
import i18next from 'i18next';
import pl from './Tanslations/pl.json';
import en from './Tanslations/en.json';
import { I18nextProvider } from 'react-i18next';
import { RecoilRoot } from 'recoil';
import GetRouter from './Components/Router';

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
	const [isDarkMode] = useCookies();
	return (
		<I18nextProvider i18n={i18next}>
			<ThemeProvider
				theme={
					isDarkMode['isDarkMode'] === 'true' ||
					isDarkMode['isDarkMode'] === undefined
						? darkTheme
						: lightTheme
				}
			>
				<CssBaseline />
				<RecoilRoot>
					<RouterProvider router={GetRouter()} />
				</RecoilRoot>
			</ThemeProvider>
		</I18nextProvider>
	);
}

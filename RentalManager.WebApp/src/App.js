import { RouterProvider } from 'react-router-dom';
import React from 'react';
import { createTheme, Snackbar, ThemeProvider } from '@mui/material';
import CssBaseline from '@mui/material/CssBaseline';
import { useCookies } from 'react-cookie';
import { DefaultValue, RecoilRoot, useRecoilState } from 'recoil';
import GetRouter from './Router';
import './i18n';
import MuiAlert from '@mui/material/Alert';
import { globalSnackbar } from './Components/Atoms/GeneralAtoms';

const darkTheme = createTheme({
	palette: {
		mode: 'dark',
	},
	shape: {
		borderRadius: 10,
	},
});

const lightTheme = createTheme({
	palette: {
		mode: 'light',
	},
	shape: {
		borderRadius: 10,
	},
});

const Alert = React.forwardRef(function Alert(props, ref) {
	return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
});

function GetSnackbar() {
	const [snackbar, setSnackbar] = useRecoilState(globalSnackbar);
	const handleClose = () => {
		setSnackbar({ ...snackbar, show: false });
		setTimeout(() => setSnackbar(new DefaultValue()), 250);
	};
	return (
		<Snackbar open={snackbar.show} autoHideDuration={3000} onClose={handleClose}>
			<Alert onClose={handleClose} severity={snackbar.severity} sx={{ width: '100%' }}>
				{snackbar.title}
			</Alert>
		</Snackbar>
	);
}

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
				<GetSnackbar />
				<RouterProvider router={GetRouter()} />
			</RecoilRoot>
		</ThemeProvider>
	);
}

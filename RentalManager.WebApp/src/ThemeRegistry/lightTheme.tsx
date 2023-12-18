import { createTheme } from "@mui/material/styles";

const lightTheme = createTheme({
	palette: {
		mode: "light",
		primary: {
			main: "#3b82f6",
		},
	},
	shape: {
		borderRadius: 10,
	},
});

export default lightTheme;

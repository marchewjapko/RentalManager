import { createTheme } from "@mui/material/styles";

const darkTheme = createTheme({
	palette: {
		mode: "dark",
		primary: {
			main: "#3b82f6",
		},
	},
	shape: {
		borderRadius: 10,
	},
});

export default darkTheme;

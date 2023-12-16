import { errorAtom } from "@/app/SnackbarErrors/ErrorAtom";
import { Alert, Snackbar } from "@mui/material";
import { useRecoilState } from "recoil";

export default function ErrorSnackbarProvider() {
	const [error, setError] = useRecoilState(errorAtom);

	const handleClose = () => {
		setError({
			message: null,
		});
	};

	return (
		<>
			<Snackbar
				open={error.message !== null}
				anchorOrigin={{ horizontal: "left", vertical: "top" }}
				autoHideDuration={3000}
				onClose={handleClose}
			>
				<Alert onClose={handleClose} severity="error" sx={{ width: "100%" }}>
					{error.message}
				</Alert>
			</Snackbar>
		</>
	);
}

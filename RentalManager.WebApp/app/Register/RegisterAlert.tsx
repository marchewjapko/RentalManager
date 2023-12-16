import React, { Dispatch, SetStateAction } from "react";
import { Alert, AlertTitle } from "@mui/material";
import { registerAlertType } from "@/app/Register/RegisterForm";

export default function RegisterAlert(props: {
	alert: registerAlertType | null;
	setAlert: Dispatch<SetStateAction<registerAlertType | null>>;
}) {
	const { alert, setAlert } = props;

	if (alert === null) {
		return;
	}

	return (
		<Alert severity={alert?.mode} onClose={() => setAlert(null)}>
			<AlertTitle>{alert?.title}</AlertTitle>
			{alert?.message}
		</Alert>
	);
}

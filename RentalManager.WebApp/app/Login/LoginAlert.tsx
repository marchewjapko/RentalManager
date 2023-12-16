import { Alert, AlertTitle } from "@mui/material";
import React, { Dispatch, SetStateAction } from "react";
import { registerAlertType } from "@/app/Register/RegisterForm";
import { loginAlertType } from "@/app/Login/LoginForm";

export default function LoginAlert(props: {
	alert: loginAlertType | null;
	setAlert: Dispatch<SetStateAction<loginAlertType | null>>;
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

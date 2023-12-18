import React, { Dispatch, SetStateAction } from "react";
import { Alert, AlertTitle } from "@mui/material";
import { IInlineAlert } from "@/src/InlineAlert/IInlineAlert";

export default function InlineAlert(props: {
	alert: IInlineAlert | null;
	setAlert: Dispatch<SetStateAction<IInlineAlert | null>>;
}) {
	const { alert, setAlert } = props;

	if (alert === null) {
		return;
	}

	return (
		<Alert severity={alert?.mode} onClose={() => setAlert(null)}>
			<AlertTitle>{alert.title ?? "Unknown Error"}</AlertTitle>
			{alert.message ?? "An unknown error has occurred"}
		</Alert>
	);
}

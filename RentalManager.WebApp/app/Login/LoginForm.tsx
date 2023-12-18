"use client";
import React, { useState } from "react";
import { useFormik } from "formik";
import {
	Button,
	Checkbox,
	FormControlLabel,
	InputAdornment,
	TextField,
} from "@mui/material";
import { loginSchema } from "@/app/Login/loginSchema";
import LockOpenIcon from "@mui/icons-material/LockOpen";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import LockIcon from "@mui/icons-material/Lock";
import { LoadingButton } from "@mui/lab";
import { IInlineAlert } from "@/src/InlineAlert/IInlineAlert";
import InlineAlert from "@/src/InlineAlert/InlineAlert";
import SignIn from "@/app/Actions/SignIn";

export default function LoginForm() {
	const [isLoading, setIsLoading] = useState(false);
	const [alert, setAlert] = useState<IInlineAlert | null>(null);

	const formik = useFormik({
		initialValues: {
			userName: "",
			password: "",
			rememberMe: true,
		},
		validationSchema: loginSchema,
		onSubmit: (values) => {
			setIsLoading(true);
			SignIn({
				userName: values.userName,
				password: values.password,
				useCookies: true,
				useSessionCookies: !values.rememberMe,
			}).then((result) => {
				setIsLoading(false);
				if (!result.isSuccess)
					setAlert({
						mode: "error",
						title: result.title,
						message: result.message,
					});
			});
		},
	});

	return (
		<form
			onSubmit={formik.handleSubmit}
			className={"flex flex-col gap-5 w-full"}
		>
			<TextField
				id="userName"
				name="userName"
				type="text"
				onChange={formik.handleChange}
				onBlur={formik.handleBlur}
				value={formik.values.userName}
				label="Username"
				helperText={
					formik.errors.userName ? formik.errors.userName : "Required"
				}
				error={formik.touched.userName && Boolean(formik.errors.userName)}
				InputProps={{
					startAdornment: (
						<InputAdornment position="start">
							<AccountCircleIcon />
						</InputAdornment>
					),
				}}
			/>

			<TextField
				id="password"
				name="password"
				type="password"
				onChange={formik.handleChange}
				onBlur={formik.handleBlur}
				value={formik.values.password}
				label="Password"
				helperText={
					formik.errors.password ? formik.errors.password : "Required"
				}
				error={formik.touched.password && Boolean(formik.errors.password)}
				InputProps={{
					startAdornment: (
						<InputAdornment position="start">
							<LockIcon />
						</InputAdornment>
					),
				}}
			/>

			<div className={"flex flex-row max-sm:flex-col justify-between"}>
				<FormControlLabel
					control={<Checkbox checked={formik.values.rememberMe} />}
					onBlur={formik.handleBlur}
					onChange={formik.handleChange}
					id="rememberMe"
					name="rememberMe"
					label="Remember me"
				/>

				<Button variant="text" className={"h-min self-end"}>
					Change password
				</Button>
			</div>

			<InlineAlert alert={alert} setAlert={setAlert}></InlineAlert>

			<LoadingButton
				variant="contained"
				type="submit"
				endIcon={<LockOpenIcon />}
				loading={isLoading}
			>
				Sign in
			</LoadingButton>
		</form>
	);
}

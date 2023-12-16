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
import { OpenAPI, ProblemDetails, UserService } from "@/app/ApiClient/codegen";
import { loginSchema } from "@/app/Login/loginSchema";
import LockOpenIcon from "@mui/icons-material/LockOpen";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import LockIcon from "@mui/icons-material/Lock";
import { LoadingButton } from "@mui/lab";
import LoginAlert from "@/app/Login/LoginAlert";
import { registerAlertType } from "@/app/Register/RegisterForm";

OpenAPI.BASE = "http://localhost:8080";
OpenAPI.WITH_CREDENTIALS = true;
OpenAPI.CREDENTIALS = "include";

export interface loginAlertType {
	mode: "error" | "success";
	title: string;
	message: string;
}

export default function LoginForm() {
	const [isLoading, setIsLoading] = useState(false);
	const [alert, setAlert] = useState<loginAlertType | null>(null);

	const formik = useFormik({
		initialValues: {
			userName: "",
			password: "",
			rememberMe: true,
		},
		validationSchema: loginSchema,
		onSubmit: (values) => {
			setIsLoading(true);
			UserService.postUserLogin({
				userName: values.userName,
				password: values.password,
				useCookies: true,
				useSessionCookies: !values.rememberMe,
			})
				.then(() => {
					setIsLoading(false);
				})
				.catch((error) => {
					setIsLoading(false);
					const errorBody = JSON.parse(JSON.stringify(error))
						.body as ProblemDetails;

					let message = errorBody.detail;

					if (errorBody.title === "Login failed") {
						message = "Username and/or password is incorrect";
					} else if (errorBody.title === "User not confirmed") {
						message = "Contact your administrator to confirm your account.";
					}
					setAlert({
						mode: "error",
						title: errorBody.title,
						message: message,
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

			<LoginAlert alert={alert} setAlert={setAlert}></LoginAlert>

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

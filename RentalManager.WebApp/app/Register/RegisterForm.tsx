"use client";

import React, { useState } from "react";
import { useFormik } from "formik";
import { OpenAPI, ProblemDetails, UserService } from "@/app/ApiClient/codegen";
import {
	Button,
	Divider,
	IconButton,
	InputAdornment,
	TextField,
} from "@mui/material";
import BadgeIcon from "@mui/icons-material/Badge";
import { LoadingButton } from "@mui/lab";
import PersonAddIcon from "@mui/icons-material/PersonAdd";
import { registerSchema } from "@/app/Register/registerSchema";
import RegisterAlert from "@/app/Register/RegisterAlert";
import VisibilityOffIcon from "@mui/icons-material/VisibilityOff";
import VisibilityIcon from "@mui/icons-material/Visibility";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import LockIcon from "@mui/icons-material/Lock";

OpenAPI.BASE = "http://localhost:8080";
OpenAPI.WITH_CREDENTIALS = true;
OpenAPI.CREDENTIALS = "include";

export interface registerAlertType {
	mode: "error" | "success";
	title: string;
	message: string;
}

export default function RegisterForm() {
	const [isLoading, setIsLoading] = useState(false);
	const [alert, setAlert] = useState<registerAlertType | null>(null);
	const [isPasswordHidden, setIsPasswordHidden] = useState(true);

	const formik = useFormik({
		initialValues: {
			name: "",
			surname: "",
			password: "",
			userName: "",
		},
		validationSchema: registerSchema,
		onSubmit: (values) => {
			setIsLoading(true);
			UserService.postUser({
				formData: {
					Name: values.name,
					Surname: values.surname,
					Password: values.password,
					UserName: values.userName,
				},
			})
				.then(() => {
					setIsLoading(false);
					setAlert({
						mode: "success",
						title: "Your account has been created!",
						message: "Contact your administrator to confirm your registration.",
					});
				})
				.catch((error) => {
					setIsLoading(false);

					const errorBody = JSON.parse(JSON.stringify(error))
						.body as ProblemDetails;

					let title = errorBody.title;
					let message = errorBody.detail;

					if (errorBody.detail.includes("DuplicateUserName")) {
						title = "Username already taken :/";
						message = "Choose a different one.";
					}
					setAlert({
						mode: "error",
						title: title,
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
			<Divider textAlign={"left"}>
				<div className={"flex flex-row content-center gap-2 items-center"}>
					<BadgeIcon sx={{ fontSize: 40 }} />
					<p className="text-lg max-md:text-base">Personal Information</p>
				</div>
			</Divider>

			<TextField
				id="name"
				name="name"
				type="text"
				onChange={formik.handleChange}
				onBlur={formik.handleBlur}
				value={formik.values.name}
				label="Name"
				helperText={formik.errors.name ? formik.errors.name : "Required"}
				error={formik.touched.name && Boolean(formik.errors.name)}
			/>

			<TextField
				id="surname"
				name="surname"
				type="surname"
				onChange={formik.handleChange}
				onBlur={formik.handleBlur}
				value={formik.values.surname}
				label="Surname"
				helperText={formik.errors.surname ? formik.errors.surname : "Required"}
				error={formik.touched.surname && Boolean(formik.errors.surname)}
			/>

			<Divider className={"!mb-2.5"} />

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

			<Divider className={"!mb-2.5"} />

			<TextField
				id="password"
				name="password"
				type={isPasswordHidden ? "password" : "text"}
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
					endAdornment: (
						<IconButton
							onClick={() => setIsPasswordHidden((prevState) => !prevState)}
						>
							{isPasswordHidden ? <VisibilityOffIcon /> : <VisibilityIcon />}
						</IconButton>
					),
				}}
			/>

			<RegisterAlert alert={alert} setAlert={setAlert}></RegisterAlert>

			<LoadingButton
				variant="contained"
				type="submit"
				endIcon={<PersonAddIcon />}
				loading={isLoading}
			>
				Sign up
			</LoadingButton>
		</form>
	);
}

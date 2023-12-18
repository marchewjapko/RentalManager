"use client";

import React, { useState } from "react";
import { useFormik } from "formik";
import { Divider, IconButton, InputAdornment, TextField } from "@mui/material";
import BadgeIcon from "@mui/icons-material/Badge";
import { LoadingButton } from "@mui/lab";
import PersonAddIcon from "@mui/icons-material/PersonAdd";
import { registerSchema } from "@/app/Register/registerSchema";
import VisibilityOffIcon from "@mui/icons-material/VisibilityOff";
import VisibilityIcon from "@mui/icons-material/Visibility";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import LockIcon from "@mui/icons-material/Lock";
import { IInlineAlert } from "@/src/InlineAlert/IInlineAlert";
import InlineAlert from "@/src/InlineAlert/InlineAlert";
import SignUp from "@/app/Actions/SignUp";

export default function RegisterForm() {
	const [isLoading, setIsLoading] = useState(false);
	const [alert, setAlert] = useState<IInlineAlert | null>(null);
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
			SignUp({
				formData: {
					Name: values.name,
					Surname: values.surname,
					Password: values.password,
					UserName: values.userName,
				},
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

			<InlineAlert alert={alert} setAlert={setAlert}></InlineAlert>

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

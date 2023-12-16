import * as yup from "yup";

export const registerSchema = yup.object({
	name: yup
		.string()
		.trim()
		.required("Required")
		.matches(/^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻĆ ']+$/, "Invalid character"),
	surname: yup
		.string()
		.trim()
		.required("Required")
		.matches(/^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻĆ ']+$/, "Invalid character"),
	// image: yup.string().trim().required("required"),
	password: yup
		.string()
		.trim()
		.required("Required")
		.min(6, "Password too short")
		.max(100)
		.matches(/\d/gm, "Must contain at least one digit"),
	userName: yup
		.string()
		.trim()
		.required("Required")
		.matches(
			/^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻĆ 0-9\/.\/+\-\/%]*$/gm,
			"Invalid character",
		),
});

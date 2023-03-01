import * as yup from 'yup';

export const employeeValidationSchema = yup.object({
	name: yup
		.string()
		.trim()
		.required('required')
		.matches(/^[\p{L} ]+$/gu, 'onlyLettersAllowed'),
	surname: yup
		.string()
		.trim()
		.required('required')
		.matches(/^[\p{L} ]+$/gu, 'onlyLettersAllowed'),
});

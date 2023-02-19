import * as yup from 'yup';

export const employeeValidationSchema = yup.object({
	name: yup
		.string()
		.trim()
		.required('Required')
		.matches(/^[\p{L}| ]+$/gu, 'Only letters allowed'),
	surname: yup
		.string()
		.trim()
		.required('Required')
		.matches(/^[\p{L}| ]+$/gu, 'Only letters allowed'),
});

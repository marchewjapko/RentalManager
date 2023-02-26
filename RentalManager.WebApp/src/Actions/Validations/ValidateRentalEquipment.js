import * as yup from 'yup';

export const rentalEquipmentValidationSchema = yup.object({
	name: yup
		.string()
		.trim()
		.required('required')
		.matches(/^[\p{L}| ]+$/gu, 'onlyLettersAllowed'),
	price: yup
		.string()
		.trim()
		.required('required')
		.matches(/^[0-9]+$/, 'onlyNumbersAllowed'),
});

import * as yup from 'yup';

export const rentalEquipmentValidationSchema = yup.object({
	name: yup
		.string()
		.trim()
		.required('required')
		.matches(/^[\p{L} [0-9]+$/gu, 'onlyLettersAndNumbersAllowed'),
	price: yup
		.string()
		.trim()
		.required('required')
		.matches(/^[0-9]+$/, 'onlyNumbersAllowed'),
});

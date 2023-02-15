import { containsOnlyLetters, isNotNullOrEmpty } from './BasicValidation';

export function validateEmployeeName(value) {
	if (!isNotNullOrEmpty(value)) {
		return 'noValue';
	}
	if (!containsOnlyLetters(value)) {
		return 'invalidFormat';
	}
	return '';
}

export function validateEmployeeSurname(value) {
	if (!isNotNullOrEmpty(value)) {
		return 'noValue';
	}
	if (!containsOnlyLetters(value)) {
		return 'invalidFormat';
	}
	return '';
}

export default function ValidateEmployee(employee) {
	return [validateEmployeeSurname(employee.name), validateEmployeeName(employee.surname)].every(
		(x) => x === ''
	);
}

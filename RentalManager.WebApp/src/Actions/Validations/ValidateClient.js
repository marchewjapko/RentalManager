import {
	containsOnlyLetters,
	containsOnlyLettersAndNumbers,
	containsOnlyLettersNumbersAndWhiteSpace,
	isNotNullOrEmpty,
} from './BasicValidation';

export function validateName(value) {
	if (!isNotNullOrEmpty(value)) {
		return 'noValue';
	}
	if (!containsOnlyLetters(value)) {
		return 'invalidFormat';
	}
	return '';
}

export function validateSurname(value) {
	if (!isNotNullOrEmpty(value)) {
		return 'noValue';
	}
	if (!containsOnlyLetters(value)) {
		return 'invalidFormat';
	}
	return '';
}

export function validateIdCard(value) {
	if (!isNotNullOrEmpty(value)) {
		return '';
	}
	let regex = new RegExp('\\b([a-zA-Z]){3}[ ]?[0-9]{6}\\b');
	if (!regex.test(value)) {
		return 'invalidFormat';
	}
	return '';
}

export function validatePhoneNumber(value) {
	if (!isNotNullOrEmpty(value)) {
		return 'noValue';
	}
	let regexMobile = new RegExp('^([0-9]{3})?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$');
	let regexStationary = new RegExp('^([0-9]{2})?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$');
	if (!regexMobile.test(value) && !regexStationary.test(value)) {
		return 'invalidFormat';
	}
	return '';
}

export function validateEmail(value) {
	if (!isNotNullOrEmpty(value)) {
		return '';
	}
	let regex = new RegExp('^[\\w-.]+@([\\w-]+\\.)+[\\w-]{2,4}$');
	if (!regex.test(value)) {
		return 'invalidFormat';
	}
}

export function validateCity(value) {
	if (!isNotNullOrEmpty(value)) {
		return 'noValue';
	}
	if (!containsOnlyLetters(value)) {
		return 'invalidFormat';
	}
	return '';
}

export function validateStreet(value) {
	if (!isNotNullOrEmpty(value)) {
		return 'noValue';
	}
	if (!containsOnlyLettersNumbersAndWhiteSpace(value)) {
		return 'invalidFormat';
	}
	return '';
}

export default function ValidateClient(client) {
	return [
		validateName(client.name),
		validateSurname(client.surname),
		validateIdCard(client.idCard),
		validatePhoneNumber(client.phoneNumber),
		validateEmail(client.email),
		validateCity(client.city),
		validateStreet(client.street),
	].every((x) => x === '');
}

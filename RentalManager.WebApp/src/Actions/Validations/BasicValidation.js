export function isNotNullOrEmpty(value) {
	if (value === null) {
		return false;
	}
	if (typeof value !== 'number') {
		return value.trim().length !== 0;
	}
	return true;
}

export function containsOnlyLettersNumbersAndWhiteSpace(value) {
	return /^[0-9|\p{L}| ]+$/gu.test(value);
}

export function containsOnlyLetters(value) {
	return /^[\p{L}]+$/gu.test(value);
}

export function containsOnlyNumbers(value) {
	return /^[0-9]+$/.test(value);
}

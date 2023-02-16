import { containsOnlyNumbers, isNotNullOrEmpty } from './BasicValidation';
import dayjs from 'dayjs';

export function ValidateAmount(value) {
	if (!isNotNullOrEmpty(value)) {
		return 'noValue';
	}
	if (!containsOnlyNumbers(value)) {
		return 'invalidFormat';
	}
	return '';
}

export function ValidateDateFrom(value) {
	if (!isNotNullOrEmpty(value)) {
		return 'noValue';
	}
	if (!dayjs(value).isValid()) {
		return 'invalidFormat';
	}
	return '';
}

export function ValidateDateTo(value) {
	if (!isNotNullOrEmpty(value)) {
		return 'noValue';
	}
	if (!dayjs(value).isValid()) {
		return 'invalidFormat';
	}
	return '';
}

export function ValidatePayment(payment) {
	return [
		ValidateAmount(payment.amount),
		ValidateDateFrom(payment.from),
		ValidateDateTo(payment.to),
	].every((x) => x === '');
}

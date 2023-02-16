import ValidateClient from './ValidateClient';
import ValidateRentalEquipment from './ValidateRentalEquipment';
import ValidateEmployee from './ValidateEmployee';
import {
	containsOnlyLettersNumbersAndWhiteSpace,
	containsOnlyNumbers,
	isNotNullOrEmpty,
} from './BasicValidation';
import dayjs from 'dayjs';

export function ValidateTransportTo(value) {
	if (!isNotNullOrEmpty(value)) {
		return 'noValue';
	}
	if (!containsOnlyNumbers(value)) {
		return 'invalidFormat';
	}
	return '';
}

export function ValidateTransportFrom(value) {
	if (!isNotNullOrEmpty(value)) {
		return '';
	}
	if (!containsOnlyNumbers(value)) {
		return 'invalidFormat';
	}
	return '';
}

export function ValidateDeposit(value) {
	if (!isNotNullOrEmpty(value)) {
		return 'noValue';
	}
	if (!containsOnlyNumbers(value)) {
		return 'invalidFormat';
	}
	return '';
}

export function ValidateDateAdded(value) {
	if (!dayjs(value).isValid()) {
		return 'invalidFormat';
	}
	return '';
}

export function ValidateComment(value) {
	// if (!isNotNullOrEmpty(value)) {
	// 	return '';
	// }
	// if (!containsOnlyLettersNumbersAndWhiteSpace(value)) {
	// 	return 'invalidFormat';
	// }
	return '';
}

export default function ValidateRentalAgreement(rentalAgreement, validateClient, validatePayments) {
	if (validateClient && !ValidateClient(rentalAgreement.client)) {
		return false;
	}
	if (!rentalAgreement.rentalEquipment.map((x) => ValidateRentalEquipment(x)).every((x) => x)) {
		return false;
	}
	if (rentalAgreement.employee === null || !ValidateEmployee(rentalAgreement.employee)) {
		return false;
	}
	if (
		validatePayments &&
		(rentalAgreement.payments === null || rentalAgreement.payments.length === 0)
	) {
		return false;
	}
	return [
		ValidateTransportTo(rentalAgreement.transportTo),
		ValidateTransportFrom(rentalAgreement.transportFrom),
		ValidateDeposit(rentalAgreement.deposit),
		ValidateDateAdded(rentalAgreement.dateAdded),
		ValidateComment(rentalAgreement.comment),
	].every((x) => x === '');
}

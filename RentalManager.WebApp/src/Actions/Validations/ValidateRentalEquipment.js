import {
	containsOnlyLettersNumbersAndWhiteSpace,
	containsOnlyNumbers,
	isNotNullOrEmpty,
} from './BasicValidation';

export function validateEquipmentName(value) {
	if (!isNotNullOrEmpty(value)) {
		return 'noValue';
	}
	if (!containsOnlyLettersNumbersAndWhiteSpace(value)) {
		return 'invalidFormat';
	}
	return '';
}

export function validateEquipmentPrice(value) {
	if (!isNotNullOrEmpty(value)) {
		return 'noValue';
	}
	if (!containsOnlyNumbers(value)) {
		return 'invalidFormat';
	}
	return '';
}

export default function ValidateRentalEquipment(equipment) {
	return [validateEquipmentName(equipment.name), validateEquipmentPrice(equipment.price)].every(
		(x) => x === ''
	);
}

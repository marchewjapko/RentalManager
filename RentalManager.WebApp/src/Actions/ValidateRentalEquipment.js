export default function ValidateRentalEquipment(rentalEquipment) {
	let result = [];
	if (rentalEquipment.name.trim().length === 0) {
		result.push('noName');
	}
	if (rentalEquipment.price.length === 0) {
		result.push('noPrice');
	}
	return result;
}

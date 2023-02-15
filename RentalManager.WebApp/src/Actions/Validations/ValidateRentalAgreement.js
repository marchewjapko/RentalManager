import dayjs from 'dayjs';

export default function ValidateRentalAgreement(rentalAgreement) {
	let result = [];
	if (rentalAgreement.client.id === -1) {
		result.push('noClient');
	}
	if (rentalAgreement.deposit.length === 0) {
		result.push('noDeposit');
	}
	if (rentalAgreement.transportTo === undefined || rentalAgreement.transportTo.length === 0) {
		result.push('noTransportTo');
	}
	if (!rentalAgreement.dateAdded || rentalAgreement.dateAdded.length === 0) {
		result.push('noDateAdded');
	} else if (!dayjs(rentalAgreement.dateAdded).isValid()) {
		result.push('invalidDateAdded');
	}
	if (!rentalAgreement.employee) {
		result.push('noEmployee');
	}
	if (!rentalAgreement.rentalEquipment || rentalAgreement.rentalEquipment.length === 0) {
		result.push('noRentalEquipment');
	}
	if (rentalAgreement.payments === null || rentalAgreement.payments.length === 0) {
		result.push('noPayments');
	}
	return result;
}

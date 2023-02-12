export default function ValidateClient(client) {
	let result = [];
	if (client.name.trim().length === 0) {
		result.push('noName');
	}
	if (client.surname.trim().length === 0) {
		result.push('noSurname');
	}
	let idCardRegEx = new RegExp('\\b([a-zA-Z]){3}[ ]?[0-9]{6}\\b');
	if (!idCardRegEx.test(client.idCard) && client.idCard.trim().length !== 0) {
		result.push('invalidIdCard');
	}
	let phoneNumberRegEx = new RegExp(
		'^([0-9]{3})?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$'
	);
	if (client.phoneNumber.trim().length === 0) {
		result.push('noPhone');
	} else if (!phoneNumberRegEx.test(client.phoneNumber)) {
		result.push('invalidPhone');
	}
	let emailRegEx = new RegExp('^[\\w-.]+@([\\w-]+\\.)+[\\w-]{2,4}$');
	if (!emailRegEx.test(client.email) && client.email.trim().length !== 0) {
		result.push('invalidEmail');
	}
	if (client.city.trim().length === 0) {
		result.push('noCity');
	}
	if (client.street.trim().length === 0) {
		result.push('noStreet');
	}
	return result;
}

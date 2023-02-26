import { atom } from 'recoil';

export const rentalEquipmentAtom = atom({
	key: 'rentalEquipmentAtom',
	default: {
		id: -1,
		name: '',
		price: '',
		dateAdded: '',
	},
});

export const rentalEquipmentShowDeleteConfirmation = atom({
	key: 'rentalEquipmentShowDeleteConfirmation',
	default: false,
});

export const rentalEquipmentShowEditDialog = atom({
	key: 'rentalEquipmentShowEditDialog',
	default: false,
});

// export const rentalEquipmentSnackbar = atom({
// 	key: 'employeeSnackbar',
// 	default: {
// 		show: false,
// 		title: 'ups',
// 		severity: 'success',
// 	},
// });

export const forceRentalEquipmentRefresh = atom({
	key: 'forceRentalEquipmentRefresh',
	default: false,
});

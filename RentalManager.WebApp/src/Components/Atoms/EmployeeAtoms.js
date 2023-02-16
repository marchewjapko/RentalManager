import { atom } from 'recoil';
import { filterAgreements } from '../../Actions/RestAPI/RentalAgreementActions';
import { getAllEmployees } from '../../Actions/RestAPI/EmployeeActions';

export const employeeAtom = atom({
	key: 'employeeAtom',
	default: {
		id: -1,
		name: '',
		surname: '',
		dateAdded: '',
	},
});

export const employeeShowDeleteConfirmation = atom({
	key: 'employeeShowDeleteConfirmation',
	default: false,
});

export const employeeShowEditDialog = atom({
	key: 'employeeShowEditDialog',
	default: false,
});

export const employeeSnackbar = atom({
	key: 'employeeSnackbar',
	default: {
		show: false,
		title: 'ups',
		severity: 'success',
	},
});

export const forceEmployeeRefresh = atom({
	key: 'forceEmployeeRefresh',
	default: false,
});

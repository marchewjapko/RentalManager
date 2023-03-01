import { atom } from 'recoil';

export const globalSnackbar = atom({
	key: 'globalSnackbar',
	default: {
		show: false,
		title: 'ups',
		severity: 'success',
	},
});

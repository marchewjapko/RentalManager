import { atom } from 'recoil';

export const clientAtom = atom({
	key: 'clientAtom',
	default: {
		id: -1,
		name: '',
		surname: '',
		phoneNumber: '',
		email: '',
		idCard: '',
		city: '',
		street: '',
		streetNumber: '',
	},
});

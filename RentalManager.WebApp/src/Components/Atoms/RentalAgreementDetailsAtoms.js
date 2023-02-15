import { atom } from 'recoil';
import dayjs from 'dayjs';

export const rentalAgreementDetailsAtom = atom({
	key: 'rentalAgreementDetailsAtom',
	default: {
		id: 0,
		isActive: true,
		employee: null,
		rentalEquipment: [],
		comment: '',
		deposit: '',
		transportFrom: null,
		transportTo: '',
		payments: [],
		dateAdded: dayjs(),
	},
});

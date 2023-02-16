import { DefaultValue, selector } from 'recoil';
import { rentalAgreementDetailsAtom } from './RentalAgreementDetailsAtoms';
import { clientAtom } from './ClientAtoms';

export const rentalAgreementAtom = selector({
	key: 'rentalAgreementAtom',
	get: ({ get }) => {
		const rentalAgreementDetails = get(rentalAgreementDetailsAtom);
		const client = get(clientAtom);
		return { ...rentalAgreementDetails, client: client };
	},
	set: ({ set }, value) => {
		if (value instanceof DefaultValue) {
			set(rentalAgreementDetailsAtom, value);
			set(clientAtom, value);
			return;
		}
		set(rentalAgreementDetailsAtom, value.rentalAgreementDetails);
		set(clientAtom, value.client);
	},
});

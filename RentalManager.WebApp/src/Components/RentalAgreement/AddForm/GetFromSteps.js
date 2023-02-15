import Clients from '../../Clients/Clients';
import RentalAgreementAgreementDetails from '../RentalAgreementAgreementDetails';
import Payments from '../../Payments/Payments';
import * as React from 'react';

export default function GetFormSteps() {
	return [
		{
			label: 'Choose client',
			content: <Clients isCheckable={true} />,
		},
		{
			label: 'Fill the details',
			content: <RentalAgreementAgreementDetails mode={'post'} />,
		},
		{
			label: 'Add payment',
			content: <Payments mode={'post'} />,
		},
	];
}

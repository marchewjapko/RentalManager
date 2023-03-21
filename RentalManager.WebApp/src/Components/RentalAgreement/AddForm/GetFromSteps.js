import ClientsOLD from '../../Clients/ClientsOLD';
import RentalAgreementAgreementDetails from '../RentalAgreementAgreementDetails';
import Payments from '../../Payments/Payments';
import * as React from 'react';

export default function GetFormSteps() {
	return [
		{
			label: 'chooseClient',
			content: <ClientsOLD isCheckable={true} />,
		},
		{
			label: 'fillTheDetails',
			content: <RentalAgreementAgreementDetails mode={'post'} />,
		},
		{
			label: 'addPayment',
			content: <Payments mode={'post'} />,
		},
	];
}

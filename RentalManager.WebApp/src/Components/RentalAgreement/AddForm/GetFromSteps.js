import Clients from '../../Clients/Clients';
import RentalAgreementAgreementDetails from '../RentalAgreementAgreementDetails';
import Payments from '../../Payments/Payments';
import * as React from 'react';
import { useTranslation } from 'react-i18next';

export default function GetFormSteps() {
	return [
		{
			label: 'chooseClient',
			content: <Clients isCheckable={true} />,
		},
		{
			label: 'fillTheDetails',
			content: <RentalAgreementAgreementDetails mode={'post'} />,
		},
		{
			label: 'paymentTranslation',
			content: <Payments mode={'post'} />,
		},
	];
}

import axios from 'axios';
import { RentalAgreementMock } from '../../Mocks/RentalAgreementMock';

const client = axios.create({
	baseURL: process.env.REACT_APP_API_URL,
});

export const addPayment = (id, payment) =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve('Payment added!'),
						console.log('Added payment:', payment)
					),
					2500
				);
		  })
		: client.post(
				`/Payment/`,
				{
					method: payment.method,
					amount: payment.amount,
					from: payment.from,
					to: payment.to,
				},
				{
					params: {
						rentalAgreementId: id,
					},
				}
		  );

export const getPayment = (rentalAgreementId) =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve(
							Object.values(
								RentalAgreementMock.filter(
									(x) => x.id === id
								)[0].payments
							)
						),
						console.log('Got all payments')
					),
					2500
				);
		  })
		: client.get('/Payment', {
				params: {
					RentalAgreementId: rentalAgreementId,
				},
		  });

export const deletePayment = (id) =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve('Payment deleted!'),
						console.log('Deleted payment:', id)
					),
					2500
				);
		  })
		: client.delete(`/Payment/${id}`);
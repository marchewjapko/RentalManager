import { RentalAgreementMock } from '../../Mocks/RentalAgreementMock';
import dayjs from 'dayjs';
import axios from 'axios';

const client = axios.create({
	baseURL: process.env.REACT_APP_API_URL,
});

export const getAgreement = (id) =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve(Object.values(RentalAgreementMock.filter((x) => x.id === id))),
						console.log('Got all agreements')
					),
					2500
				);
		  })
		: client.get(`/RentalAgreement/${id}`);

export const getAllAgreements = () =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve(Object.values(RentalAgreementMock)),
						console.log('Got all agreements')
					),
					2500
				);
		  })
		: client.get('/RentalAgreement');

export const updateRentalAgreement = (rentalAgreement) =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve('Rental agreement updated!'),
						console.log('Updated agreement equipment:', rentalAgreement)
					),
					2500
				);
		  })
		: client.put(`/RentalAgreement/${rentalAgreement.id}`, {
				employeeId: rentalAgreement.employee.id,
				isActive: true,
				clientId: rentalAgreement.client.id,
				rentalEquipmentIds: rentalAgreement.rentalEquipment.map((x) => x.id),
				comment: rentalAgreement.comment,
				deposit: rentalAgreement.deposit,
				transportFrom: rentalAgreement.transportFrom,
				transportTo: rentalAgreement.transportTo,
				dateAdded: rentalAgreement.dateAdded,
		  });

export const deleteRentalAgreement = (id) =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve('Rental agreement deleted!'),
						console.log('Deleted agreement equipment:', id)
					),
					2500
				);
		  })
		: client.delete(`/RentalAgreement/${id}`);

export const addRentalAgreement = (rentalAgreement) =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve('Rental agreement added!'),
						console.log('Added rental agreement:', rentalAgreement)
					),
					2500
				);
		  })
		: client.post('/RentalAgreement', {
				employeeId: rentalAgreement.employee.id,
				isActive: true,
				clientId: rentalAgreement.client.id,
				rentalEquipmentIds: rentalAgreement.rentalEquipment.map((x) => x.id),
				comment: rentalAgreement.comment,
				deposit: rentalAgreement.deposit,
				transportFrom: rentalAgreement.transportFrom,
				transportTo: rentalAgreement.transportTo,
				dateAdded: rentalAgreement.dateAdded,
				payments: rentalAgreement.payments,
		  });

export const filterAgreements = (searchParams) =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve(
							Object.values(
								RentalAgreementMock.filter(
									(x) =>
										x.client.surname
											.toLowerCase()
											.includes(searchParams.surname.toLowerCase()) &&
										x.client.phone
											.toLowerCase()
											.includes(searchParams.phone.toLowerCase()) &&
										x.client.city
											.toLowerCase()
											.includes(searchParams.city.toLowerCase()) &&
										x.client.street
											.toLowerCase()
											.includes(searchParams.street.toLowerCase()) &&
										((searchParams.onlyUnpaid &&
											dayjs(x.validUntil).diff(dayjs(), 'day') < 0) ||
											!searchParams.onlyUnpaid) &&
										(x.isActive === searchParams.onlyActive ||
											!searchParams.onlyActive)
								)
							)
						),
						console.log('Filtered agreements, params:', searchParams)
					),
					2500
				);
		  })
		: client.get('/RentalAgreement', {
				params: searchParams,
		  });

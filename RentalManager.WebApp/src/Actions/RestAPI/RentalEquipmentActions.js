import { RentalEquipmentMock } from '../../Mocks/RentalEquipmentMock';
import axios from 'axios';

const client = axios.create({
	baseURL: process.env.REACT_APP_API_URL,
});

export const getAllRentalEquipment = () =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve(Object.values(RentalEquipmentMock)),
						console.log('Got all rental equipment')
					),
					2500
				);
		  })
		: client.get('/RentalEquipment');

export const addRentalEquipment = (rentalEquipment) =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve('Rental equipment added!'),
						console.log('Added rental equipment:', rentalEquipment)
					),
					2500
				);
		  })
		: client.post('/RentalEquipment', {
				name: rentalEquipment.name,
				price: rentalEquipment.price,
		  });

export const filterRentalEquipment = (name) =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve(
							Object.values(
								RentalEquipmentMock.filter((x) =>
									x.name.toLowerCase().includes(name.toLowerCase())
								)
							)
						),
						console.log('Filtered rental equipments')
					),
					2500
				);
		  })
		: client.get('/RentalEquipment', {
				params: {
					name: name,
				},
		  });

export const updateRentalEquipment = (rentalEquipment) =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve('Rental equipment updated!'),
						console.log('Updated rental equipment:', rentalEquipment)
					),
					2500
				);
		  })
		: client.put(`/RentalEquipment/${rentalEquipment.id}`, {
				name: rentalEquipment.name,
				price: rentalEquipment.price,
		  });

export const deleteRentalEquipment = (id) =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve('Rental equipment deleted!'),
						console.log('Deleted rental equipment:', id)
					),
					2500
				);
		  })
		: client.delete(`/RentalEquipment/${id}`);

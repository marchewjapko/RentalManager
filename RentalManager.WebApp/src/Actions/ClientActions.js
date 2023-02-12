import axios from 'axios';
import { ClientsMock } from '../Mocks/ClientsMock';

const client = axios.create({
	baseURL: process.env.REACT_APP_API_URL,
});

export const getAllClients = () =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve(Object.values(ClientsMock)),
						console.log('Got all clients')
					),
					2500
				);
		  })
		: client.get('/Client');

export const addClient = (newClient) =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve(Object.values(ClientsMock)),
						console.log('Got all clients')
					),
					2500
				);
		  })
		: client.post('/Client', {
				name: newClient.name,
				surname: newClient.surname,
				phoneNumber: newClient.phoneNumber,
				email: newClient.email,
				idCard: newClient.idCard,
				city: newClient.city,
				street: newClient.street,
		  });

export const updateClient = (newClient) =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve('Client updated!'),
						console.log('Updated client:', client)
					),
					2500
				);
		  })
		: client.put(`/Client/${newClient.id}`, {
				name: newClient.name,
				surname: newClient.surname,
				phoneNumber: newClient.phoneNumber,
				email: newClient.email,
				idCard: newClient.idCard,
				city: newClient.city,
				street: newClient.street,
		  });

export const deleteClient = (id) =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve('Client deleted!'),
						console.log('Deleted client:', id)
					),
					2500
				);
		  })
		: client.delete(`/Client/${id}`);

export const filterClients = (searchParams) =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve(
							Object.values(
								ClientsMock.filter(
									(x) =>
										x.surname
											.toLowerCase()
											.includes(
												searchParams.surname.toLowerCase()
											) &&
										x.phone
											.toLowerCase()
											.includes(
												searchParams.phone.toLowerCase()
											) &&
										x.city
											.toLowerCase()
											.includes(
												searchParams.city.toLowerCase()
											) &&
										x.street
											.toLowerCase()
											.includes(
												searchParams.street.toLowerCase()
											)
								)
							)
						),
						console.log('Filtered clients, params:', searchParams)
					),
					2500
				);
		  })
		: client.get('/Client', {
				params: searchParams,
		  });

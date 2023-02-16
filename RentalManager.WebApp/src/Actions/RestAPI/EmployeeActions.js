import { EmployeesMock } from '../../Mocks/EmployeesMock';
import axios from 'axios';

const client = axios.create({
	baseURL: process.env.REACT_APP_API_URL,
});

export const getAllEmployees = () =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (resolve(Object.values(EmployeesMock)), console.log('Got all employees')),
					2500
				);
		  })
		: client.get('/Employee');

export const addEmployee = (employee) =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (resolve('Employee added!'), console.log('Added employee:', employee)),
					2500
				);
		  })
		: client.post('/Employee', {
				name: employee.name,
				surname: employee.surname,
		  });

export const filterEmployees = (name) =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve(
							Object.values(
								EmployeesMock.filter((x) =>
									x.surname.toLowerCase().includes(name.toLowerCase())
								)
							)
						),
						console.log('Filtered employees')
					),
					2500
				);
		  })
		: client.get('/Employee', {
				params: {
					name: name,
				},
		  });

export const updateEmployee = (employee) =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (
						resolve('Employee updated!'), console.log('Updated employee:', employee)
					),
					2500
				);
		  })
		: client.put(`/Employee/${employee.id}`, {
				name: employee.name,
				surname: employee.surname,
		  });

export const deleteEmployee = (id) =>
	process.env.REACT_APP_Mock_Version === 'true'
		? new Promise((resolve) => {
				setTimeout(
					() => (resolve('Employee deleted!'), console.log('Deleted employee:', id)),
					2500
				);
		  })
		: client.delete(`/Employee/${id}`);

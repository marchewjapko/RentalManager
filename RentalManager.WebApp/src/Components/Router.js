import Header from './Header/Header';
import { createBrowserRouter, Outlet } from 'react-router-dom';
import Dashboard from './Dashboard/Dashboard';
import RentalAgreement from './RentalAgreement/RentalAgreement';
import AddRentalEquipmentForm from './RentalAgreement/AddForm/AddRentalEquipmentForm';
import RentalEquipment from './RentalEquipment/RentalEquipment';
import Employees from './Employees/Employees';
import React from 'react';

const AppLayout = () => (
	<>
		<Header />
		<Outlet />
	</>
);

export default function GetRouter() {
	return new createBrowserRouter([
		{
			element: <AppLayout />,
			children: [
				{
					path: '/',
					element: <Dashboard />,
				},
				{
					path: '/rental-agreement',
					element: <RentalAgreement />,
				},
				{
					path: '/add-rental-agreement',
					element: <AddRentalEquipmentForm />,
				},
				{
					path: '/rental-equipment',
					element: <RentalEquipment />,
				},
				{
					path: '/employees',
					element: <Employees />,
				},
			],
		},
	]);
}

import Header from './Components/Header/Header';
import { createBrowserRouter, createHashRouter, Outlet } from 'react-router-dom';
import Dashboard from './Components/Dashboard/Dashboard';
import RentalAgreement from './Components/RentalAgreement/RentalAgreement';
import AddRentalEquipmentForm from './Components/RentalAgreement/AddForm/AddRentalEquipmentForm';
import RentalEquipment from './Components/RentalEquipment/RentalEquipment';
import Employees from './Components/Employees/Employees';
import React from 'react';

const AppLayout = () => (
	<>
		<Header />
		<Outlet />
	</>
);

export default function GetRouter() {
	if (window.cordova) {
		return new createHashRouter([
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
	} else {
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
}

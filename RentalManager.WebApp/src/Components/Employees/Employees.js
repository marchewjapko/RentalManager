import * as React from 'react';
import { Button, Skeleton, Typography } from '@mui/material';
import { getAllEmployees } from '../../Actions/RestAPI/EmployeeActions';
import AddCircleRoundedIcon from '@mui/icons-material/AddCircleRounded';
import { useTranslation } from 'react-i18next';
import EmployeeCard from './EmployeeCard';
import DeleteEmployeeDialog from './DeleteEmployeeDialog';
import {
	employeeAtom,
	employeeShowDeleteConfirmation,
	employeeShowEditDialog,
	forceEmployeeRefresh,
} from '../Atoms/EmployeeAtoms';
import { DefaultValue, useRecoilState, useRecoilValue, useSetRecoilState } from 'recoil';
import './Employee.css';
import EmployeeForm from './EmployeeForm';
import { useEffect, useState } from 'react';
import { AnimatePresence, motion } from 'framer-motion';
import { Masonry } from '@mui/lab';

function GetSkeletonCards() {
	const skeletonArray = Array(5).fill(0);
	return (
		<>
			{skeletonArray.map((x, index) => (
				<motion.div
					key={index}
					variants={item}
					custom={[index, skeletonArray.length]}
					initial="initial"
					animate="animate"
					exit="exit"
				>
					<Skeleton className={'employee-card-skeleton'} />
				</motion.div>
			))}
		</>
	);
}

function GetNormalCards({ data }) {
	return (
		<>
			{data.map((employee, index) => (
				<motion.div
					key={index}
					variants={item}
					custom={[index, data.length]}
					initial="initial"
					animate="animate"
					exit="exit"
				>
					<EmployeeCard employee={employee} />
				</motion.div>
			))}
		</>
	);
}

function getChildDelay(index, size) {
	if (size <= 31) {
		return index * 0.05;
	} else {
		return index * (1.5 / (size - 1));
	}
}

const item = {
	exit: { opacity: 0 },
	initial: {
		opacity: 0,
	},
	animate: (i) => ({
		opacity: 1,
		transition: {
			delay: getChildDelay(i[0], i[1]),
		},
	}),
};

export default function Employees() {
	const [data, setData] = useState([]);
	const [isLoading, setIsLoading] = useState(true);
	const refreshFunction = useRecoilValue(forceEmployeeRefresh);
	const [showEditDialog, setShowEditDialog] = useRecoilState(employeeShowEditDialog);
	const showDeleteDialog = useRecoilValue(employeeShowDeleteConfirmation);
	const setEmployee = useSetRecoilState(employeeAtom);
	const { t } = useTranslation(['generalTranslation', 'employeeTranslation']);

	useEffect(() => {
		setIsLoading(true);
		getAllEmployees()
			.then((result) => {
				if (result.hasOwnProperty('data')) {
					setData(result.data);
				} else {
					setData(result);
				}
				setIsLoading(false);
			})
			.catch((error) => {
				console.log(error);
			});
	}, [refreshFunction]);

	const handleAddClick = () => {
		setEmployee(new DefaultValue());
		setShowEditDialog(true);
	};

	return (
		<>
			{showDeleteDialog && <DeleteEmployeeDialog />}
			{showEditDialog && <EmployeeForm />}
			<div className={'employee-container'}>
				<Typography variant={'h3'}>
					{t('employees', { ns: 'employeeTranslation' })}
				</Typography>
				<Button
					startIcon={<AddCircleRoundedIcon />}
					variant={'contained'}
					onClick={handleAddClick}
					disabled={isLoading}
				>
					{t('add')}
				</Button>
				<div className="employee-card-container">
					<AnimatePresence mode="wait">
						{isLoading ? (
							<GetSkeletonCards key={1} />
						) : (
							<GetNormalCards data={data} key={2} />
						)}
					</AnimatePresence>
				</div>
			</div>
		</>
	);
}

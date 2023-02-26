import * as React from 'react';
import { Button, Skeleton, Typography } from '@mui/material';
import AddCircleRoundedIcon from '@mui/icons-material/AddCircleRounded';
import { useTranslation } from 'react-i18next';
import DeleteRentalEquipmentDialog from './DeleteRentalEquipmentDialog';
import { DefaultValue, useRecoilState, useRecoilValue, useSetRecoilState } from 'recoil';
import RentalEquipmentForm from './RentalEquipmentForm';
import { useEffect, useState } from 'react';
import { AnimatePresence, motion } from 'framer-motion';
import {
	forceRentalEquipmentRefresh,
	rentalEquipmentAtom,
	rentalEquipmentShowDeleteConfirmation,
	rentalEquipmentShowEditDialog,
} from '../Atoms/RentaLEquipmentAtoms';
import { getAllRentalEquipment } from '../../Actions/RestAPI/RentalEquipmentActions';
import RentalEquipmentCard from './RentalEquipmentCard';
import './RentalEquipment.css';

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
					<Skeleton className={'rental-equipment-card-skeleton'} />
				</motion.div>
			))}
		</>
	);
}

function GetNormalCards({ data }) {
	return (
		<>
			{data.map((rentalEquipment, index) => (
				<motion.div
					key={index}
					variants={item}
					custom={[index, data.length]}
					initial="initial"
					animate="animate"
					exit="exit"
				>
					<RentalEquipmentCard rentalEquipment={rentalEquipment} />
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

export default function RentalEquipment() {
	const [data, setData] = useState([]);
	const [isLoading, setIsLoading] = useState(true);
	const refreshFunction = useRecoilValue(forceRentalEquipmentRefresh);
	const [showEditDialog, setShowEditDialog] = useRecoilState(rentalEquipmentShowEditDialog);
	const showDeleteDialog = useRecoilValue(rentalEquipmentShowDeleteConfirmation);
	const setRentalEquipment = useSetRecoilState(rentalEquipmentAtom);
	const { t } = useTranslation(['generalTranslation', 'equipmentTranslation']);

	useEffect(() => {
		setIsLoading(true);
		getAllRentalEquipment()
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
		setRentalEquipment(new DefaultValue());
		setShowEditDialog(true);
	};

	return (
		<>
			{showDeleteDialog && <DeleteRentalEquipmentDialog />}
			{showEditDialog && <RentalEquipmentForm />}
			<div className={'rental-equipment-container'}>
				<Typography variant={'h3'}>
					{t('equipment', { ns: 'equipmentTranslation' })}
				</Typography>
				<Button
					startIcon={<AddCircleRoundedIcon />}
					variant={'contained'}
					onClick={handleAddClick}
					disabled={isLoading}
				>
					{t('add')}
				</Button>
				<div className="rental-equipment-card-container">
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

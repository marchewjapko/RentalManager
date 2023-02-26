import { Button, Card, Fade, Typography } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import { useRef, useState } from 'react';
import { useSetRecoilState } from 'recoil';
import { RandomGradients } from '../Shared/RandomGradients';
import * as React from 'react';
import {
	rentalEquipmentAtom,
	rentalEquipmentShowDeleteConfirmation,
	rentalEquipmentShowEditDialog,
} from '../Atoms/RentaLEquipmentAtoms';

export default function RentalEquipmentCard({ rentalEquipment }) {
	const [isMouseOver, setIsMouseOver] = useState(false);
	const setShowDeleteDialog = useSetRecoilState(rentalEquipmentShowDeleteConfirmation);
	const setShowEditDialog = useSetRecoilState(rentalEquipmentShowEditDialog);
	const setDialogEmployee = useSetRecoilState(rentalEquipmentAtom);
	const randomIndex = useRef(Math.floor(Math.random() * 20));

	const handleDeleteClick = () => {
		setDialogEmployee(rentalEquipment);
		setShowDeleteDialog(true);
	};
	const handleEditClick = () => {
		setDialogEmployee(rentalEquipment);
		setShowEditDialog(true);
	};
	const backdropStyle = {
		background: RandomGradients.at(randomIndex.current),
	};

	return (
		<div className={'rental-equipment-card'}>
			<div
				className={'rental-equipment-backdrop'}
				style={backdropStyle}
				onMouseLeave={() => setIsMouseOver(false)}
				onMouseEnter={() => setIsMouseOver(true)}
			>
				<Fade in={isMouseOver} style={{ transitionDelay: '50ms' }} timeout={400}>
					<Button
						onClick={handleEditClick}
						startIcon={<EditIcon />}
						variant={'contained'}
						color={'success'}
					>
						Edit
					</Button>
				</Fade>
				<Fade in={isMouseOver} style={{ transitionDelay: '50ms' }} timeout={400}>
					<Button
						onClick={handleDeleteClick}
						startIcon={<DeleteIcon />}
						variant={'contained'}
						color={'error'}
					>
						Delete
					</Button>
				</Fade>
			</div>
			<Card className={'rental-equipment-content'}>
				<Typography
					fontWeight={700}
					variant={'h6'}
					className={'rental-equipment-name-typography'}
				>
					{rentalEquipment.name}
				</Typography>
				<Typography
					fontWeight={350}
					variant={'body1'}
					className={'rental-equipment-name-typography'}
				>
					{rentalEquipment.price + ' zł'}
				</Typography>
			</Card>
		</div>
	);
}

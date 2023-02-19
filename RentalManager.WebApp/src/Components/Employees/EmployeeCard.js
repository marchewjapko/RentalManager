import { Avatar, Button, Card, Fade, Typography } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import { useRef, useState } from 'react';
import { useSetRecoilState } from 'recoil';
import {
	employeeAtom,
	employeeShowDeleteConfirmation,
	employeeShowEditDialog,
} from '../Atoms/EmployeeAtoms';
import { RandomGradients } from '../Shared/RandomGradients';
import * as React from 'react';

export default function ({ employee }) {
	const [isMouseOver, setIsMouseOver] = useState(false);
	const setShowDeleteDialog = useSetRecoilState(employeeShowDeleteConfirmation);
	const setShowEditDialog = useSetRecoilState(employeeShowEditDialog);
	const setDialogEmployee = useSetRecoilState(employeeAtom);
	const randomIndex = useRef(Math.floor(Math.random() * 20));

	const handleDeleteClick = () => {
		setDialogEmployee(employee);
		setShowDeleteDialog(true);
	};
	const handleEditClick = () => {
		setDialogEmployee(employee);
		setShowEditDialog(true);
	};
	const backdropStyle = {
		background: RandomGradients.at(randomIndex.current),
	};

	return (
		<div className={'employee-card'}>
			<div
				className={'employee-backdrop'}
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
			<Card className={'employee-content'}>
				<Avatar
					variant="rounded"
					src="https://mui.com/static/images/avatar/1.jpg"
					sx={{ width: 70, height: 70, borderRadius: '10px' }}
				/>
				<Typography fontWeight={700} variant={'h6'} className={'employee-name-typography'}>
					{employee.name + ' ' + employee.surname}
				</Typography>
			</Card>
		</div>
	);
}

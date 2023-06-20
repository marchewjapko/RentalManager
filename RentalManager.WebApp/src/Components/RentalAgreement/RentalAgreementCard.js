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
import { useTranslation } from 'react-i18next';
import { useTheme } from '@mui/material/styles';
import dayjs from 'dayjs';
require('dayjs/locale/pl');
require('dayjs/locale/en');

export default function RentalAgreementCard({ rentalAgreement }) {
	const [isMouseOver, setIsMouseOver] = useState(false);
	const setShowDeleteDialog = useSetRecoilState(rentalEquipmentShowDeleteConfirmation);
	const setShowEditDialog = useSetRecoilState(rentalEquipmentShowEditDialog);
	const setDialogEmployee = useSetRecoilState(rentalEquipmentAtom);
	const randomIndex = useRef(Math.floor(Math.random() * 20));
	const theme = useTheme();
	const { t, i18n } = useTranslation(['generalTranslation']);

	const handleDeleteClick = () => {
		setDialogEmployee(rentalAgreement);
		setShowDeleteDialog(true);
	};
	const handleEditClick = () => {
		setDialogEmployee(rentalAgreement);
		setShowEditDialog(true);
	};
	const backdropStyle = {
		background: RandomGradients.at(randomIndex.current),
	};

	const getRowColor = (row) => {
		const lastPayment = row.payments
			.slice()
			.sort((a, b) => (dayjs(a.to).isAfter(dayjs(b.to)) ? 1 : -1))
			.at(-1);
		if (!row.isActive) {
			return theme.palette.text.disabled;
		}
		if (dayjs(lastPayment.to).diff(dayjs(), 'day') < 0) {
			return theme.palette.error.dark;
		} else {
			return theme.palette.background.paper;
		}
	};

	return (
		<div className={'rental-agreement-card'}>
			<div
				className={'rental-agreement-backdrop'}
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
						{t('edit')}
					</Button>
				</Fade>
				<Fade in={isMouseOver} style={{ transitionDelay: '50ms' }} timeout={400}>
					<Button
						onClick={handleDeleteClick}
						startIcon={<DeleteIcon />}
						variant={'contained'}
						color={'error'}
					>
						{t('delete')}
					</Button>
				</Fade>
			</div>
			<Card
				className={'rental-agreement-content'}
				style={{ backgroundColor: getRowColor(rentalAgreement) }}
			>
				<Typography
					fontWeight={'bold'}
					variant={'h4'}
					className={'rental-agreement-client-typography'}
				>
					{rentalAgreement.client.name + ' ' + rentalAgreement.client.surname}
				</Typography>
				<div
					style={{
						display: 'flex',
						flexWrap: 'wrap',
						gap: '0 5px',
					}}
				>
					<Typography fontWeight={'bold'} variant={'body1'}>
						{rentalAgreement.client.city}
					</Typography>
					<Typography fontWeight={'normal'} variant={'body1'}>
						{rentalAgreement.client.street}
					</Typography>
				</div>
				<Typography fontWeight={'normal'} variant={'body1'}>
					Equipment:
				</Typography>
				<ul style={{ margin: '0' }}>
					{rentalAgreement.rentalEquipment.map((rentalEquipment, index) => (
						<li key={index}>
							{rentalEquipment.name + ': ' + rentalEquipment.price + 'zł'}
						</li>
					))}
				</ul>
				<Typography
					fontWeight={'normal'}
					variant={'body2'}
					className={'rental-agreement-date-typography'}
				>
					{dayjs(rentalAgreement.dateAdded).locale(i18n.language).format('DD MMMM YYYY')}
				</Typography>
			</Card>
		</div>
	);
}

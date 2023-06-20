import { DefaultValue, useRecoilState, useSetRecoilState } from 'recoil';
import * as React from 'react';
import { useState } from 'react';
import { filterAgreements } from '../../Actions/RestAPI/RentalAgreementActions';
import {
	Button,
	Dialog,
	DialogActions,
	DialogContent,
	DialogTitle,
	Skeleton,
	Slide,
	Stack,
	Typography,
} from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import CancelIcon from '@mui/icons-material/Cancel';
import { useTheme } from '@mui/material/styles';
import { useTranslation } from 'react-i18next';
import {
	forceRentalEquipmentRefresh,
	rentalEquipmentAtom,
	rentalEquipmentShowDeleteConfirmation,
} from '../Atoms/RentaLEquipmentAtoms';
import { deleteRentalEquipment } from '../../Actions/RestAPI/RentalEquipmentActions';
import { globalSnackbar } from '../Atoms/GeneralAtoms';

const Transition = React.forwardRef(function Transition(props, ref) {
	return <Slide direction="down" ref={ref} {...props} />;
});

export default function DeleteRentalEquipmentDialog() {
	const [rentalEquipment, setRentalEquipment] = useRecoilState(rentalEquipmentAtom);
	const [showDialog, setShowDialog] = useState(true);
	const setShowDialogAtom = useSetRecoilState(rentalEquipmentShowDeleteConfirmation);
	const setSnackbar = useSetRecoilState(globalSnackbar);
	const [forceRefresh, setForceRefresh] = useRecoilState(forceRentalEquipmentRefresh);
	const [numberOfAgreements, setNumberOfAgreements] = useState(0);
	const [isLoading, setIsLoading] = useState(true);
	const { t } = useTranslation(['equipmentTranslation', 'generalTranslation']);
	const theme = useTheme();

	React.useEffect(() => {
		const searchParams = { rentalEquipmentId: rentalEquipment.id };
		filterAgreements(searchParams)
			.then((result) => {
				if (result.hasOwnProperty('data')) {
					setNumberOfAgreements(result.data.length);
				} else {
					setNumberOfAgreements(result.length);
				}
				setIsLoading(false);
			})
			.catch(() => {
				setSnackbar({
					show: true,
					title: 'An error has occurred',
					severity: 'error',
				});
			});
	}, []);

	const handleCloseDialog = () => {
		setShowDialog(false);
		setTimeout(() => {
			setRentalEquipment(new DefaultValue());
			setShowDialogAtom(false);
		}, 300);
	};
	const handleDeleteClick = () => {
		if (numberOfAgreements === 0) {
			deleteRentalEquipment(rentalEquipment.id)
				.then(() => {
					setSnackbar({
						show: true,
						title: t('rentalEquipmentDeleted'),
						severity: 'success',
					});
					setRentalEquipment(new DefaultValue());
					handleCloseDialog();
					setForceRefresh(!forceRefresh);
				})
				.catch(() => {
					setSnackbar({
						show: true,
						title: t('errorHasOccurred', { ns: 'generalTranslation' }),
						severity: 'error',
					});
				});
		}
	};

	function getDialogContent() {
		if (isLoading) {
			return (
				<Stack direction={'column'}>
					<Skeleton variant="text" sx={{ fontSize: '1em' }} />
					<Skeleton variant="text" sx={{ fontSize: '1em' }} />
				</Stack>
			);
		} else if (numberOfAgreements > 0) {
			return (
				<>
					<Typography>
						{t('equipment')}{' '}
						<i>
							{' '}
							<b> {rentalEquipment.name} </b>{' '}
						</i>{' '}
						{t('isAssignedTo')} <b>{numberOfAgreements}</b> {t('agreement(s)')}
					</Typography>
					<Typography>{t('delete these agreements to proceed')}</Typography>
				</>
			);
		}
		return (
			<Typography>
				{t('are you sure you want to delete equipment')}
				<i>
					<b> {rentalEquipment.name} </b>
				</i>
				?
			</Typography>
		);
	}

	return (
		<Dialog
			onClose={handleCloseDialog}
			open={showDialog}
			TransitionComponent={Transition}
			maxWidth={'xs'}
			PaperProps={{
				sx: `border: 2px solid ${theme.palette.primary.main}`,
			}}
			slotProps={{
				backdrop: {
					timeout: 250,
					sx: 'background-color: rgba(0,0,0,0); backdrop-filter: blur(5px);',
				},
			}}
		>
			<DialogTitle>{t('deleteRentalEquipment')}</DialogTitle>
			<DialogContent dividers>{getDialogContent()}</DialogContent>
			<DialogActions>
				<Button
					autoFocus
					endIcon={<CancelIcon />}
					variant={'text'}
					onClick={handleCloseDialog}
				>
					{t('cancel', { ns: 'generalTranslation' })}
				</Button>
				<Button
					onClick={handleDeleteClick}
					startIcon={<DeleteIcon />}
					variant={'contained'}
					color={'error'}
					disabled={numberOfAgreements !== 0 || isLoading}
				>
					{t('delete', { ns: 'generalTranslation' })}
				</Button>
			</DialogActions>
		</Dialog>
	);
}

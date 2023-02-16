import * as React from 'react';
import ValidateRentalEquipment, {
	validateEquipmentName,
	validateEquipmentPrice,
} from '../../Actions/Validations/ValidateRentalEquipment';
import {
	addRentalEquipment,
	deleteRentalEquipment,
	updateRentalEquipment,
} from '../../Actions/RestAPI/RentalEquipmentActions';
import {
	Backdrop,
	Button,
	CircularProgress,
	DialogContent,
	Stack,
	Typography,
} from '@mui/material';
import DoneIcon from '@mui/icons-material/Done';
import CancelIcon from '@mui/icons-material/Cancel';
import DeleteIcon from '@mui/icons-material/Delete';
import Grid from '@mui/material/Unstable_Grid2';
import ConstructionIcon from '@mui/icons-material/Construction';
import ValidatedTextField from '../Shared/ValidatedTextField';
import { useTranslation } from 'react-i18next';

export default function RentalEquipmentDialog({
	handleCancelDialog,
	rentalEquipment,
	handleDialogSuccess,
	mode,
}) {
	const [rentalEquipmentDialog, setRentalEquipmentDialog] = React.useState(rentalEquipment);
	const [isLoading, setIsLoading] = React.useState(false);
	const { t } = useTranslation(['equipmentTranslation', 'generalTranslation']);

	const handleChange = (event) => {
		const newEquipment = {
			...rentalEquipmentDialog,
			[event.target.name]: event.target.value,
		};
		setRentalEquipmentDialog(newEquipment);
	};

	const handleSave = async () => {
		if (ValidateRentalEquipment(rentalEquipmentDialog)) {
			setIsLoading(true);
			switch (mode) {
				case 'delete':
					await deleteRentalEquipment(rentalEquipment.id);
					break;
				case 'update':
					await updateRentalEquipment(rentalEquipmentDialog);
					break;
				case 'post':
					await addRentalEquipment(rentalEquipmentDialog);
					break;
			}
			setIsLoading(false);
			handleDialogSuccess(mode, rentalEquipmentDialog);
		}
	};

	return (
		<div>
			<DialogContent>
				<Backdrop
					sx={{
						color: '#fff',
						zIndex: (theme) => theme.zIndex.drawer + 1,
					}}
					open={isLoading}
				>
					<CircularProgress />
				</Backdrop>
				<Stack spacing={2}>
					<Stack direction={'row'} className={'DialogTopStack'}>
						<ConstructionIcon className={'DividerIcon'} />
						<Typography variant="h6" className={'MarginTopBottomAuto'}>
							{t('equipmentInformation')}
						</Typography>
					</Stack>
					<Grid container spacing={2}>
						<Grid xs={12} md={6}>
							<ValidatedTextField
								name="name"
								label={t('equipmentName')}
								value={rentalEquipmentDialog.name}
								onChange={handleChange}
								validationFunction={validateEquipmentName}
								isRequired={true}
								isReadonly={mode === 'delete' || mode === 'info'}
							/>
						</Grid>
						<Grid xs={12} md={6}>
							<ValidatedTextField
								name="price"
								label={t('price')}
								value={rentalEquipmentDialog.price}
								onChange={handleChange}
								validationFunction={validateEquipmentPrice}
								isRequired={true}
								isReadonly={mode === 'delete' || mode === 'info'}
							/>
						</Grid>
					</Grid>
				</Stack>
			</DialogContent>
			{mode !== 'info' ? (
				<Stack direction="row" justifyContent="space-between" className={'DialogStack'}>
					{mode === 'delete' ? (
						<Button
							variant="contained"
							color={'error'}
							size="large"
							endIcon={<DeleteIcon />}
							onClick={handleSave}
							className={'DialogButton'}
						>
							{t('delete', { ns: 'generalTranslation' })}
						</Button>
					) : (
						<Button
							variant="contained"
							color={'success'}
							size="large"
							endIcon={<DoneIcon />}
							onClick={handleSave}
							className={'DialogButton'}
							disabled={!ValidateRentalEquipment(rentalEquipmentDialog)}
						>
							{t('save', { ns: 'generalTranslation' })}
						</Button>
					)}
					<Button
						variant="outlined"
						color={'primary'}
						size="large"
						endIcon={<CancelIcon />}
						onClick={() => handleCancelDialog(false)}
						className={'DialogButton'}
					>
						{t('cancel', { ns: 'generalTranslation' })}
					</Button>
				</Stack>
			) : null}
		</div>
	);
}

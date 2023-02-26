import * as React from 'react';
import {
	Button,
	Dialog,
	DialogActions,
	DialogContent,
	DialogTitle,
	InputAdornment,
	Slide,
	Stack,
	TextField,
	Typography,
} from '@mui/material';
import CancelIcon from '@mui/icons-material/Cancel';
import { DefaultValue, useRecoilState, useSetRecoilState } from 'recoil';
import { employeeSnackbar } from '../Atoms/EmployeeAtoms';
import { useTheme } from '@mui/material/styles';
import DoneIcon from '@mui/icons-material/Done';
import { useFormik } from 'formik';
import { useState } from 'react';
import EngineeringIcon from '@mui/icons-material/Engineering';
import Grid from '@mui/material/Unstable_Grid2';
import { motion } from 'framer-motion';
import { useTranslation } from 'react-i18next';
import {
	forceRentalEquipmentRefresh,
	rentalEquipmentAtom,
	rentalEquipmentShowEditDialog,
} from '../Atoms/RentaLEquipmentAtoms';
import {
	addRentalEquipment,
	updateRentalEquipment,
} from '../../Actions/RestAPI/RentalEquipmentActions';
import { rentalEquipmentValidationSchema } from '../../Actions/Validations/ValidateRentalEquipment';

const Transition = React.forwardRef(function Transition(props, ref) {
	return <Slide direction="down" ref={ref} {...props} />;
});

const variants = {
	animateError: {
		x: [null, -10, 10, 0],
		transition: {
			type: 'spring',
			bounce: 0.6,
		},
	},
	animateSuccess: {
		y: [null, -50, 0],
		transition: {
			type: 'spring',
			bounce: 0.6,
		},
	},
	initial: {
		opacity: 1,
	},
};

function animateInputError(employeeId, isTouched, isError) {
	if (employeeId === -1) {
		console.log(isTouched && isError);
		return isTouched && isError;
	}
	return isError;
}

export default function RentalEquipmentForm() {
	const [showDialog, setShowDialog] = useState(true);
	const setShowDialogAtom = useSetRecoilState(rentalEquipmentShowEditDialog);
	const [rentalEquipment, setRentalEquipment] = useRecoilState(rentalEquipmentAtom);
	const setSnackbar = useSetRecoilState(employeeSnackbar);
	const [forceRefresh, setForceRefresh] = useRecoilState(forceRentalEquipmentRefresh);
	const { t } = useTranslation(['equipmentTranslation', 'generalTranslation']);
	const theme = useTheme();
	const handleCloseDialog = () => {
		setShowDialog(false);
		setTimeout(() => {
			setRentalEquipment(new DefaultValue());
			setShowDialogAtom(false);
		}, 300);
	};

	const formik = useFormik({
		initialValues: {
			id: rentalEquipment.id,
			name: rentalEquipment.name,
			price: rentalEquipment.price,
		},
		validateOnChange: true,
		enableReinitialize: true,
		validationSchema: rentalEquipmentValidationSchema,
		initialErrors: rentalEquipment.id === -1 ? [' '] : [],
		onSubmit: (values) => {
			if (rentalEquipment.id === -1) {
				addRentalEquipment(values)
					.then(() => {
						setSnackbar({
							show: true,
							title: t('rentalEquipmentAdded'),
							severity: 'success',
						});
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
			} else {
				updateRentalEquipment(values)
					.then(() => {
						setSnackbar({
							show: true,
							title: t('rentalEquipmentEdited'),
							severity: 'success',
						});
						handleCloseDialog();
						setForceRefresh(!forceRefresh);
					})
					.catch(() => {
						setSnackbar({
							show: true,
							title: t('errorHasOccurred'),
							severity: 'error',
						});
					});
			}
		},
	});

	return (
		<Dialog
			onClose={handleCloseDialog}
			open={showDialog}
			TransitionComponent={Transition}
			maxWidth={'xs'}
			PaperProps={{
				sx: `border: 2px solid ${theme.palette.primary.main}; overflow: hidden;`,
			}}
			slotProps={{
				backdrop: {
					timeout: 250,
					sx: 'background-color: rgba(0,0,0,0); backdrop-filter: blur(5px);',
				},
			}}
		>
			<form onSubmit={formik.handleSubmit}>
				<DialogTitle>
					{rentalEquipment.id === -1 ? t('addRentalEquipment') : t('editRentalEquipment')}
				</DialogTitle>
				<DialogContent dividers className={'employee-form'}>
					<Stack spacing={2}>
						<Stack direction={'row'} spacing={1} sx={{ paddingLeft: '8px' }}>
							<EngineeringIcon />
							<Typography variant="h6">{t('rentalEquipmentInformation')}</Typography>
						</Stack>
						<Grid container spacing={2}>
							<Grid xs={12} md={7}>
								<TextField
									fullWidth
									id="name"
									name="name"
									label={t('name')}
									value={formik.values.name}
									onChange={formik.handleChange}
									onBlur={formik.handleBlur}
									error={
										rentalEquipment.id === -1
											? formik.touched.name && Boolean(formik.errors.name)
											: Boolean(formik.errors.name)
									}
									helperText={
										formik.errors.name ? t(formik.errors.name) : t('required')
									}
									autoComplete="off"
									component={motion.div}
									variants={variants}
									initial={false}
									animate={
										animateInputError(
											rentalEquipment.id,
											formik.touched.name,
											formik.errors.name
										)
											? 'animateError'
											: 'initial'
									}
								/>
							</Grid>
							<Grid xs={6} md={5}>
								<TextField
									fullWidth
									id="price"
									name="price"
									label={t('price')}
									type="price"
									value={formik.values.price}
									onChange={formik.handleChange}
									onBlur={formik.handleBlur}
									error={
										rentalEquipment.id === -1
											? formik.touched.price && Boolean(formik.errors.price)
											: Boolean(formik.errors.price)
									}
									helperText={
										formik.errors.price ? t(formik.errors.price) : t('required')
									}
									InputProps={{
										endAdornment: (
											<InputAdornment position="end">zł</InputAdornment>
										),
									}}
									autoComplete="off"
									component={motion.div}
									variants={variants}
									initial={false}
									animate={
										animateInputError(
											rentalEquipment.id,
											formik.touched.price,
											formik.errors.price
										)
											? 'animateError'
											: 'initial'
									}
								/>
							</Grid>
						</Grid>
					</Stack>
				</DialogContent>
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
						type="submit"
						startIcon={<DoneIcon />}
						variant={'contained'}
						color={'success'}
						disabled={!formik.isValid}
						component={motion.button}
						variants={variants}
						initial={false}
						animate={formik.isValid ? 'animateSuccess' : 'animateError'}
					>
						{rentalEquipment.id === -1
							? t('add', { ns: 'generalTranslation' })
							: t('edit', { ns: 'generalTranslation' })}
					</Button>
				</DialogActions>
			</form>
		</Dialog>
	);
}

import * as React from 'react';
import {
	Button,
	Dialog,
	DialogActions,
	DialogContent,
	DialogTitle,
	Slide,
	Stack,
	TextField,
	Typography,
} from '@mui/material';
import CancelIcon from '@mui/icons-material/Cancel';
import { DefaultValue, useRecoilState, useSetRecoilState } from 'recoil';
import {
	employeeAtom,
	employeeShowEditDialog,
	employeeSnackbar,
	forceEmployeeRefresh,
} from '../Atoms/EmployeeAtoms';
import { useTheme } from '@mui/material/styles';
import DoneIcon from '@mui/icons-material/Done';
import { useFormik } from 'formik';
import { useState } from 'react';
import EngineeringIcon from '@mui/icons-material/Engineering';
import Grid from '@mui/material/Unstable_Grid2';
import { motion } from 'framer-motion';
import { employeeValidationSchema } from '../../Actions/Validations/ValidateEmployee';
import { addEmployee, updateEmployee } from '../../Actions/RestAPI/EmployeeActions';

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
		y: [-50, 0],
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

export default function EmployeeForm() {
	const [showDialog, setShowDialog] = useState(true);
	const setShowDialogAtom = useSetRecoilState(employeeShowEditDialog);
	const [employee, setEmployee] = useRecoilState(employeeAtom);
	const setSnackbar = useSetRecoilState(employeeSnackbar);
	const [forceRefresh, setForceRefresh] = useRecoilState(forceEmployeeRefresh);
	const theme = useTheme();
	const handleCloseDialog = () => {
		setShowDialog(false);
		setTimeout(() => {
			setEmployee(new DefaultValue());
			setShowDialogAtom(false);
		}, 300);
	};

	const formik = useFormik({
		initialValues: {
			name: employee.name,
			surname: employee.surname,
		},
		validateOnChange: true,
		enableReinitialize: true,
		validationSchema: employeeValidationSchema,
		initialErrors: employee.id === -1 ? [' '] : [],
		onSubmit: (values) => {
			if (employee.id === -1) {
				addEmployee(values)
					.then(() => {
						setSnackbar({
							show: true,
							title: 'Employee added successfully',
							severity: 'success',
						});
						handleCloseDialog();
						setForceRefresh(!forceRefresh);
					})
					.catch(() => {
						setSnackbar({
							show: true,
							title: 'An error has occurred',
							severity: 'error',
						});
					});
			} else {
				updateEmployee(values)
					.then(() => {
						setSnackbar({
							show: true,
							title: 'Employee updated successfully',
							severity: 'success',
						});
						handleCloseDialog();
						setForceRefresh(!forceRefresh);
					})
					.catch(() => {
						setSnackbar({
							show: true,
							title: 'An error has occurred',
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
				<DialogTitle>{employee.id === -1 ? 'Add employee' : 'Edit employee'}</DialogTitle>
				<DialogContent dividers className={'employee-form'}>
					<Stack spacing={2}>
						<Stack direction={'row'} spacing={1} sx={{ paddingLeft: '8px' }}>
							<EngineeringIcon />
							<Typography variant="h6">Employee Information</Typography>
						</Stack>
						<Grid container spacing={2}>
							<Grid xs={12} md={6}>
								<TextField
									fullWidth
									id="name"
									name="name"
									label="Name"
									value={formik.values.name}
									onChange={formik.handleChange}
									onBlur={formik.handleBlur}
									error={
										employee.id === -1
											? formik.touched.name && Boolean(formik.errors.name)
											: Boolean(formik.errors.name)
									}
									helperText={formik.errors.name ?? 'Required'}
									autoComplete="off"
									component={motion.div}
									variants={variants}
									initial={false}
									animate={
										animateInputError(
											employee.id,
											formik.touched.name,
											formik.errors.name
										)
											? 'animateError'
											: 'initial'
									}
								/>
							</Grid>
							<Grid xs={12} md={6}>
								<TextField
									fullWidth
									id="surname"
									name="surname"
									label="Surname"
									type="surname"
									value={formik.values.surname}
									onChange={formik.handleChange}
									onBlur={formik.handleBlur}
									error={
										employee.id === -1
											? formik.touched.surname &&
											  Boolean(formik.errors.surname)
											: Boolean(formik.errors.surname)
									}
									helperText={formik.errors.surname ?? 'Required'}
									autoComplete="off"
									component={motion.div}
									variants={variants}
									initial={false}
									animate={
										animateInputError(
											employee.id,
											formik.touched.surname,
											formik.errors.surname
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
						Cancel
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
						{employee.id === -1 ? 'Add' : 'Edit'}
					</Button>
				</DialogActions>
			</form>
		</Dialog>
	);
}

import * as React from 'react';
import ValidateEmployee, {
	validateEmployeeName,
	validateEmployeeSurname,
} from '../../Actions/Validations/ValidateEmployee';
import { addEmployee, deleteEmployee, updateEmployee } from '../../Actions/RestAPI/EmployeeActions';
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
import EngineeringIcon from '@mui/icons-material/Engineering';
import ValidatedTextField from '../Shared/ValidatedTextField';

export default function EmployeeDialog({
	handleCancelDialog,
	employee,
	handleDialogSuccess,
	mode,
}) {
	const [employeeDialog, setEmployeeDialog] = React.useState(employee);
	const [isLoading, setIsLoading] = React.useState(false);
	const handleChange = (event) => {
		const newEmployee = {
			...employeeDialog,
			[event.target.name]: event.target.value,
		};
		setEmployeeDialog(newEmployee);
	};
	const handleSave = async () => {
		if (ValidateEmployee(employeeDialog)) {
			setIsLoading(true);
			switch (mode) {
				case 'delete':
					await deleteEmployee(employeeDialog.id);
					break;
				case 'update':
					await updateEmployee(employeeDialog);
					break;
				case 'post':
					await addEmployee(employeeDialog);
					break;
			}
			setIsLoading(false);
			handleDialogSuccess(mode, employeeDialog);
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
						<EngineeringIcon className={'DividerIcon'} />
						<Typography variant="h6" className={'MarginTopBottomAuto'}>
							Employee information
						</Typography>
					</Stack>
					<Grid container spacing={2}>
						<Grid xs={12} md={6}>
							<ValidatedTextField
								name="name"
								label="Name"
								value={employeeDialog.name}
								onChange={handleChange}
								validationFunction={validateEmployeeName}
								isRequired={true}
								isReadonly={mode === 'delete' || mode === 'info'}
							/>
						</Grid>
						<Grid xs={12} md={6}>
							<ValidatedTextField
								name="surname"
								label="Surname"
								value={employeeDialog.surname}
								onChange={handleChange}
								validationFunction={validateEmployeeSurname}
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
							Delete
						</Button>
					) : (
						<Button
							variant="contained"
							color={'success'}
							size="large"
							endIcon={<DoneIcon />}
							onClick={handleSave}
							className={'DialogButton'}
							disabled={!ValidateEmployee(employeeDialog)}
						>
							Save
						</Button>
					)}
					<Button
						variant="outlined"
						color={'primary'}
						size="large"
						endIcon={<CancelIcon />}
						onClick={() => handleCancelDialog()}
						className={'DialogButton'}
					>
						Cancel
					</Button>
				</Stack>
			) : null}
		</div>
	);
}

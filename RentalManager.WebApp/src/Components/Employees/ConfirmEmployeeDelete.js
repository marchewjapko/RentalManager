import {
	employeeAtom,
	employeeShowDeleteConfirmation,
	employeeSnackbar,
	forceEmployeeRefresh,
} from '../Atoms/EmployeeAtoms';
import { DefaultValue, useRecoilState, useSetRecoilState } from 'recoil';
import * as React from 'react';
import { useState } from 'react';
import { filterAgreements } from '../../Actions/RestAPI/RentalAgreementActions';
import {
	Button,
	Dialog,
	DialogContent,
	DialogTitle,
	Skeleton,
	Slide,
	Stack,
	Typography,
} from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import CancelIcon from '@mui/icons-material/Cancel';
import { deleteEmployee } from '../../Actions/RestAPI/EmployeeActions';

const Transition = React.forwardRef(function Transition(props, ref) {
	return <Slide direction="down" ref={ref} {...props} />;
});

export default function ConfirmEmployeeDelete() {
	const [employee, setEmployee] = useRecoilState(employeeAtom);
	const [showDialog, setShowDialog] = useRecoilState(employeeShowDeleteConfirmation);
	const setSnackbar = useSetRecoilState(employeeSnackbar);
	const [forceRefresh, setForceRefresh] = useRecoilState(forceEmployeeRefresh);
	const [numberOfAgreements, setNumberOfAgreements] = useState(0);
	const [isLoading, setIsLoading] = useState(true);

	React.useEffect(() => {
		if (employee.id !== -1) {
			const searchParams = { employeeId: employee.id };
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
		}
	}, [employee]);
	const handleCloseDialog = () => {
		setShowDialog(false);
		setTimeout(() => {
			setEmployee(new DefaultValue());
		}, 250);
	};
	const handleDeleteClick = () => {
		if (numberOfAgreements === 0) {
			deleteEmployee(employee.id)
				.then(() => {
					setSnackbar({
						show: true,
						title: 'Employee deleted successfully',
						severity: 'success',
					});
					setEmployee(new DefaultValue());
					setShowDialog(new DefaultValue());
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
	};

	function getDialogContent() {
		if (numberOfAgreements > 0) {
			return (
				<>
					<Typography>
						Employee {employee.name} has <b>{numberOfAgreements}</b> agreement(s)
						assigned to them.
					</Typography>
					<Typography>Delete these agreements to proceed.</Typography>
				</>
			);
		} else if (isLoading) {
			return (
				<Stack direction={'column'}>
					<Skeleton variant="text" sx={{ fontSize: '1em' }} />
					<Skeleton variant="text" sx={{ fontSize: '1em' }} />
				</Stack>
			);
		}
		return <Typography>Are you sure you want to delete {employee.name}?</Typography>;
	}

	return (
		<Dialog
			onClose={handleCloseDialog}
			open={showDialog}
			TransitionComponent={Transition}
			maxWidth={'xs'}
		>
			<DialogTitle>Delete employee</DialogTitle>
			<DialogContent>{getDialogContent()}</DialogContent>
			<Stack
				direction={'row'}
				alignItems={'center'}
				justifyContent={'space-around'}
				sx={{ paddingLeft: '10px', paddingRight: '10px', paddingBottom: '10px' }}
				gap={'10px'}
			>
				<Button
					onClick={handleDeleteClick}
					startIcon={<DeleteIcon />}
					sx={{ flex: 1 }}
					variant={'contained'}
					color={'error'}
					disabled={numberOfAgreements !== 0 || isLoading}
				>
					Delete
				</Button>
				<Button
					autoFocus
					sx={{ flex: 1 }}
					endIcon={<CancelIcon />}
					variant={'outlined'}
					onClick={handleCloseDialog}
				>
					Cancel
				</Button>
			</Stack>
		</Dialog>
	);
}

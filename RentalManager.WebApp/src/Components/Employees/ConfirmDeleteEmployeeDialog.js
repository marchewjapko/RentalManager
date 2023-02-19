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
import { deleteEmployee } from '../../Actions/RestAPI/EmployeeActions';
import { useTheme } from '@mui/material/styles';

const Transition = React.forwardRef(function Transition(props, ref) {
	return <Slide direction="down" ref={ref} {...props} />;
});

export default function ConfirmDeleteEmployeeDialog() {
	const [employee, setEmployee] = useRecoilState(employeeAtom);
	const [showDialog, setShowDialog] = useState(true);
	const setShowDialogAtom = useSetRecoilState(employeeShowDeleteConfirmation);
	const setSnackbar = useSetRecoilState(employeeSnackbar);
	const [forceRefresh, setForceRefresh] = useRecoilState(forceEmployeeRefresh);
	const [numberOfAgreements, setNumberOfAgreements] = useState(0);
	const [isLoading, setIsLoading] = useState(true);
	const theme = useTheme();

	React.useEffect(() => {
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
	}, []);

	const handleCloseDialog = () => {
		setShowDialog(false);
		setTimeout(() => {
			setEmployee(new DefaultValue());
			setShowDialogAtom(false);
		}, 300);
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
						Employee {employee.name} has <b>{numberOfAgreements}</b> agreement(s)
						assigned to them.
					</Typography>
					<Typography>Delete these agreements to proceed.</Typography>
				</>
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
			<DialogTitle>Delete employee</DialogTitle>
			<DialogContent dividers>{getDialogContent()}</DialogContent>
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
					onClick={handleDeleteClick}
					startIcon={<DeleteIcon />}
					variant={'contained'}
					color={'error'}
					disabled={numberOfAgreements !== 0 || isLoading}
				>
					Delete
				</Button>
			</DialogActions>
		</Dialog>
	);
}

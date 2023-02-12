import * as React from 'react';
import './Clients.js.css';
import '../SharedStyles.css';
import {
	Alert,
	Button,
	Dialog,
	DialogTitle,
	Paper,
	Snackbar,
	Stack,
	Table,
	TableBody,
	TableContainer,
	TablePagination,
} from '@mui/material';
import ClientTableRow from './ClientTableRow';
import { filterClients, getAllClients } from '../../Actions/ClientActions';
import ClientsDialog from './ClientsDialog';
import AddCircleRoundedIcon from '@mui/icons-material/AddCircleRounded';
import ClientsSearchSelect from './ClientsSearchSelect';
import SkeletonTableClients from '../SkeletonTables/SkeletonTableClients';
import useMediaQuery from '@mui/material/useMediaQuery';
import { useTheme } from '@mui/material/styles';

export default function Clients({ isCheckable, client, setClient }) {
	const [page, setPage] = React.useState(0);
	const [rowsPerPage, setRowsPerPage] = React.useState(5);
	const [isLoading, setIsLoading] = React.useState(true);
	const [data, setData] = React.useState();
	const [showSnackbar, setShowSnackbar] = React.useState(false);
	const [showDialog, setShowDialog] = React.useState(false);
	const [dialogMode, setDialogMode] = React.useState('');
	const theme = useTheme();
	const isSmallScreen = useMediaQuery(theme.breakpoints.down('sm'));

	const handleChangePage = (event, newPage) => {
		setPage(newPage);
	};

	const handleChangeRowsPerPage = (event) => {
		setRowsPerPage(parseInt(event.target.value, 10));
		setPage(0);
	};

	React.useEffect(() => {
		const getData = async () => {
			const result = await getAllClients();
			setData(result.data);
			setIsLoading(false);
		};
		getData();
	}, []);

	const getDialogTitle = () => {
		switch (dialogMode) {
			case 'post':
				return 'Add client';
			case 'update':
				return 'Edit client';
			case 'delete':
				return 'Delete client';
		}
	};

	const getSnackbarTitle = () => {
		switch (dialogMode) {
			case 'post':
				return 'Client added successfully';
			case 'update':
				return 'Client updated successfully';
			case 'delete':
				return 'Client deleted successfully';
		}
	};

	const handleEditClick = (client) => {
		setClient(client);
		setDialogMode('update');
		setShowDialog(true);
	};

	const handleDeleteClick = (client) => {
		setClient(client);
		setDialogMode('delete');
		setShowDialog(true);
	};

	const handleAddClick = () => {
		setDialogMode('post');
		setClient({
			id: -1,
			name: '',
			surname: '',
			phoneNumber: '',
			email: '',
			idCard: '',
			city: '',
			street: '',
			streetNumber: '',
		});
		setShowDialog(true);
	};

	const handleCloseDialog = () => {
		setShowDialog(false);
	};

	const handleDialogSuccess = async (mode, client) => {
		setIsLoading(true);
		setShowSnackbar(true);
		handleCloseDialog();
		const result = await getAllClients();
		setData(result.data);
		setIsLoading(false);
		if (mode === 'delete' || mode === 'post') {
			setClient(client);
		}
	};

	const closeSnackbars = () => {
		setShowSnackbar(false);
	};

	const dialog = () => {
		return (
			<Dialog
				open={showDialog}
				maxWidth={'sm'}
				onClose={handleCloseDialog}
			>
				<DialogTitle>{getDialogTitle()}</DialogTitle>
				<ClientsDialog
					client={client}
					handleCancelDialog={handleCloseDialog}
					handleDialogSuccess={handleDialogSuccess}
					mode={dialogMode}
					setClient={setClient}
					showDialogButtons={true}
				/>
			</Dialog>
		);
	};

	const handleSearch = async (searchValues) => {
		setIsLoading(true);
		const result = await filterClients(searchValues);
		setData(result.data);
		setIsLoading(false);
		setPage(0);
		setClient(result[0]);
	};

	return (
		<div>
			<Snackbar
				open={showSnackbar}
				autoHideDuration={6000}
				onClose={closeSnackbars}
				anchorOrigin={{ vertical: 'top', horizontal: 'left' }}
			>
				<Alert
					onClose={closeSnackbars}
					severity="success"
					sx={{ width: '100%' }}
				>
					{getSnackbarTitle()}
				</Alert>
			</Snackbar>
			<div className={'ComponentContainer'}>
				<Stack
					direction={'row'}
					spacing={2}
					justifyContent="space-between"
					alignItems="center"
					className={'ComponentHeadStack'}
				>
					<Button
						startIcon={<AddCircleRoundedIcon />}
						color={'primary'}
						variant={'contained'}
						onClick={handleAddClick}
						disabled={isLoading}
					>
						Add client
					</Button>
					<ClientsSearchSelect
						isLoading={isLoading}
						handleSearch={handleSearch}
					/>
				</Stack>
				{isLoading ? (
					<SkeletonTableClients />
				) : (
					<>
						{dialog()}
						<TableContainer className={'ClientsTable'}>
							<Table
								stickyHeader
								size={isSmallScreen ? 'small' : 'medium'}
								padding={isSmallScreen ? 'none' : 'normal'}
							>
								<TableBody>
									{(rowsPerPage > 0
										? data.slice(
												page * rowsPerPage,
												page * rowsPerPage + rowsPerPage
										  )
										: data
									).map((row) => (
										<ClientTableRow
											key={row.id}
											row={row}
											handleEditClick={handleEditClick}
											handleDeleteClick={
												handleDeleteClick
											}
											isCheckable={isCheckable}
											setClient={setClient}
											client={client}
										/>
									))}
								</TableBody>
							</Table>
						</TableContainer>
						<TablePagination
							rowsPerPageOptions={[
								5,
								10,
								25,
								{ label: 'All', value: -1 },
							]}
							sx={{ paddingLeft: 0 }}
							component="div"
							count={data.length}
							rowsPerPage={rowsPerPage}
							page={page}
							onPageChange={handleChangePage}
							onRowsPerPageChange={handleChangeRowsPerPage}
						/>
					</>
				)}
			</div>
		</div>
	);
}

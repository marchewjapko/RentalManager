import * as React from 'react';
import '../SharedStyles.css';
import {
	Alert,
	Box,
	Button,
	ButtonGroup,
	Dialog,
	DialogTitle,
	IconButton,
	Paper,
	Popover,
	Snackbar,
	Stack,
	Table,
	TableBody,
	TableCell,
	TableContainer,
	TableHead,
	TablePagination,
	TableRow,
} from '@mui/material';
import { Scrollbars } from 'react-custom-scrollbars-2';
import AddCircleRoundedIcon from '@mui/icons-material/AddCircleRounded';
import TempNavigation from '../Shared/TempNavigation';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import MoreHorizIcon from '@mui/icons-material/MoreHoriz';
import dayjs from 'dayjs';
import 'dayjs/locale/pl';
import CloseIcon from '@mui/icons-material/Close';
import InfoIcon from '@mui/icons-material/Info';
import RentalAgreementDialog from './RentalAgreementDialog';
import useMediaQuery from '@mui/material/useMediaQuery';
import { useTheme } from '@mui/material/styles';
import { filterAgreements } from '../../Actions/RentalAgreementActions';
import RentalAgreementSearchSelect from './RentalAgreementSearchSelect';
import SkeletonTableRentalAgreement from '../SkeletonTables/SkeletonTableRentalAgreement';
import { useNavigate } from 'react-router-dom';

export default function RentalAgreement() {
	const [page, setPage] = React.useState(0);
	const [rowsPerPage, setRowsPerPage] = React.useState(5);
	const [isLoading, setIsLoading] = React.useState(true);
	const [data, setData] = React.useState();
	const [showSnackbar, setShowSnackbar] = React.useState(false);
	const [showDialog, setShowDialog] = React.useState(false);
	const [focusedAgreement, setFocusedAgreement] = React.useState();
	const [dialogMode, setDialogMode] = React.useState('');
	const [anchorEl, setAnchorEl] = React.useState(null);
	const theme = useTheme();
	const navigate = useNavigate();
	const dialogFullScreen = useMediaQuery(theme.breakpoints.down('sm'));
	const [searchValues, setSearchValues] = React.useState({
		surname: '',
		phone: '',
		city: '',
		street: '',
		onlyActive: true,
		onlyUnpaid: false,
	});
	dayjs.locale('pl');

	React.useEffect(() => {
		const getData = async () => {
			const result = await filterAgreements(searchValues);
			setData(result.data);
			setIsLoading(false);
		};
		getData();
	}, []);

	const handleChangePage = (event, newPage) => {
		setPage(newPage);
	};

	const handleChangeRowsPerPage = (event) => {
		setRowsPerPage(parseInt(event.target.value, 10));
		setPage(0);
	};

	const getDialogTitle = () => {
		switch (dialogMode) {
			case 'post':
				return 'Add agreement';
			case 'update':
				return 'Edit agreement';
			case 'delete':
				return 'Delete agreement';
			case 'info':
				return 'Agreement details';
		}
	};

	const getSnackbarTitle = () => {
		switch (dialogMode) {
			case 'post':
				return 'Agreement added successfully';
			case 'update':
				return 'Agreement updated successfully';
			case 'delete':
				return 'Agreement deleted successfully';
		}
	};

	const handleEditClick = () => {
		setAnchorEl(null);
		setDialogMode('update');
		setShowDialog(true);
	};

	const handleDeleteClick = () => {
		setAnchorEl(null);
		setDialogMode('delete');
		setShowDialog(true);
	};

	const handleDetailsClick = () => {
		setAnchorEl(null);
		setDialogMode('info');
		setShowDialog(true);
	};

	const handleAddClick = () => {
		navigate('/add-rental-agreement');
	};

	const handleCloseDialog = () => {
		setShowDialog(false);
	};

	const handleDialogSuccess = async () => {
		setIsLoading(true);
		setShowSnackbar(true);
		handleCloseDialog();
		const newSearchParams = {
			surname: '',
			phone: '',
			city: '',
			street: '',
			onlyActive: true,
			onlyUnpaid: false,
		};
		setSearchValues(newSearchParams);
		const result = await filterAgreements(newSearchParams);
		setData(result.data);
		setIsLoading(false);
	};

	const closeSnackbars = () => {
		setShowSnackbar(false);
	};

	const handleSearch = async () => {
		setIsLoading(true);
		const result = await filterAgreements(searchValues);
		setData(result.data);
		setIsLoading(false);
	};

	const dialog = () => {
		return (
			<Dialog
				open={showDialog}
				onClose={handleCloseDialog}
				fullScreen={dialogFullScreen && dialogMode === 'post'}
			>
				<DialogTitle>
					<Stack direction={'row'} justifyContent="space-between">
						{getDialogTitle()}
						<IconButton
							edge="start"
							color="inherit"
							onClick={handleCloseDialog}
							aria-label="close"
						>
							<CloseIcon />
						</IconButton>
					</Stack>
				</DialogTitle>
				<RentalAgreementDialog
					agreement={focusedAgreement}
					handleCancelDialog={handleCloseDialog}
					handleDialogSuccess={handleDialogSuccess}
					mode={dialogMode}
				/>
			</Dialog>
		);
	};

	const getDate = (date) => {
		return dayjs(date)
			.format('dddd, DD MMMM YYYY')
			.replace(/(^|\s)\S/g, (l) => l.toUpperCase());
	};

	const handleOpenPopper = (event, agreement) => {
		setAnchorEl(event.currentTarget);
		setFocusedAgreement(agreement);
	};

	const handleClosePopper = () => {
		setAnchorEl(null);
	};

	const tablePopper = () => {
		return (
			<Popover
				open={Boolean(anchorEl)}
				anchorEl={anchorEl}
				onClose={handleClosePopper}
				anchorOrigin={{
					vertical: 'bottom',
					horizontal: 'center',
				}}
				transformOrigin={{
					vertical: 'top',
					horizontal: 'center',
				}}
			>
				<ButtonGroup
					variant="outlined"
					aria-label="outlined primary button group"
				>
					<Button
						variant="contained"
						color="success"
						startIcon={<EditIcon />}
						sx={{ width: '6.5rem', height: '2.5rem' }}
						onClick={handleEditClick}
					>
						Edit
					</Button>
					<Button
						variant="contained"
						color="error"
						startIcon={<DeleteIcon />}
						sx={{ width: '7rem', height: '2.5rem' }}
						onClick={handleDeleteClick}
					>
						Delete
					</Button>
					<Button
						variant="contained"
						startIcon={<InfoIcon />}
						sx={{ width: '6.5rem', height: '2.5rem' }}
						onClick={handleDetailsClick}
					>
						Details
					</Button>
				</ButtonGroup>
			</Popover>
		);
	};
	const getRowColor = (row) => {
		const lastPayment = row.payments
			.sort((a, b) => (dayjs(a).isAfter(dayjs(b)) ? 1 : -1))
			.at(-1);
		if (!row.isActive) {
			return theme.palette.text.disabled;
		} else if (dayjs(lastPayment.to).diff(dayjs(), 'day') < 0) {
			return theme.palette.error.light;
		}
	};

	return (
		<div>
			<Paper className={'ComponentContainer'}>
				<Stack
					direction={'row'}
					justifyContent="space-between"
					alignItems="center"
					className={'ComponentHeadStack'}
				>
					<Button
						startIcon={<AddCircleRoundedIcon />}
						variant={'contained'}
						onClick={handleAddClick}
						disabled={isLoading}
					>
						Add agreement
					</Button>
					<RentalAgreementSearchSelect
						isLoading={isLoading}
						handleSearch={handleSearch}
						searchValues={searchValues}
						setSearchValues={setSearchValues}
					/>
				</Stack>
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
				{isLoading ? (
					<SkeletonTableRentalAgreement />
				) : (
					<div>
						{tablePopper()}
						{dialog()}
						<TableContainer className={'RentalEquipmentTable'}>
							<Scrollbars
								autoHeight={true}
								autoHeightMin={0}
								autoHeightMax={460}
								autoHide
								autoHideTimeout={750}
								autoHideDuration={500}
							>
								<Table stickyHeader>
									<TableHead>
										<TableRow>
											<TableCell
												className={
													'TableRentalEquipmentColumnName'
												}
											>
												Client
											</TableCell>
											<TableCell
												align="right"
												className={
													'TableRentalEquipmentColumnPrice'
												}
											>
												Date added
											</TableCell>
											<TableCell
												align="right"
												sx={{ width: '10px' }}
											/>
										</TableRow>
									</TableHead>
									<TableBody>
										{(rowsPerPage > 0
											? data.slice(
													page * rowsPerPage,
													page * rowsPerPage +
														rowsPerPage
											  )
											: data
										).map((row) => (
											<TableRow
												key={row.id}
												sx={{
													backgroundColor:
														getRowColor(row),
												}}
											>
												<TableCell
													component="th"
													scope="row"
												>
													{row.client.name +
														' ' +
														row.client.surname}
												</TableCell>
												<TableCell align="right">
													{getDate(row.dateAdded)}
												</TableCell>
												<TableCell align="right">
													<Box>
														<IconButton
															color={'inherit'}
															onClick={(event) =>
																handleOpenPopper(
																	event,
																	row
																)
															}
															disabled={isLoading}
														>
															<MoreHorizIcon fontSize="inherit" />
														</IconButton>
													</Box>
												</TableCell>
											</TableRow>
										))}
									</TableBody>
								</Table>
							</Scrollbars>
						</TableContainer>
						<TablePagination
							rowsPerPageOptions={[
								5,
								10,
								25,
								{ label: 'All', value: -1 },
							]}
							component="div"
							count={data.length}
							rowsPerPage={rowsPerPage}
							page={page}
							onPageChange={handleChangePage}
							onRowsPerPageChange={handleChangeRowsPerPage}
						/>
					</div>
				)}
			</Paper>
			<TempNavigation />
		</div>
	);
}

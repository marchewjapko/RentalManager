import * as React from 'react';
import '../SharedStyles.css';
import {
	Box,
	Button,
	ButtonGroup,
	Dialog,
	DialogTitle,
	IconButton,
	Paper,
	Popover,
	Stack,
	Table,
	TableBody,
	TableCell,
	TableContainer,
	TablePagination,
	TableRow,
} from '@mui/material';
import { Scrollbars } from 'react-custom-scrollbars-2';
import AddCircleRoundedIcon from '@mui/icons-material/AddCircleRounded';
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
import { filterAgreements } from '../../Actions/RestAPI/RentalAgreementActions';
import RentalAgreementSearchSelect from './RentalAgreementSearchSelect';
import SkeletonTableRentalAgreement from '../SkeletonTables/SkeletonTableRentalAgreement';
import { useNavigate } from 'react-router-dom';
import { DefaultValue, useRecoilState } from 'recoil';
import { rentalAgreementAtom } from '../Atoms/RentalAgreementAtoms';
import { useTranslation } from 'react-i18next';

export default function RentalAgreement() {
	const [page, setPage] = React.useState(0);
	const [rowsPerPage, setRowsPerPage] = React.useState(5);
	const [isLoading, setIsLoading] = React.useState(true);
	const [data, setData] = React.useState();
	const [showDialog, setShowDialog] = React.useState(false);
	const [focusedAgreement, setFocusedAgreement] = useRecoilState(rentalAgreementAtom);
	const [dialogMode, setDialogMode] = React.useState('');
	const [anchorEl, setAnchorEl] = React.useState(null);
	const theme = useTheme();
	const navigate = useNavigate();
	const dialogFullScreen = useMediaQuery(theme.breakpoints.down('sm'));
	const { t, i18n } = useTranslation(['generalTranslation', 'agreementTranslation']);
	const [searchValues, setSearchValues] = React.useState({
		surname: '',
		phone: '',
		city: '',
		street: '',
		onlyActive: true,
		onlyUnpaid: false,
	});
	dayjs.locale(i18n.language);
	React.useEffect(() => {
		const getData = async () => {
			const result = await filterAgreements(searchValues);
			setData(result.hasOwnProperty('data') ? result.data : result);
			setIsLoading(false);
		};
		getData();
		setFocusedAgreement(new DefaultValue());
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
			case 'update':
				return t('editAgreement', { ns: 'agreementTranslation' });
			case 'delete':
				return t('deleteAgreement', { ns: 'agreementTranslation' });
			case 'info':
				return t('agreementDetails', { ns: 'agreementTranslation' });
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
		setData(result.hasOwnProperty('data') ? result.data : result);
		setIsLoading(false);
	};
	const handleSearch = async () => {
		setIsLoading(true);
		const result = await filterAgreements(searchValues);
		setData(result.hasOwnProperty('data') ? result.data : result);
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
		const rentalAgreementDetails = Object.fromEntries(
			Object.entries(agreement).filter((e) => e[0] !== 'client')
		);
		setFocusedAgreement({
			rentalAgreementDetails: rentalAgreementDetails,
			client: agreement.client,
		});
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
				<ButtonGroup variant="outlined">
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
			.slice()
			.sort((a, b) => (dayjs(a.to).isAfter(dayjs(b.to)) ? 1 : -1))
			.at(-1);
		if (!row.isActive) {
			return theme.palette.text.disabled;
		} else if (dayjs(lastPayment.to).diff(dayjs(), 'day') < 0) {
			return theme.palette.error.light;
		}
	};

	function defaultLabelDisplayedRows({ from, to, count }) {
		if (i18n.language === 'en') {
			return `${from}–${to} of ${count !== -1 ? count : `more than ${to}`}`;
		}
		return `${from}–${to} z ${count !== -1 ? count : `more than ${to}`}`;
	}

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
						{t('add')}
					</Button>
					<RentalAgreementSearchSelect
						isLoading={isLoading}
						handleSearch={handleSearch}
						searchValues={searchValues}
						setSearchValues={setSearchValues}
					/>
				</Stack>
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
								<Table>
									<TableBody>
										{(rowsPerPage > 0
											? data.slice(
													page * rowsPerPage,
													page * rowsPerPage + rowsPerPage
											  )
											: data
										).map((row) => (
											<TableRow
												key={row.id}
												sx={{
													backgroundColor: getRowColor(row),
												}}
											>
												<TableCell component="th" scope="row">
													{row.client.name + ' ' + row.client.surname}
												</TableCell>
												<TableCell align="right">
													{getDate(row.dateAdded)}
												</TableCell>
												<TableCell align="right">
													<Box>
														<IconButton
															color={'inherit'}
															onClick={(event) =>
																handleOpenPopper(event, row)
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
							rowsPerPageOptions={[5, 10, 25, { label: 'All', value: -1 }]}
							component="div"
							count={data.length}
							rowsPerPage={rowsPerPage}
							page={page}
							onPageChange={handleChangePage}
							onRowsPerPageChange={handleChangeRowsPerPage}
							labelRowsPerPage={t('labelRowsPerPage')}
							labelDisplayedRows={defaultLabelDisplayedRows}
						/>
					</div>
				)}
			</Paper>
		</div>
	);
}

import * as React from 'react';
import './Clients.js.css';
import '../SharedStyles.css';
import {
	Button,
	Dialog,
	DialogTitle,
	Stack,
	Table,
	TableBody,
	TableContainer,
	TablePagination,
} from '@mui/material';
import ClientTableRow from './ClientTableRow';
import { filterClients, getAllClients } from '../../Actions/RestAPI/ClientActions';
import ClientsDialog from './ClientsDialog';
import AddCircleRoundedIcon from '@mui/icons-material/AddCircleRounded';
import ClientsSearchSelect from './ClientsSearchSelect';
import SkeletonTableClients from '../SkeletonTables/SkeletonTableClients';
import useMediaQuery from '@mui/material/useMediaQuery';
import { useTheme } from '@mui/material/styles';
import { DefaultValue, useRecoilState } from 'recoil';
import { clientAtom } from '../Atoms/ClientAtoms';
import { useTranslation } from 'react-i18next';

export default function Clients() {
	const [client, setClient] = useRecoilState(clientAtom);
	const [page, setPage] = React.useState(0);
	const [rowsPerPage, setRowsPerPage] = React.useState(5);
	const [isLoading, setIsLoading] = React.useState(true);
	const [data, setData] = React.useState();
	const [showDialog, setShowDialog] = React.useState(false);
	const [dialogMode, setDialogMode] = React.useState('');
	const theme = useTheme();
	const isSmallScreen = useMediaQuery(theme.breakpoints.down('sm'));
	const { t, i18n } = useTranslation(['generalTranslation', 'clientTranslation']);

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
			setData(result.hasOwnProperty('data') ? result.data : result);
			setIsLoading(false);
		};
		getData();
	}, []);
	const getDialogTitle = () => {
		switch (dialogMode) {
			case 'post':
				return t('addClient', { ns: 'clientTranslation' });
			case 'update':
				return t('editClient', { ns: 'clientTranslation' });
			case 'delete':
				return t('deleteClient', { ns: 'clientTranslation' });
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
		setClient(new DefaultValue());
		setShowDialog(true);
	};
	const handleCloseDialog = () => {
		setShowDialog(false);
	};
	const handleDialogSuccess = async (mode, client) => {
		setIsLoading(true);
		handleCloseDialog();
		const result = await getAllClients();
		setData(result.hasOwnProperty('data') ? result.data : result);
		setIsLoading(false);
		if (mode === 'delete' || mode === 'post') {
			setClient(client);
		}
	};
	const dialog = () => {
		return (
			<Dialog open={showDialog} maxWidth={'sm'} onClose={handleCloseDialog}>
				<DialogTitle>{getDialogTitle()}</DialogTitle>
				<ClientsDialog
					handleCancelDialog={handleCloseDialog}
					handleDialogSuccess={handleDialogSuccess}
					mode={dialogMode}
					showDialogButtons={true}
				/>
			</Dialog>
		);
	};
	const handleSearch = async (searchValues) => {
		setIsLoading(true);
		const result = await filterClients(searchValues);
		setData(result.hasOwnProperty('data') ? result.data : result);
		setIsLoading(false);
		setPage(0);
		setClient(result[0]);
	};

	function defaultLabelDisplayedRows({ from, to, count }) {
		if (i18n.language === 'en') {
			return `${from}–${to} of ${count !== -1 ? count : `more than ${to}`}`;
		}
		return `${from}–${to} z ${count !== -1 ? count : `more than ${to}`}`;
	}

	return (
		<div>
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
						{t('add')}
					</Button>
					<ClientsSearchSelect isLoading={isLoading} handleSearch={handleSearch} />
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
											handleDeleteClick={handleDeleteClick}
											setClient={setClient}
											client={client}
										/>
									))}
								</TableBody>
							</Table>
						</TableContainer>
						<TablePagination
							rowsPerPageOptions={[5, 10, 25, { label: 'All', value: -1 }]}
							sx={{ paddingLeft: 0 }}
							component="div"
							count={data.length}
							rowsPerPage={rowsPerPage}
							page={page}
							onPageChange={handleChangePage}
							onRowsPerPageChange={handleChangeRowsPerPage}
							labelRowsPerPage={t('labelRowsPerPage')}
							labelDisplayedRows={defaultLabelDisplayedRows}
						/>
					</>
				)}
			</div>
		</div>
	);
}

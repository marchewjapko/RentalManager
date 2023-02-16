import * as React from 'react';
import './Employees.js.css';
import {
	Box,
	Button,
	Dialog,
	DialogTitle,
	IconButton,
	Paper,
	Stack,
	Table,
	TableBody,
	TableCell,
	TableContainer,
	TableRow,
} from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import { Scrollbars } from 'react-custom-scrollbars-2';
import { filterEmployees, getAllEmployees } from '../../Actions/RestAPI/EmployeeActions';
import AddCircleRoundedIcon from '@mui/icons-material/AddCircleRounded';
import SearchTextField from '../Shared/SearchTextField';
import EmployeeDialog from './EmployeeDialog';
import SkeletonTableEmployees from '../SkeletonTables/SkeletonTableEmployees';
import { useTranslation } from 'react-i18next';

export default function Employees() {
	const [showDialog, setShowDialog] = React.useState(false);
	const [focusedEmployee, setFocusedEmployee] = React.useState();
	const [data, setData] = React.useState();
	const [isLoading, setIsLoading] = React.useState(true);
	const [dialogMode, setDialogMode] = React.useState('');
	const { t } = useTranslation(['generalTranslation', 'employeeTranslation']);

	const handleSearch = async (searchName) => {
		setIsLoading(true);
		const result = await filterEmployees(searchName);
		setData(result.hasOwnProperty('data') ? result.data : result);
		setIsLoading(false);
	};
	React.useEffect(() => {
		const getData = async () => {
			const result = await getAllEmployees();
			setData(result.hasOwnProperty('data') ? result.data : result);
			setIsLoading(false);
		};
		getData();
	}, []);
	const handleAddClick = () => {
		setDialogMode('post');
		setFocusedEmployee({ id: 0, name: '', surname: '' });
		setShowDialog(true);
	};
	const handleEditClick = (employee) => {
		setDialogMode('update');
		setFocusedEmployee(employee);
		setShowDialog(true);
	};
	const handleDeleteClick = (employee) => {
		setDialogMode('delete');
		setFocusedEmployee(employee);
		setShowDialog(true);
	};
	const handleDialogSuccess = async () => {
		setIsLoading(true);
		setShowDialog(false);
		const result = await getAllEmployees();
		setData(result.hasOwnProperty('data') ? result.data : result);
		setIsLoading(false);
	};
	const getDialogTitle = () => {
		switch (dialogMode) {
			case 'post':
				return t('addEmployee', { ns: 'employeeTranslation' });
			case 'update':
				return t('editEmployee', { ns: 'employeeTranslation' });
			case 'delete':
				return t('deleteEmployee', { ns: 'employeeTranslation' });
		}
	};
	const dialog = () => {
		return (
			<Dialog open={showDialog} maxWidth={'sm'} onClose={() => setShowDialog(false)}>
				<DialogTitle>{getDialogTitle()}</DialogTitle>
				<EmployeeDialog
					employee={focusedEmployee}
					handleCancelDialog={() => setShowDialog(false)}
					handleDialogSuccess={handleDialogSuccess}
					mode={dialogMode}
				/>
			</Dialog>
		);
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
						{t('add')}
					</Button>
					<SearchTextField isLoading={isLoading} handleSearch={handleSearch} />
				</Stack>
				{isLoading ? (
					<SkeletonTableEmployees />
				) : (
					<div>
						{dialog()}
						<TableContainer className={'EmployeesTable'}>
							<Scrollbars
								autoHeight={true}
								autoHeightMin={0}
								autoHeightMax={460}
								autoHide
								autoHideTimeout={750}
								autoHideDuration={500}
							>
								<Table stickyHeader>
									<TableBody>
										{data.map((row) => (
											<TableRow key={row.name}>
												<TableCell component="th" scope="row">
													{row.name}
												</TableCell>
												<TableCell align="right">{row.surname}</TableCell>
												<TableCell align="right">
													<Box>
														<IconButton
															aria-label="delete"
															size="small"
															onClick={() => handleEditClick(row)}
														>
															<EditIcon fontSize="small" />
														</IconButton>
														<IconButton
															aria-label="delete"
															size="small"
															color={'error'}
															onClick={() => handleDeleteClick(row)}
														>
															<DeleteIcon fontSize="small" />
														</IconButton>
													</Box>
												</TableCell>
											</TableRow>
										))}
									</TableBody>
								</Table>
							</Scrollbars>
						</TableContainer>
					</div>
				)}
			</Paper>
		</div>
	);
}

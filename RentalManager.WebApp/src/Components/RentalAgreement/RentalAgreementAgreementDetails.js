import Grid from '@mui/material/Unstable_Grid2';
import {
	Box,
	Divider,
	Stack,
	Table,
	TableBody,
	TableCell,
	TableContainer,
	TableRow,
	TextField,
	Typography,
} from '@mui/material';
import EngineeringIcon from '@mui/icons-material/Engineering';
import ConstructionIcon from '@mui/icons-material/Construction';
import * as React from 'react';
import EmployeesSelect from '../Employees/EmployeesSelect';
import RentalEquipmentSelect from '../RentalEquipment/RentalEquipmentSelect';
import AttachMoneyIcon from '@mui/icons-material/AttachMoney';
import CalendarMonthIcon from '@mui/icons-material/CalendarMonth';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { DatePicker, LocalizationProvider } from '@mui/x-date-pickers';
import CommentIcon from '@mui/icons-material/Comment';
import { useRecoilState } from 'recoil';
import { rentalAgreementDetailsAtom } from '../Atoms/RentalAgreementDetailsAtoms';
import { useTranslation } from 'react-i18next';
import ValidatedTextField from '../Shared/ValidatedTextField';
import {
	ValidateDateAdded,
	ValidateDeposit,
	ValidateTransportFrom,
	ValidateTransportTo,
} from '../../Actions/Validations/ValidateRentalAgreement';

export default function RentalAgreementAgreementDetails({ mode }) {
	const [agreement, setAgreement] = useRecoilState(rentalAgreementDetailsAtom);
	const { t } = useTranslation([
		'agreementTranslation',
		'equipmentTranslation',
		'employeeTranslation',
	]);
	const handleChange = (event) => {
		let newValue = event.target.value;
		if (['transportTo', 'transportFrom', 'deposit'].includes(event.target.name)) {
			newValue = newValue.replace(/\D/g, '');
		}
		const newAgreement = {
			...agreement,
			[event.target.name]: newValue,
		};
		setAgreement(newAgreement);
	};
	const handleChangeDateAdded = (event) => {
		setAgreement({
			...agreement,
			dateAdded: event,
		});
	};
	return (
		<Box sx={{ overflow: 'hidden', maxWidth: '800px' }}>
			<Stack spacing={2}>
				{mode === 'info' || mode === 'delete' ? (
					<Grid container spacing={2} sx={{ margin: '0 !important' }}>
						<Grid xs={12} md={6} sx={{ paddingTop: '0 !important' }}>
							<div>
								<Stack
									direction={'row'}
									className={'ClientUpdateDialogStack'}
									sx={{
										marginBottom: '24px',
									}}
								>
									<EngineeringIcon className={'DividerIcon'} />
									<Typography variant="h6" className={'MarginTopBottomAuto'}>
										{t('employee', { ns: 'employeeTranslation' })}
									</Typography>
								</Stack>
								<TextField
									margin="dense"
									label={t('nameAndSurname', { ns: 'employeeTranslation' })}
									fullWidth
									variant="outlined"
									value={
										agreement.employee.name + ' ' + agreement.employee.surname
									}
									InputProps={
										mode === 'delete' || mode === 'info'
											? { readOnly: true }
											: null
									}
								/>
							</div>
						</Grid>
						<Grid xs={12} md={6} sx={{ paddingTop: '0 !important' }}>
							<Stack
								direction={'row'}
								className={'ClientUpdateDialogStack'}
								sx={{
									marginBottom: '24px',
								}}
							>
								<ConstructionIcon className={'DividerIcon'} />
								<Typography variant="h6" className={'MarginTopBottomAuto'}>
									{t('equipment', { ns: 'equipmentTranslation' })}
								</Typography>
							</Stack>
							<TableContainer sx={{ width: '100%' }}>
								<Table size="small">
									<TableBody>
										{agreement.rentalEquipment.map((row) => (
											<TableRow
												key={row.name}
												sx={{
													'&:last-child td, &:last-child th': {
														border: 0,
													},
												}}
											>
												<TableCell component="th" scope="row">
													{row.name}
												</TableCell>
												<TableCell align="right">
													{row.price + ' zł'}
												</TableCell>
											</TableRow>
										))}
										<TableRow>
											<TableCell colSpan={2} align="right">
												{t('total', { ns: 'equipmentTranslation' })}
												{': '}
												{agreement.rentalEquipment
													.map((x) => x.price)
													.reduce((partialSum, a) => partialSum + a, 0) +
													' zł'}
											</TableCell>
										</TableRow>
									</TableBody>
								</Table>
							</TableContainer>
						</Grid>
					</Grid>
				) : (
					<Grid
						container
						sx={{
							paddingLeft: '8px',
							paddingRight: '8px',
							paddingBottom: '8px',
						}}
					>
						<Grid xs={12} md={6}>
							<EmployeesSelect agreement={agreement} setAgreement={setAgreement} />
						</Grid>
						<Grid xs={12} md={12}>
							<RentalEquipmentSelect
								agreement={agreement}
								setAgreement={setAgreement}
							/>
						</Grid>
					</Grid>
				)}
				<Divider />
				<Stack direction={'row'} className={'DialogTopStack'}>
					<AttachMoneyIcon className={'DividerIcon'} />
					<Typography variant="h6" className={'MarginTopBottomAuto'}>
						{t('costs')}
					</Typography>
				</Stack>
				<Grid container spacing={2}>
					<Grid xs={12} sm={12} md={6}>
						<ValidatedTextField
							name={'transportTo'}
							label={t('transportTo')}
							value={agreement.transportTo}
							onChange={handleChange}
							validationFunction={ValidateTransportTo}
							isRequired={true}
							isReadonly={mode === 'delete' || mode === 'info'}
						/>
					</Grid>
					<Grid xs={12} sm={7} md={6}>
						<ValidatedTextField
							name={'transportFrom'}
							label={t('transportFrom')}
							value={agreement.transportFrom ?? ''}
							onChange={handleChange}
							validationFunction={ValidateTransportFrom}
							isReadonly={mode === 'delete' || mode === 'info'}
						/>
					</Grid>
					<Grid xs={6} md={6}>
						<ValidatedTextField
							name={'deposit'}
							label={t('deposit')}
							value={agreement.deposit ?? ''}
							onChange={handleChange}
							validationFunction={ValidateDeposit}
							isRequired={true}
							isReadonly={mode === 'delete' || mode === 'info'}
						/>
					</Grid>
				</Grid>
				<Divider />
				<Stack direction={'row'} className={'DialogTopStack'}>
					<CalendarMonthIcon className={'DividerIcon'} />
					<Typography variant="h6" className={'MarginTopBottomAuto'}>
						{t('dates')}
					</Typography>
				</Stack>
				<Grid container spacing={2}>
					<Grid xs={12} sm={6}>
						<LocalizationProvider dateAdapter={AdapterDayjs}>
							<DatePicker
								name={'dateAdded'}
								label={t('dateAdded')}
								value={agreement.dateAdded}
								onChange={handleChangeDateAdded}
								readOnly={mode === 'info' || mode === 'delete'}
								renderInput={(params) => (
									<TextField
										{...params}
										fullWidth
										helperText="Required"
										error={ValidateDateAdded(agreement.dateAdded).length !== 0}
									/>
								)}
							/>
						</LocalizationProvider>
					</Grid>
				</Grid>
				<Divider />
				<Stack direction={'row'} className={'DialogTopStack'}>
					<CommentIcon className={'DividerIcon'} />
					<Typography variant="h6" className={'MarginTopBottomAuto'}>
						{t('comment')}
					</Typography>
				</Stack>
				<Grid container spacing={2}>
					<Grid xs={12}>
						<ValidatedTextField
							name={'comment'}
							label={t('comment')}
							value={agreement.comment ?? ''}
							onChange={handleChange}
							validationFunction={ValidateTransportFrom}
							isReadonly={mode === 'delete' || mode === 'info'}
						/>
					</Grid>
				</Grid>
			</Stack>
		</Box>
	);
}

import {
	Autocomplete,
	Button,
	IconButton,
	InputAdornment,
	Stack,
	Table,
	TableBody,
	TableCell,
	TableContainer,
	TableHead,
	TableRow,
	TextField,
} from '@mui/material';
import dayjs from 'dayjs';
import { useState } from 'react';
import * as React from 'react';
import { DatePicker, LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import CheckIcon from '@mui/icons-material/Check';
import useMediaQuery from '@mui/material/useMediaQuery';
import { useTheme } from '@mui/material/styles';
import { addPayment, deletePayment, getPayment } from '../../Actions/RestAPI/PaymentActions';
import DeleteIcon from '@mui/icons-material/Delete';
import { useRecoilState } from 'recoil';
import { rentalAgreementDetailsAtom } from '../Atoms/RentalAgreementDetailsAtoms';
import { useTranslation } from 'react-i18next';
import { ValidatePayment } from '../../Actions/Validations/ValidatePayment';

const paymentOptions = ['Card', 'Cash', 'Transfer'];

export default function Payments({ mode, setIsLoading }) {
	const theme = useTheme();
	const [agreement, setAgreement] = useRecoilState(rentalAgreementDetailsAtom);
	const isSmallScreen = useMediaQuery(theme.breakpoints.down('sm'));
	const { t } = useTranslation(['paymentTranslation', 'generalTranslation']);
	const [newPayment, setNewPayment] = useState({
		id:
			agreement.payments.length > 0
				? agreement.payments.map((x) => x.id).sort((a, b) => b - a)[0] + 1
				: 0,
		amount: '',
		method: paymentOptions[0],
		from: dayjs().format('YYYY-MM-DDTHH:mm:ss'),
		to: dayjs().add(1, 'month').format('YYYY-MM-DDTHH:mm:ss'),
	});

	React.useEffect(() => {
		const getData = async () => {
			const result = await getPayment(agreement.id);
			setAgreement({
				...agreement,
				payments: result.hasOwnProperty('data') ? result.data : result,
			});
			if (typeof setIsLoading !== 'undefined') {
				setIsLoading(false);
			}
		};
		getData();
	}, []);

	const handleChangePrice = (e) => {
		setNewPayment({
			...newPayment,
			amount: e.target.value.replace(/\D/g, ''),
		});
	};
	const handleChangeMethod = (e, value) => {
		setNewPayment({ ...newPayment, method: value });
	};
	const handleChangeFrom = (date) => {
		setNewPayment({
			...newPayment,
			from: date.format('YYYY-MM-DDTHH:mm:ss'),
		});
	};
	const handleChangeTo = (date) => {
		setNewPayment({
			...newPayment,
			to: date.format('YYYY-MM-DDTHH:mm:ss'),
		});
	};
	const handleAddPayment = async () => {
		if (mode === 'post') {
			setAgreement({
				...agreement,
				payments: [...agreement.payments, newPayment],
			});
			setNewPayment({
				...newPayment,
				amount: '',
				id: newPayment.id + 1,
			});
		} else {
			if (typeof setIsLoading !== 'undefined') {
				setIsLoading(true);
			}
			addPayment(agreement.id, newPayment)
				.then((result) => {
					setAgreement({
						...agreement,
						payments: [...agreement.payments, newPayment],
					});
					setNewPayment({
						...newPayment,
						amount: '',
						id: newPayment.id + 1,
					});
					if (typeof setIsLoading !== 'undefined') {
						setIsLoading(false);
					}
				})
				.catch((err) => {
					console.log(err);
					if (typeof setIsLoading !== 'undefined') {
						setIsLoading(false);
					}
				});
		}
	};
	const handleDeletePayment = async (id) => {
		if (mode === 'post') {
			setAgreement({
				...agreement,
				payments: [...agreement.payments.filter((x) => x.id !== id)],
			});
		} else {
			if (typeof setIsLoading !== 'undefined') {
				setIsLoading(true);
			}
			deletePayment(id)
				.then((result) => {
					setAgreement({
						...agreement,
						payments: [...agreement.payments.filter((x) => x.id !== id)],
					});
					if (typeof setIsLoading !== 'undefined') {
						setIsLoading(false);
					}
				})
				.catch((err) => {
					console.log(err);
					if (typeof setIsLoading !== 'undefined') {
						setIsLoading(false);
					}
				});
		}
	};
	return (
		<Stack direction={'column'} sx={{ padding: '15px' }}>
			{mode === 'update' || mode === 'post' ? (
				<Stack
					direction={'column'}
					alignItems={'center'}
					justifyContent={'center'}
					spacing={2}
				>
					<Stack
						direction={'row'}
						alignItems={'center'}
						justifyContent={'center'}
						spacing={2}
						sx={{ width: '100%' }}
					>
						<Autocomplete
							disableClearable
							options={paymentOptions}
							onChange={handleChangeMethod}
							value={newPayment.method}
							fullWidth
							getOptionLabel={(option) => {
								return t(option.toString());
							}}
							renderInput={(params) => (
								<TextField
									{...params}
									label={t('paymentMethod')}
									size={isSmallScreen ? 'small' : 'medium'}
								/>
							)}
						/>
						<TextField
							margin="dense"
							label={t('amount')}
							variant="outlined"
							fullWidth
							value={newPayment.amount}
							size={isSmallScreen ? 'small' : 'medium'}
							InputProps={{
								endAdornment: <InputAdornment position="end">zł</InputAdornment>,
							}}
							onChange={handleChangePrice}
						/>
					</Stack>
					<Stack
						direction={'row'}
						alignItems={'center'}
						justifyContent={'center'}
						spacing={2}
						sx={{ width: '100%' }}
					>
						<LocalizationProvider dateAdapter={AdapterDayjs}>
							<DatePicker
								name={'dateAdded'}
								label={t('paidFrom')}
								value={newPayment.from}
								fullWidth
								onChange={handleChangeFrom}
								renderInput={(params) => (
									<TextField
										{...params}
										fullWidth
										size={isSmallScreen ? 'small' : 'medium'}
									/>
								)}
							/>
							<DatePicker
								name={'dateAdded'}
								label={t('paidTo')}
								value={newPayment.to}
								fullWidth
								onChange={handleChangeTo}
								renderInput={(params) => (
									<TextField
										{...params}
										fullWidth
										size={isSmallScreen ? 'small' : 'medium'}
									/>
								)}
							/>
						</LocalizationProvider>
					</Stack>
					<Button
						variant="contained"
						endIcon={<CheckIcon />}
						disabled={!ValidatePayment(newPayment)}
						onClick={handleAddPayment}
					>
						{t('add', { ns: 'generalTranslation' })}
					</Button>
				</Stack>
			) : null}
			<TableContainer>
				<Table size={isSmallScreen ? 'small' : 'medium'}>
					<TableHead>
						<TableRow>
							<TableCell>{t('method')}</TableCell>
							<TableCell align="right">{t('amount')}</TableCell>
							<TableCell align="right">{t('from')}</TableCell>
							<TableCell align="right">{t('to')}</TableCell>
							{mode === 'update' || mode === 'post' ? <TableCell /> : <></>}
						</TableRow>
					</TableHead>
					<TableBody>
						{agreement.payments.length > 0 ? (
							agreement.payments.map((row) => (
								<TableRow
									key={row.id}
									sx={{
										'&:last-child td, &:last-child th': {
											border: 0,
										},
									}}
								>
									<TableCell component="th" scope="row">
										{t(row.method)}
									</TableCell>
									<TableCell align="right">{row.amount + ' zł'}</TableCell>
									<TableCell align="right">
										{dayjs(row.from).format('DD.MM.YYYY')}
									</TableCell>
									<TableCell align="right">
										{dayjs(row.to).format('DD.MM.YYYY')}
									</TableCell>
									{mode === 'update' || mode === 'post' ? (
										<TableCell align="right">
											<IconButton
												aria-label="delete"
												size="small"
												color={'error'}
												onClick={() => handleDeletePayment(row.id)}
											>
												<DeleteIcon fontSize="small" />
											</IconButton>
										</TableCell>
									) : (
										<></>
									)}
								</TableRow>
							))
						) : (
							<TableRow>
								<TableCell component="th" scope="row" colSpan={4} align={'center'}>
									<b>{t('noPayments')}</b>
								</TableCell>
							</TableRow>
						)}

						<TableRow>
							<TableCell rowSpan={1} />
							<TableCell colSpan={1} align="right">
								{t('total') + ': '}
								{agreement.payments
									.map((x) => x.amount)
									.reduce((partialSum, a) => partialSum + Number(a), 0) + ' zł'}
							</TableCell>
						</TableRow>
					</TableBody>
				</Table>
			</TableContainer>
		</Stack>
	);
}

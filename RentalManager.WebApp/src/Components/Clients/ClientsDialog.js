import * as React from 'react';
import ValidateClient, {
	validateCity,
	validateEmail,
	validateIdCard,
	validateName,
	validatePhoneNumber,
	validateStreet,
	validateSurname,
} from '../../Actions/Validations/ValidateClient';
import { addClient, deleteClient, updateClient } from '../../Actions/RestAPI/ClientActions';
import {
	Backdrop,
	Button,
	CircularProgress,
	DialogContent,
	Divider,
	Stack,
	Typography,
} from '@mui/material';
import PersonIcon from '@mui/icons-material/Person';
import Grid from '@mui/material/Unstable_Grid2';
import ContactPhoneIcon from '@mui/icons-material/ContactPhone';
import HomeIcon from '@mui/icons-material/Home';
import DoneIcon from '@mui/icons-material/Done';
import CancelIcon from '@mui/icons-material/Cancel';
import DeleteIcon from '@mui/icons-material/Delete';
import { useRecoilState } from 'recoil';
import { clientAtom } from '../Atoms/ClientAtoms';
import ValidatedTextField from '../Shared/ValidatedTextField';
import { useTranslation } from 'react-i18next';

export default function ClientsDialog({
	handleCancelDialog,
	handleDialogSuccess,
	mode,
	showDialogButtons,
}) {
	const [isLoading, setIsLoading] = React.useState(false);
	const { t } = useTranslation(['clientTranslation']);
	const [client, setClient] = useRecoilState(clientAtom);
	const handleChange = (event) => {
		let newValue = event.target.value;
		if (event.target.name === 'idCard') {
			newValue = newValue.toUpperCase();
		}
		const newClient = {
			...client,
			[event.target.name]: newValue,
		};
		setClient(newClient);
	};
	const handleSave = async () => {
		if (ValidateClient(client)) {
			setIsLoading(true);
			switch (mode) {
				case 'delete':
					await deleteClient(client.id);
					break;
				case 'update':
					await updateClient(client);
					break;
				case 'post':
					await addClient(client);
					break;
			}
			setIsLoading(false);
			handleDialogSuccess(mode, client);
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
						<PersonIcon className={'DividerIcon'} />
						<Typography variant="h6" className={'MarginTopBottomAuto'}>
							{t('personalInformation')}
						</Typography>
					</Stack>
					<Grid container spacing={2}>
						<Grid xs={6} md={5}>
							<ValidatedTextField
								name="name"
								label={t('name')}
								value={client.name}
								onChange={handleChange}
								validationFunction={validateName}
								isRequired={true}
								isReadonly={mode === 'delete' || mode === 'info'}
							/>
						</Grid>
						<Grid xs={6} md={7}>
							<ValidatedTextField
								name="surname"
								label={t('surname')}
								value={client.surname}
								onChange={handleChange}
								validationFunction={validateSurname}
								isRequired={true}
								isReadonly={mode === 'delete' || mode === 'info'}
							/>
						</Grid>
						<Grid xs={6} md={5}>
							<ValidatedTextField
								name="idCard"
								label={t('idCard')}
								value={client.idCard}
								onChange={handleChange}
								validationFunction={validateIdCard}
								isReadonly={mode === 'delete' || mode === 'info'}
								mask="aaa 999999"
							/>
						</Grid>
					</Grid>
					<Divider />
					<Stack direction={'row'} className={'DialogTopStack'}>
						<ContactPhoneIcon className={'DividerIcon'} />
						<Typography variant="h6" className={'MarginTopBottomAuto'}>
							{t('contactInformation')}
						</Typography>
					</Stack>
					<Grid container spacing={2} columns={{ xs: 6, sm: 12 }}>
						<Grid xs={6} md={5}>
							<ValidatedTextField
								name="phoneNumber"
								label={t('phoneNumber')}
								value={client.phoneNumber}
								onChange={handleChange}
								validationFunction={validatePhoneNumber}
								isRequired={true}
								isReadonly={mode === 'delete' || mode === 'info'}
								mask="999 999 999"
							/>
						</Grid>
						<Grid xs={6} md={7}>
							<ValidatedTextField
								name={'email'}
								label={t('email')}
								value={client.email}
								onChange={handleChange}
								validationFunction={validateEmail}
								isReadonly={mode === 'delete' || mode === 'info'}
							/>
						</Grid>
					</Grid>
					<Divider />
					<Stack direction={'row'} className={'DialogTopStack'}>
						<HomeIcon className={'DividerIcon'} />
						<Typography variant="h6" className={'MarginTopBottomAuto'}>
							{t('address')}
						</Typography>
					</Stack>
					<Grid container spacing={2} columns={{ xs: 24, sm: 12 }}>
						<Grid xs={24} md={5}>
							<ValidatedTextField
								name={'city'}
								label={t('city')}
								value={client.city}
								onChange={handleChange}
								validationFunction={validateCity}
								isRequired={true}
								isReadonly={mode === 'delete' || mode === 'info'}
							/>
						</Grid>
						<Grid xs={13} md={7}>
							<ValidatedTextField
								name={'street'}
								label={t('street')}
								value={client.street}
								onChange={handleChange}
								validationFunction={validateStreet}
								isRequired={true}
								isReadonly={mode === 'delete' || mode === 'info'}
							/>
						</Grid>
					</Grid>
				</Stack>
			</DialogContent>
			{mode !== 'info' && showDialogButtons ? (
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
							disabled={!ValidateClient(client)}
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

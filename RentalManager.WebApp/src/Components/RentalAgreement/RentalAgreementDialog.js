import {
	AppBar,
	Backdrop,
	Button,
	CircularProgress,
	Stack,
	Tab,
	Tabs,
} from '@mui/material';
import * as React from 'react';
import SwipeableViews from 'react-swipeable-views';
import ClientsDialog from '../Clients/ClientsDialog';
import './RentalAgreement.js.css';
import DescriptionIcon from '@mui/icons-material/Description';
import PersonIcon from '@mui/icons-material/Person';
import RentalAgreementAgreementDetails from './RentalAgreementAgreementDetails';
import DeleteIcon from '@mui/icons-material/Delete';
import DoneIcon from '@mui/icons-material/Done';
import CancelIcon from '@mui/icons-material/Cancel';
import ValidateClient from '../../Actions/Validations/ValidateClient';
import { Scrollbars } from 'react-custom-scrollbars-2';
import ValidateRentalAgreement from '../../Actions/Validations/ValidateRentalAgreement';
import AttachMoneyIcon from '@mui/icons-material/AttachMoney';
import {
	deleteClient,
	updateClient,
} from '../../Actions/RestAPI/ClientActions';
import {
	deleteRentalAgreement,
	updateRentalAgreement,
} from '../../Actions/RestAPI/RentalAgreementActions';
import Payments from '../Payments/Payments';

export default function RentalAgreementDialog({
	agreement,
	mode,
	handleCancelDialog,
	handleDialogSuccess,
}) {
	const [value, setValue] = React.useState(0);
	const [agreementDialog, setAgreementDialog] = React.useState(
		agreement ? agreement : null
	);
	const [isLoading, setIsLoading] = React.useState(false);
	const handleChange = (event, newValue) => {
		setValue(newValue);
	};
	const handleChangeIndex = (index) => {
		setValue(index);
	};

	const handleSave = async () => {
		const resAgreement = ValidateRentalAgreement(agreementDialog);
		if (agreementDialog.client) {
			const resClient = ValidateClient(agreementDialog.client);
			if (resAgreement.length === 0 && resClient.length === 0) {
				setIsLoading(true);
				switch (mode) {
					case 'delete':
						await Promise.all([
							deleteClient(agreementDialog.client.id),
							deleteRentalAgreement(agreementDialog.id),
						]);
						break;
					case 'update':
						await Promise.all([
							updateClient(agreementDialog.client),
							updateRentalAgreement(agreementDialog),
						]);
						break;
				}
				setIsLoading(false);
				handleDialogSuccess(mode);
			}
		}
	};

	return (
		<div>
			<Backdrop
				sx={{
					color: '#fff',
					zIndex: (theme) => theme.zIndex.drawer + 1,
				}}
				open={isLoading}
			>
				<CircularProgress />
			</Backdrop>
			<AppBar position="static">
				<Tabs
					value={value}
					onChange={handleChange}
					indicatorColor="primary"
					textColor="inherit"
					variant="fullWidth"
					centered={true}
				>
					<Tab icon={<PersonIcon />} label="Client" />
					<Tab icon={<DescriptionIcon />} label="Agreement" />
					<Tab icon={<AttachMoneyIcon />} label="Payments" />
				</Tabs>
			</AppBar>
			<SwipeableViews index={value} onChangeIndex={handleChangeIndex}>
				<div className={'rentalAgreementSlide'}>
					<Scrollbars
						autoHeight={true}
						autoHeightMin={0}
						autoHeightMax={'57vh'}
						autoHide
						autoHideTimeout={750}
						autoHideDuration={500}
					>
						<ClientsDialog
							client={agreementDialog.client}
							handleCancelDialog={() => null}
							handleDialogSuccess={() => null}
							mode={mode === 'delete' ? 'info' : mode}
							setClient={(newClient) =>
								setAgreementDialog({
									...agreementDialog,
									client: newClient,
								})
							}
						/>
					</Scrollbars>
				</div>
				<div
					className={'rentalAgreementSlide'}
					style={{ padding: '20px 24px' }}
				>
					<Scrollbars
						autoHeight={true}
						autoHeightMin={0}
						autoHeightMax={'57vh'}
						autoHide
						autoHideTimeout={750}
						autoHideDuration={500}
					>
						<RentalAgreementAgreementDetails
							mode={mode}
							agreement={agreementDialog}
							setAgreement={setAgreementDialog}
						/>
					</Scrollbars>
				</div>
				<div className={'rentalAgreementSlide'}>
					<Scrollbars
						autoHeight={true}
						autoHeightMin={0}
						autoHeightMax={'57vh'}
						autoHide
						autoHideTimeout={750}
						autoHideDuration={500}
					>
						<Payments
							agreement={agreementDialog}
							mode={mode}
							setAgreement={setAgreementDialog}
							isLoading={isLoading}
							setIsLoading={setIsLoading}
						/>
					</Scrollbars>
				</div>
			</SwipeableViews>
			<Stack
				direction="row"
				justifyContent="space-between"
				className={'DialogStack'}
			>
				{mode === 'delete' && (
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
				)}

				{mode === 'update' && (
					<Button
						variant="contained"
						color={'success'}
						size="large"
						endIcon={<DoneIcon />}
						onClick={handleSave}
						className={'DialogButton'}
						disabled={
							!agreementDialog.client ||
							ValidateClient(agreementDialog.client).length !==
								0 ||
							ValidateRentalAgreement(agreementDialog).length !==
								0
						}
					>
						Save
					</Button>
				)}
				<Button
					variant="outlined"
					color={'primary'}
					size="large"
					endIcon={<CancelIcon />}
					onClick={() => handleCancelDialog(false)}
					className={'DialogButton'}
				>
					Cancel
				</Button>
			</Stack>
		</div>
	);
}

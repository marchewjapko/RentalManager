import {
	Avatar,
	Box,
	Button,
	Stack,
	Step,
	StepButton,
	StepContent,
	Stepper,
} from '@mui/material';
import PersonIcon from '@mui/icons-material/Person';
import DescriptionIcon from '@mui/icons-material/Description';
import AttachMoneyIcon from '@mui/icons-material/AttachMoney';
import { useTheme } from '@mui/material/styles';
import * as React from 'react';
import dayjs from 'dayjs';
import Clients from '../Clients/Clients';
import ValidateRentalAgreement from '../../Actions/ValidateRentalAgreement';
import RentalAgreementAgreementDetails from './RentalAgreementAgreementDetails';
import { Scrollbars } from 'react-custom-scrollbars-2';
import useMediaQuery from '@mui/material/useMediaQuery';
import TempNavigation from '../Shared/TempNavigation';

function GetSteps(newRentalAgreement, setNewRentalAgreement) {
	return [
		{
			label: 'Choose client',
			content: (
				<Clients
					isCheckable={true}
					client={newRentalAgreement.client}
					setClient={(client) =>
						setNewRentalAgreement({
							...newRentalAgreement,
							client: client,
						})
					}
				/>
			),
		},
		{
			label: 'Fill the details',
			content: (
				<RentalAgreementAgreementDetails
					mode={'post'}
					agreement={newRentalAgreement}
					setAgreement={setNewRentalAgreement}
				/>
			),
		},
		{
			label: 'Add payment',
			content: <div>:P</div>,
		},
	];
}

function GetStepIcon(index, isCompleted, isActive, isValid) {
	const theme = useTheme();
	const icons = [
		<PersonIcon color={'action'} />,
		<DescriptionIcon color={'action'} />,
		<AttachMoneyIcon color={'action'} />,
	];
	let backGroundColor;
	if (isActive) {
		backGroundColor = { bgcolor: theme.palette.primary.main };
	} else if (isValid === false) {
		backGroundColor = { bgcolor: theme.palette.error.dark };
	} else if (isValid === true) {
		backGroundColor = { bgcolor: theme.palette.success.main };
	}
	return <Avatar sx={backGroundColor}>{icons[index]}</Avatar>;
}

export default function AddRentalEquipmentForm() {
	const [activeStep, setActiveStep] = React.useState(0);
	const [completed, setCompleted] = React.useState({});
	const theme = useTheme();
	const isSmallScreen = useMediaQuery(theme.breakpoints.down('sm'));
	const [newRentalAgreement, setNewRentalAgreement] = React.useState({
		id: 0,
		isActive: true,
		employee: null,
		client: null,
		rentalEquipment: [],
		comment: '',
		deposit: '',
		transportFrom: null,
		transportTo: '',
		payments: [],
		dateAdded: dayjs(),
	});
	console.log(
		GetSteps(newRentalAgreement, setNewRentalAgreement).findIndex(
			(step, i) => !(i in completed)
		)
	);
	const [validationState, setValidationState] = React.useState({
		'Choose client': null,
		'Fill the details': null,
		'Add payment': null,
	});
	const isLastStep = () => {
		return (
			activeStep ===
			GetSteps(newRentalAgreement, setNewRentalAgreement).length - 1
		);
	};
	const allStepsCompleted = () => {
		return (
			Object.keys(completed).length ===
			GetSteps(newRentalAgreement, setNewRentalAgreement).length
		);
	};
	const handleNext = () => {
		validateCurrentStep();
		const newActiveStep =
			isLastStep() && !allStepsCompleted()
				? GetSteps(newRentalAgreement, setNewRentalAgreement).findIndex(
						(step, i) => !(i in completed)
				  )
				: activeStep + 1;
		setActiveStep(newActiveStep);
	};
	const handleBack = () => {
		validateCurrentStep();
		setActiveStep((prevActiveStep) => prevActiveStep - 1);
	};
	const handleStep = (step) => () => {
		validateCurrentStep();
		setActiveStep(step);
	};
	const handleComplete = () => {
		const newCompleted = completed;
		newCompleted[activeStep] = true;
		setCompleted(newCompleted);
	};
	const removeFromCompleted = () => {
		const newCompleted = completed;
		delete newCompleted[activeStep];
		setCompleted(newCompleted);
	};

	console.log('completed', completed);

	const validateCurrentStep = () => {
		const validationResult = ValidateRentalAgreement(newRentalAgreement);
		console.log('validationResult', validationResult);
		switch (activeStep) {
			case 0:
				setValidationState({
					...validationState,
					'Choose client': !validationResult.includes('noClient'),
				});
				if (!validationResult.includes('noClient')) {
					handleComplete();
				} else {
					removeFromCompleted();
				}
				break;
			case 1:
				setValidationState({
					...validationState,
					'Fill the details':
						!validationResult.filter(
							(x) => x !== 'noClient' && x !== 'noPayments'
						).length > 0,
				});
				if (
					!validationResult.filter(
						(x) => x !== 'noClient' && x !== 'noPayments'
					).length > 0
				) {
					handleComplete();
				} else {
					removeFromCompleted();
				}
				break;
			case 2:
				setValidationState({
					...validationState,
					'Add payment': !validationResult.includes('noPayments'),
				});
				if (!validationResult.includes('noPayments')) {
					handleComplete();
				} else {
					removeFromCompleted();
				}
				break;
		}
	};

	console.log('validationState', validationState);

	return (
		<Box sx={{ padding: '10px', height: '90vh', maxWidth: '2000px' }}>
			<Stepper nonLinear activeStep={activeStep} orientation="vertical">
				{GetSteps(newRentalAgreement, setNewRentalAgreement).map(
					(step, index) => (
						<Step key={step.label} completed={completed[index]}>
							<StepButton
								color="inherit"
								onClick={handleStep(index)}
								icon={GetStepIcon(
									index,
									Object.keys(completed).includes(
										index.toString()
									),
									index === activeStep,
									validationState[step.label]
								)}
							>
								{step.label}
							</StepButton>
							<StepContent
								TransitionProps={{ unmountOnExit: false }}
							>
								<Scrollbars
									autoHide
									autoHeight={true}
									autoHeightMin={0}
									autoHeightMax={isSmallScreen ? 460 : '60vh'}
									autoHideTimeout={750}
									autoHideDuration={500}
								>
									{step.content}
								</Scrollbars>
								<Stack
									direction="row"
									spacing={1}
									justifyContent={'flex-start'}
									alignItems={'center'}
								>
									<div>
										<Button
											variant="contained"
											onClick={handleNext}
										>
											{allStepsCompleted()
												? 'Finish'
												: 'Continue'}
										</Button>
									</div>
									<div>
										<Button
											variant="outlined"
											onClick={handleBack}
											disabled={activeStep === 0}
										>
											Back
										</Button>
									</div>
								</Stack>
							</StepContent>
						</Step>
					)
				)}
			</Stepper>
			<TempNavigation />
		</Box>
	);
}
import {
	Avatar,
	Backdrop,
	Box,
	Button,
	CircularProgress,
	Stack,
	Step,
	StepButton,
	StepContent,
	Stepper,
} from '@mui/material';
import * as React from 'react';
import ValidateRentalAgreement from '../../../Actions/Validations/ValidateRentalAgreement';
import { Scrollbars } from 'react-custom-scrollbars-2';
import {
	addRentalAgreement,
	filterAgreements,
} from '../../../Actions/RestAPI/RentalAgreementActions';
import { useNavigate } from 'react-router-dom';
import { DefaultValue, useRecoilState, useRecoilValue } from 'recoil';
import { rentalAgreementAtom } from '../../Atoms/RentalAgreementAtoms';
import GetFormSteps from './GetFromSteps';
import GetStepIcon from './GetStepIcon';

export default function AddRentalEquipmentForm() {
	const [activeStep, setActiveStep] = React.useState(0);
	const [completed, setCompleted] = React.useState({});
	const [isLoading, setIsLoading] = React.useState(false);
	const [newRentalAgreement, setRentalAgreement] =
		useRecoilState(rentalAgreementAtom);
	const navigate = useNavigate();
	const [validationState, setValidationState] = React.useState({
		'Choose client': null,
		'Fill the details': null,
		'Add payment': null,
	});
	React.useEffect(() => {
		setRentalAgreement(new DefaultValue());
	}, []);
	const isLastStep = () => {
		return activeStep === GetFormSteps().length - 1;
	};
	const allStepsCompleted = () => {
		return Object.keys(completed).length === GetFormSteps().length;
	};
	const handleNext = () => {
		validateCurrentStep();
		if (
			allStepsCompleted() &&
			ValidateRentalAgreement(newRentalAgreement).length === 0
		) {
			setIsLoading(true);
			addRentalAgreement(newRentalAgreement)
				.then(() => {
					navigate('/');
				})
				.catch((err) => console.log(err));
		} else {
			const newActiveStep =
				isLastStep() && !allStepsCompleted()
					? GetFormSteps().findIndex((step, i) => !(i in completed))
					: activeStep + 1;
			setActiveStep(newActiveStep);
		}
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
	const validateCurrentStep = () => {
		const validationResult = ValidateRentalAgreement(newRentalAgreement);
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

	return (
		<Box
			sx={{
				height: '90vh',
				width: '100%',
				display: 'flex',
				alignItems: 'flex-start',
				justifyContent: 'center',
			}}
		>
			<Backdrop
				sx={{
					color: '#fff',
					zIndex: (theme) => theme.zIndex.drawer + 1,
				}}
				open={isLoading}
			>
				<CircularProgress />
			</Backdrop>
			<Stepper nonLinear activeStep={activeStep} orientation="vertical">
				{GetFormSteps().map((step, index) => (
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
						<StepContent TransitionProps={{ unmountOnExit: false }}>
							<Scrollbars
								autoHeight={true}
								autoHeightMin={0}
								autoHeightMax={'57vh'}
								autoHide
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
										{ValidateRentalAgreement(
											newRentalAgreement
										).length === 0
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
				))}
			</Stepper>
		</Box>
	);
}

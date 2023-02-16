import {
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
import { addRentalAgreement } from '../../../Actions/RestAPI/RentalAgreementActions';
import { useNavigate } from 'react-router-dom';
import { DefaultValue, useRecoilState } from 'recoil';
import { rentalAgreementAtom } from '../../Atoms/RentalAgreementAtoms';
import GetFormSteps from './GetFromSteps';
import GetStepIcon from './GetStepIcon';
import { useTranslation } from 'react-i18next';
import ValidateClient from '../../../Actions/Validations/ValidateClient';
import { ValidatePayment } from '../../../Actions/Validations/ValidatePayment';

export default function AddRentalEquipmentForm() {
	const [activeStep, setActiveStep] = React.useState(0);
	const [completed, setCompleted] = React.useState({});
	const [isLoading, setIsLoading] = React.useState(false);
	const { t } = useTranslation(['generalTranslation', 'agreementTranslation']);
	const [newRentalAgreement, setRentalAgreement] = useRecoilState(rentalAgreementAtom);
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
		if (allStepsCompleted() && ValidateRentalAgreement(newRentalAgreement).length === 0) {
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
		switch (activeStep) {
			case 0:
				const validationResultClient = ValidateClient(newRentalAgreement.client);
				setValidationState({
					...validationState,
					'Choose client': validationResultClient,
				});
				if (!validationResultClient) {
					handleComplete();
				} else {
					removeFromCompleted();
				}
				break;
			case 1:
				const validationResultAgreement = ValidateClient(newRentalAgreement);
				setValidationState({
					...validationState,
					'Fill the details': validationResultAgreement,
				});
				if (validationResultAgreement) {
					handleComplete();
				} else {
					removeFromCompleted();
				}
				break;
			case 2:
				const validationResultPayment = newRentalAgreement
					.map((payment) => ValidatePayment(payment))
					.every((x) => x);
				setValidationState({
					...validationState,
					'Add payment': validationResultPayment,
				});
				if (validationResultPayment) {
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
								Object.keys(completed).includes(index.toString()),
								index === activeStep,
								validationState[step.label]
							)}
						>
							{t(step.label, { ns: 'agreementTranslation' })}
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
									<Button variant="contained" onClick={handleNext}>
										{ValidateRentalAgreement(newRentalAgreement).length === 0
											? t('finish')
											: t('continue')}
									</Button>
								</div>
								<div>
									<Button
										variant="outlined"
										onClick={handleBack}
										disabled={activeStep === 0}
									>
										{t('back')}
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

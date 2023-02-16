import * as React from 'react';
import './Employees.js.css';
import { Button, Fade, Paper, Skeleton, Stack, Typography } from '@mui/material';
import { getAllEmployees } from '../../Actions/RestAPI/EmployeeActions';
import AddCircleRoundedIcon from '@mui/icons-material/AddCircleRounded';
import { useTranslation } from 'react-i18next';
import { TransitionGroup } from 'react-transition-group';
import EmployeeCard from './EmployeeCard';
import ConfirmEmployeeDelete from './ConfirmEmployeeDelete';
import {
	employeeShowDeleteConfirmation,
	employeeShowEditDialog,
	forceEmployeeRefresh,
} from '../Atoms/EmployeeAtoms';
import { useRecoilState, useRecoilValue } from 'recoil';
import { useCallback } from 'react';

const initialCards = [
	{
		type: 'skeleton',
	},
	{
		type: 'skeleton',
	},
	{
		type: 'skeleton',
	},
	{
		type: 'skeleton',
	},
	{
		type: 'skeleton',
	},
];

function getTimeoutIn(index) {
	return 250 + 100 * index;
}

function getCardsFromResponse(response) {
	return response.map((record) => {
		return {
			type: 'data',
			content: record,
		};
	});
}
function renderCard(type, content) {
	if (type === 'skeleton') {
		return (
			<Paper elevation={0}>
				<Skeleton variant="rounded" sx={{ width: '17em', height: '5em' }} />
			</Paper>
		);
	}
	return (
		<Paper elevation={5}>
			<EmployeeCard employee={content} />
		</Paper>
	);
}

export default function Employees() {
	const [employeeCards, setEmployeeCards] = React.useState(initialCards);
	const [isLoading, setIsLoading] = React.useState(true);
	const refreshFunction = useRecoilValue(forceEmployeeRefresh);
	const { t } = useTranslation(['generalTranslation', 'employeeTranslation']);

	React.useEffect(() => {
		setIsLoading(true);
		getAllEmployees().then((result) => {
			setEmployeeCards([]);
			if (result.hasOwnProperty('data')) {
				setTimeout(() => setEmployeeCards(getCardsFromResponse(result.data)), 255);
			} else {
				setTimeout(() => setEmployeeCards(getCardsFromResponse(result)), 255);
			}
			setIsLoading(false);
		});
	}, [refreshFunction]);

	return (
		<>
			<ConfirmEmployeeDelete />
			<Stack
				direction={'column'}
				alignItems={'flex-start'}
				justifyContent={'flex-start'}
				gap={'10px'}
				padding={2}
			>
				<Typography variant={'h3'}>
					{t('employees', { ns: 'employeeTranslation' })}
				</Typography>
				<Button
					startIcon={<AddCircleRoundedIcon />}
					variant={'contained'}
					onClick={() => console.log('Added!')}
					disabled={isLoading}
				>
					{t('add')}
				</Button>
				<TransitionGroup
					style={{ display: 'flex', flexDirection: 'row', flexWrap: 'wrap', gap: '10px' }}
				>
					{employeeCards.map((x, index) => (
						<Fade key={index} timeout={{ enter: getTimeoutIn(index), exit: 250 }}>
							{renderCard(x.type, x.content)}
						</Fade>
					))}
				</TransitionGroup>
			</Stack>
		</>
	);
}

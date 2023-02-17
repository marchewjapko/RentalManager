import { Avatar, Card, Chip, Fade, Stack, Typography } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import { useRef, useState } from 'react';
import { useRecoilState } from 'recoil';
import { employeeAtom, employeeShowDeleteConfirmation } from '../Atoms/EmployeeAtoms';
import { RandomGradients } from '../Shared/RandomGradients';

export default function ({ employee }) {
	const [isMouseOver, setIsMouseOver] = useState(false);
	const [, setShowDeleteDialog] = useRecoilState(employeeShowDeleteConfirmation);
	const [, setDialogEmployee] = useRecoilState(employeeAtom);
	const randomIndex = useRef(Math.floor(Math.random() * 20));

	const handleDeleteClick = () => {
		setDialogEmployee(employee);
		setShowDeleteDialog(true);
	};

	return (
		<div style={{ position: 'relative', width: '17em', height: '6em' }}>
			<Stack
				direction={'row'}
				justifyContent={'space-around'}
				alignItems={'center'}
				sx={{
					position: 'absolute',
					top: 0,
					left: 0,
					width: '100%',
					height: '100%',
					transition: 'all 0.3s ease',
					background: RandomGradients.at(randomIndex.current).transparent,
					opacity: '0',
					backdropFilter: 'blur(0px)',
					zIndex: '1',
					'&:hover': {
						backdropFilter: 'blur(2px)',
						opacity: '1',
						background: RandomGradients.at(randomIndex.current).visible,
					},
					borderRadius: '10px',
				}}
				onMouseLeave={() => setIsMouseOver(false)}
				onMouseEnter={() => setIsMouseOver(true)}
			>
				<Fade in={isMouseOver} style={{ transitionDelay: '50ms' }} timeout={400}>
					<Chip
						variant="filled"
						color={'primary'}
						onClick={() => console.log('clicked!!')}
						label={'Edit'}
						icon={<EditIcon />}
						sx={{ padding: '10px' }}
					/>
				</Fade>
				<Fade in={isMouseOver} style={{ transitionDelay: '50ms' }} timeout={400}>
					<Chip
						variant="filled"
						color={'error'}
						icon={<DeleteIcon />}
						onClick={handleDeleteClick}
						label={'Delete'}
						sx={{ padding: '10px' }}
					/>
				</Fade>
			</Stack>
			<Card sx={{ height: '100%', borderRadius: '10px' }}>
				<Stack
					direction={'row'}
					alignItems="center"
					justifyContent="space-between"
					sx={{ padding: '10px', height: '100%' }}
					gap={'10px'}
				>
					<Avatar
						variant="rounded"
						src="https://mui.com/static/images/avatar/1.jpg"
						sx={{ width: 70, height: 70, borderRadius: '10px' }}
					/>
					<Typography
						fontWeight={700}
						variant={'h6'}
						sx={{
							display: 'flex',
							alignItems: 'center',
							flexGrow: '1',
							textAlign: 'center',
							justifyContent: 'center',
						}}
					>
						{employee.name + ' ' + employee.surname}
					</Typography>
				</Stack>
			</Card>
		</div>
	);
}

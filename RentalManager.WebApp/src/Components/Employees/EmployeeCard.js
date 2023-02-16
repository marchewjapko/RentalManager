import { Avatar, Card, Chip, Fade, Stack, Typography } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import { useState } from 'react';
import { useRecoilState } from 'recoil';
import { employeeAtom, employeeShowDeleteConfirmation } from '../Atoms/EmployeeAtoms';

export default function ({ employee }) {
	const [isMouseOver, setIsMouseOver] = useState(false);
	const [, setShowDeleteDialog] = useRecoilState(employeeShowDeleteConfirmation);
	const [, setDialogEmployee] = useRecoilState(employeeAtom);

	const handleDeleteClick = () => {
		setDialogEmployee(employee);
		setShowDeleteDialog(true);
	};

	return (
		<div style={{ position: 'relative', width: '17em', height: '5em' }}>
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
					background:
						'linear-gradient(45deg, rgba(28, 181, 224, 0.5) 0%, rgba(0, 8, 81, 0.5) 100%)',
					opacity: '0',
					backdropFilter: 'blur(0px)',
					zIndex: '1',
					'&:hover': {
						backdropFilter: 'blur(2px)',
						opacity: '1',
						background:
							'linear-gradient(45deg, rgba(28, 181, 224, 0.5) 0%, rgba(0, 8, 81, 0.5) 100%)',
					},
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
			<Card>
				<Stack
					direction={'row'}
					alignItems="center"
					justifyContent="space-between"
					sx={{ padding: '10px' }}
					gap={'10px'}
				>
					<Avatar
						variant="rounded"
						src="https://mui.com/static/images/avatar/1.jpg"
						sx={{ width: 60, height: 60 }}
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

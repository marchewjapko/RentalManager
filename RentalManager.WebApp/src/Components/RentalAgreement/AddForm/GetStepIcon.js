import { useTheme } from '@mui/material/styles';
import PersonIcon from '@mui/icons-material/Person';
import DescriptionIcon from '@mui/icons-material/Description';
import AttachMoneyIcon from '@mui/icons-material/AttachMoney';
import { Avatar } from '@mui/material';
import * as React from 'react';

export default function GetStepIcon(index, isCompleted, isActive, isValid) {
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

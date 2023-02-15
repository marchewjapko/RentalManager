import React from 'react';
import {
	Divider,
	ListItem,
	ListItemButton,
	ListItemIcon,
	ListItemText,
} from '@mui/material';
import { useRecoilState } from 'recoil';
import { isHeaderOpenAtom } from '../Atoms/HeaderAtoms';
import { useNavigate } from 'react-router-dom';
import HomeIcon from '@mui/icons-material/Home';
import DescriptionIcon from '@mui/icons-material/Description';
import EngineeringIcon from '@mui/icons-material/Engineering';
import ConstructionIcon from '@mui/icons-material/Construction';
import PostAddIcon from '@mui/icons-material/PostAdd';

function GetListItem({ itemKey, url, label, icon }) {
	const navigate = useNavigate();
	const [_, setIsDrawerOpen] = useRecoilState(isHeaderOpenAtom);
	const handleNavigate = (url) => {
		setIsDrawerOpen(false);
		navigate(url);
	};
	return (
		<ListItem key={itemKey} disablePadding>
			<ListItemButton onClick={() => handleNavigate(url)}>
				<ListItemIcon>{icon}</ListItemIcon>
				<ListItemText primary={label} />
			</ListItemButton>
		</ListItem>
	);
}

const HeaderItems = [
	{
		itemKey: 'dashboard',
		url: '/',
		label: 'Dashboard',
		icon: <HomeIcon />,
	},
	{
		itemKey: 'rental-agreements',
		url: '/rental-agreement',
		label: 'Agreements',
		icon: <DescriptionIcon />,
	},
	{
		itemKey: 'add-rental-agreement',
		url: '/add-rental-agreement',
		label: 'Add agreement',
		icon: <PostAddIcon />,
	},
	{
		itemKey: 'equipment',
		url: '/rental-equipment',
		label: 'Equipment',
		icon: <ConstructionIcon />,
	},
	{
		itemKey: 'employees',
		url: '/employees',
		label: 'Employees',
		icon: <EngineeringIcon />,
	},
];

export default function HeaderListItems() {
	return (
		<>
			{HeaderItems.map((item, index) => (
				<div key={index}>
					<GetListItem
						itemKey={item.itemKey}
						url={item.url}
						label={item.label}
						icon={item.icon}
					/>
					<Divider />
				</div>
			))}
		</>
	);
}

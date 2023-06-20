import * as React from 'react';
import {
	Button,
	ButtonGroup,
	ClickAwayListener,
	Grow,
	IconButton,
	InputAdornment,
	MenuItem,
	MenuList,
	Paper,
	Popper,
	Skeleton,
	Stack,
	TextField,
	Typography,
} from '@mui/material';
import AddCircleRoundedIcon from '@mui/icons-material/AddCircleRounded';
import ArrowDropDownIcon from '@mui/icons-material/ArrowDropDown';
import ArrowUpwardIcon from '@mui/icons-material/ArrowUpward';
import SortIcon from '@mui/icons-material/Sort';
import SearchIcon from '@mui/icons-material/Search';
import ClearIcon from '@mui/icons-material/Clear';
import { useTranslation } from 'react-i18next';
import { DefaultValue, useRecoilState, useRecoilValue, useSetRecoilState } from 'recoil';
import RentalAgreementForm from './RentalAgreementForm';
import { useEffect, useState, useRef } from 'react';
import { AnimatePresence, motion } from 'framer-motion';
import {
	forceRentalEquipmentRefresh,
	rentalEquipmentAtom,
	rentalEquipmentShowDeleteConfirmation,
	rentalEquipmentShowEditDialog,
} from '../Atoms/RentaLEquipmentAtoms';
import { filterRentalEquipment } from '../../Actions/RestAPI/RentalEquipmentActions';
import RentalAgreementCard from './RentalAgreementCard';
import './RentalAgreement.css';
import ConstructionIcon from '@mui/icons-material/Construction';
import { getAllAgreements } from '../../Actions/RestAPI/RentalAgreementActions';

function GetSkeletonCards() {
	const skeletonArray = Array(7).fill(0);
	return (
		<>
			{skeletonArray.map((x, index) => (
				<motion.div
					key={index}
					variants={item}
					custom={[index, skeletonArray.length]}
					initial="initial"
					animate="animate"
					exit="exit"
				>
					<Skeleton className={'rental-agreement-card-skeleton'} />
				</motion.div>
			))}
		</>
	);
}

function GetNormalCards({ data, indexOffset }) {
	return (
		<>
			{data.map((rentalAgreement, index) => (
				<motion.div
					key={index + indexOffset + 7}
					variants={item}
					custom={[index, data.length]}
					initial="initial"
					animate="animate"
					exit="exit"
				>
					<RentalAgreementCard rentalAgreement={rentalAgreement} />
				</motion.div>
			))}
		</>
	);
}

function getChildDelay(index, size) {
	if (size <= 31) {
		return index * 0.05;
	} else {
		return index * (1 / (size - 1));
	}
}

const item = {
	exit: {
		opacity: 0,
		transition: {
			duration: 0.2,
		},
	},
	initial: {
		opacity: 0,
	},
	animate: (i) => ({
		opacity: 1,
		transition: {
			duration: 0.2,
			delay: getChildDelay(i[0], i[1]),
		},
	}),
};

const options = ['dateAdded', 'clientSurname'];

function sortData(sortDesc, option, data) {
	let result = data;
	if (sortDesc) {
		result.sort((a, b) => {
			if (option === 'clientSurname') {
				return b.client.surname.toString().localeCompare(a.client.surname);
			}
			if (a[option] < b[option]) {
				return 1;
			}
			return -1;
		});
	} else {
		result.sort((a, b) => {
			if (option === 'clientSurname') {
				return a.client.surname.toString().localeCompare(b.client.surname);
			}
			if (a[option] > b[option]) {
				return 1;
			}
			return -1;
		});
	}
	return result;
}

export default function RentalAgreement() {
	const [data, setData] = useState([]);
	const [isLoading, setIsLoading] = useState(true);
	const refreshFunction = useRecoilValue(forceRentalEquipmentRefresh);
	const [showEditDialog, setShowEditDialog] = useRecoilState(rentalEquipmentShowEditDialog);
	const showDeleteDialog = useRecoilValue(rentalEquipmentShowDeleteConfirmation);
	const setRentalEquipment = useSetRecoilState(rentalEquipmentAtom);
	const [sortOpen, setSortOpen] = React.useState(false);
	const [selectedIndex, setSelectedIndex] = React.useState(0);
	const [sortDesc, setSortDesc] = React.useState(true);
	const [searchName, setSearchName] = React.useState('');
	const { t } = useTranslation(['generalTranslation', 'equipmentTranslation']);
	const anchorRef = useRef(null);

	const indexOffset = useRef(0);

	useEffect(() => {
		setIsLoading(true);
		getAllAgreements()
			.then((result) => {
				if (result.hasOwnProperty('data')) {
					setData(sortData(sortDesc, options[selectedIndex], result.data));
				} else {
					setData(sortData(sortDesc, options[selectedIndex], result));
				}
				setIsLoading(false);
			})
			.catch((error) => {
				console.log(error);
			});
	}, [refreshFunction]);

	const handleAddClick = () => {
		setRentalEquipment(new DefaultValue());
		setShowEditDialog(true);
	};

	const handleSortButtonClick = () => {
		setSortDesc(!sortDesc);
		handleSortChange(selectedIndex, !sortDesc);
	};

	const handleMenuItemClick = (event, index) => {
		setSelectedIndex(index);
		setSortOpen(false);
		handleSortChange(index, sortDesc);
	};

	function handleSortChange(index, sortDesc) {
		let dataToSort = data;
		setData([]);
		if (indexOffset.current === 0) {
			indexOffset.current = dataToSort.length;
		} else {
			indexOffset.current = 0;
		}
		setData(sortData(sortDesc, options[index], dataToSort));
	}

	const handleSearch = () => {
		setIsLoading(true);
		filterRentalEquipment(searchName)
			.then((result) => {
				if (result.hasOwnProperty('data')) {
					setData(sortData(sortDesc, options[selectedIndex], result.data));
				} else {
					setData(sortData(sortDesc, options[selectedIndex], result));
				}
				setIsLoading(false);
			})
			.catch((error) => {
				console.log(error);
			});
	};

	return (
		<>
			{showDeleteDialog && <DeleteRentalEquipmentDialogOLD />}
			{showEditDialog && <RentalAgreementForm />}
			<div className={'rental-agreement-container'}>
				<Stack direction={'row'} alignItems={'center'} gap={'10px'}>
					<ConstructionIcon sx={{ height: '3rem', width: '3rem' }} />
					<Typography variant={'h3'}>
						{t('equipment', { ns: 'equipmentTranslation' })}
					</Typography>
				</Stack>
				<div className={'rental-agreement-actions-container'}>
					<Button
						startIcon={<AddCircleRoundedIcon />}
						variant={'outlined'}
						onClick={handleAddClick}
						disabled={isLoading}
					>
						{t('add')}
					</Button>
					<ButtonGroup ref={anchorRef} disabled={isLoading}>
						<Button
							onClick={handleSortButtonClick}
							variant={'outlined'}
							startIcon={<SortIcon />}
							endIcon={
								<ArrowUpwardIcon
									sx={{
										transform: sortDesc ? 'rotate(180deg)' : 'rotate(0deg)',
										transition: 'all 0.2s ease-out',
									}}
								/>
							}
						>
							{t(options[selectedIndex], { ns: 'equipmentTranslation' })}
						</Button>
						<Button size="small" variant={'outlined'} onClick={() => setSortOpen(true)}>
							<ArrowDropDownIcon />
						</Button>
						<Popper
							sx={{
								zIndex: 2,
							}}
							open={sortOpen}
							anchorEl={anchorRef.current}
							role={undefined}
							transition
							disablePortal
							placement={'bottom-end'}
						>
							{({ TransitionProps, placement }) => (
								<Grow
									{...TransitionProps}
									style={{
										transformOrigin:
											placement === 'bottom' ? 'center top' : 'center bottom',
									}}
								>
									<Paper
										sx={{
											borderColor: 'rgba(144, 202, 249, 0.5) !important',
											border: '1px solid',
											borderRadius: '10px',
											marginTop: '5px',
										}}
									>
										<ClickAwayListener onClickAway={() => setSortOpen(false)}>
											<MenuList autoFocusItem sx={{ padding: 0 }}>
												{options.map((option, index) => (
													<MenuItem
														key={option}
														selected={index === selectedIndex}
														onClick={(event) =>
															handleMenuItemClick(event, index)
														}
													>
														{t(option, { ns: 'equipmentTranslation' })}
													</MenuItem>
												))}
											</MenuList>
										</ClickAwayListener>
									</Paper>
								</Grow>
							)}
						</Popper>
					</ButtonGroup>
					<TextField
						size={'small'}
						label={t('name', { ns: 'equipmentTranslation' })}
						variant="outlined"
						onChange={(event) => setSearchName(event.target.value)}
						value={searchName}
						disabled={isLoading}
						InputProps={{
							endAdornment: (
								<InputAdornment position="end">
									{searchName.length > 0 && (
										<IconButton
											edge="end"
											onClick={() => setSearchName('')}
											disabled={isLoading}
										>
											<ClearIcon />
										</IconButton>
									)}
									<IconButton
										edge="end"
										onClick={handleSearch}
										disabled={isLoading}
									>
										<SearchIcon />
									</IconButton>
								</InputAdornment>
							),
						}}
					/>
				</div>
				<div className="rental-agreement-card-container">
					<AnimatePresence mode="wait">
						{isLoading || data.length === 0 ? (
							<GetSkeletonCards key={1} />
						) : (
							<GetNormalCards data={data} key={2} indexOffset={indexOffset.current} />
						)}
					</AnimatePresence>
				</div>
			</div>
		</>
	);
}
import * as React from 'react';
import {
	Button,
	ButtonGroup,
	ClickAwayListener,
	Grow,
	MenuItem,
	MenuList,
	Paper,
	Popper,
	Skeleton,
	Stack,
	Typography,
} from '@mui/material';
import AddCircleRoundedIcon from '@mui/icons-material/AddCircleRounded';
import ArrowDropDownIcon from '@mui/icons-material/ArrowDropDown';
import ArrowUpwardIcon from '@mui/icons-material/ArrowUpward';
import SortIcon from '@mui/icons-material/Sort';
import { useTranslation } from 'react-i18next';
import DeleteRentalEquipmentDialog from './DeleteRentalEquipmentDialog';
import { DefaultValue, useRecoilState, useRecoilValue, useSetRecoilState } from 'recoil';
import RentalEquipmentForm from './RentalEquipmentForm';
import { useEffect, useState, useRef } from 'react';
import { AnimatePresence, motion } from 'framer-motion';
import {
	forceRentalEquipmentRefresh,
	rentalEquipmentAtom,
	rentalEquipmentShowDeleteConfirmation,
	rentalEquipmentShowEditDialog,
} from '../Atoms/RentaLEquipmentAtoms';
import { getAllRentalEquipment } from '../../Actions/RestAPI/RentalEquipmentActions';
import RentalEquipmentCard from './RentalEquipmentCard';
import './RentalEquipment.css';
import ConstructionIcon from '@mui/icons-material/Construction';

function GetSkeletonCards() {
	const skeletonArray = Array(5).fill(0);
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
					<Skeleton className={'rental-equipment-card-skeleton'} />
				</motion.div>
			))}
		</>
	);
}

function GetNormalCards({ data, indexOffset }) {
	return (
		<>
			{data.map((rentalEquipment, index) => (
				<motion.div
					key={index + indexOffset}
					variants={item}
					custom={[index, data.length]}
					initial="initial"
					animate="animate"
					exit="exit"
				>
					<RentalEquipmentCard rentalEquipment={rentalEquipment} />
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

const options = ['name', 'dateAdded', 'price'];

function sortData(sortDesc, option, data) {
	let result = data;
	if (sortDesc) {
		result.sort((a, b) => {
			if (option === 'name') {
				return a['name'].toString().localeCompare(b['name']);
			}
			if (a[option] > b[option]) {
				return 1;
			}
			return -1;
		});
	} else {
		result.sort((a, b) => {
			if (option === 'name') {
				return b['name'].toString().localeCompare(a['name']);
			}
			if (a[option] < b[option]) {
				return 1;
			}
			return -1;
		});
	}
	return result;
}

export default function RentalEquipment() {
	const [data, setData] = useState([]);
	const [isLoading, setIsLoading] = useState(true);
	const refreshFunction = useRecoilValue(forceRentalEquipmentRefresh);
	const [showEditDialog, setShowEditDialog] = useRecoilState(rentalEquipmentShowEditDialog);
	const showDeleteDialog = useRecoilValue(rentalEquipmentShowDeleteConfirmation);
	const setRentalEquipment = useSetRecoilState(rentalEquipmentAtom);
	const [sortOpen, setSortOpen] = React.useState(false);
	const [selectedIndex, setSelectedIndex] = React.useState(0);
	const [sortDesc, setSortDesc] = React.useState(true);
	const { t } = useTranslation(['generalTranslation', 'equipmentTranslation']);
	const anchorRef = useRef(null);

	const indexOffset = useRef(0);

	useEffect(() => {
		setIsLoading(true);
		getAllRentalEquipment()
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

	return (
		<>
			{showDeleteDialog && <DeleteRentalEquipmentDialog />}
			{showEditDialog && <RentalEquipmentForm />}
			<div className={'rental-equipment-container'}>
				<Stack direction={'row'} alignItems={'center'} gap={'10px'}>
					<ConstructionIcon sx={{ height: '3rem', width: '3rem' }} />
					<Typography variant={'h3'}>
						{t('equipment', { ns: 'equipmentTranslation' })}
					</Typography>
				</Stack>
				<div className={'rental-equipment-actions-container'}>
					<Button
						startIcon={<AddCircleRoundedIcon />}
						variant={'outlined'}
						onClick={handleAddClick}
						disabled={isLoading}
					>
						{t('add')}
					</Button>
					<ButtonGroup
						variant="contained"
						ref={anchorRef}
						sx={{
							borderColor: isLoading
								? 'rgba(255, 255, 255, 0.12) !important'
								: 'rgba(144, 202, 249, 0.5) !important',
						}}
						disabled={isLoading}
					>
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
							sx={{
								borderColor: isLoading
									? 'rgba(255, 255, 255, 0.12) !important'
									: 'rgba(144, 202, 249, 0.5) !important',
							}}
						>
							{t(options[selectedIndex], { ns: 'equipmentTranslation' })}
						</Button>
						<Button
							size="small"
							variant={'outlined'}
							onClick={() => setSortOpen(true)}
							sx={{ borderColor: 'rgba(144, 202, 249, 0.5)' }}
						>
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
				</div>
				<div className="rental-equipment-card-container">
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

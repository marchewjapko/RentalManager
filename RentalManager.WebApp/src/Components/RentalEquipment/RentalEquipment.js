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
	Typography,
} from '@mui/material';
import AddCircleRoundedIcon from '@mui/icons-material/AddCircleRounded';
import ArrowDropDownIcon from '@mui/icons-material/ArrowDropDown';
import ArrowDownwardIcon from '@mui/icons-material/ArrowDownward';
import { useTranslation } from 'react-i18next';
import DeleteRentalEquipmentDialog from './DeleteRentalEquipmentDialog';
import { DefaultValue, useRecoilState, useRecoilValue, useSetRecoilState } from 'recoil';
import RentalEquipmentForm from './RentalEquipmentForm';
import { useEffect, useState } from 'react';
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

function GetNormalCards({ data }) {
	return (
		<>
			{data.map((rentalEquipment, index) => (
				<motion.div
					key={index}
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
		return index * (1.5 / (size - 1));
	}
}

const item = {
	exit: { opacity: 0 },
	initial: {
		opacity: 0,
	},
	animate: (i) => ({
		opacity: 1,
		transition: {
			delay: getChildDelay(i[0], i[1]),
		},
	}),
};

const options = ['name', 'dateAdded', 'price'];

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
	const anchorRef = React.useRef(null);

	useEffect(() => {
		setIsLoading(true);
		getAllRentalEquipment()
			.then((result) => {
				if (result.hasOwnProperty('data')) {
					sortData(selectedIndex, sortDesc, result.data)
					// setData(result.data);
				} else {
					sortData(selectedIndex, sortDesc, result)
					// setData(result);
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
		setSortDesc(!sortDesc)
		sortData(selectedIndex, !sortDesc, data)
	}

	const handleMenuItemClick = (event, index) => {
		setSelectedIndex(index);
		setSortOpen(false);
		sortData(index, sortDesc, data)
	};

	function sortData(index, shouldSortDesc, dataToSort) {
		setData([])
		setTimeout(() => {
			if(shouldSortDesc) {
				dataToSort.sort((a, b) => {
					if(a[options[index]] < b[options[index]]) {
						return 1
					}
					return -1
				})
			} else {
				dataToSort.sort((a, b) => {
					if(a[options[index]] > b[options[index]]) {
						return 1
					}
					return -1
				})
			}
			setData(dataToSort)
		}, 100)
	}

	return (
		<>
			{showDeleteDialog && <DeleteRentalEquipmentDialog />}
			{showEditDialog && <RentalEquipmentForm />}
			<div className={'rental-equipment-container'}>
				<Typography variant={'h3'}>
					{t('equipment', { ns: 'equipmentTranslation' })}
				</Typography>
				<div className={'rental-equipment-actions-container'}>
					<Button
						startIcon={<AddCircleRoundedIcon />}
						variant={'contained'}
						onClick={handleAddClick}
						disabled={isLoading}
					>
						{t('add')}
					</Button>
					<ButtonGroup variant="contained" ref={anchorRef} aria-label="split button">
						<Button
							onClick={handleSortButtonClick}
							endIcon={<ArrowDownwardIcon sx={{ transform: sortDesc ? 'rotate(0deg)' : 'rotate(180deg)', transition: 'all 0.2s ease-out' }} />}
						>
							{t('sortBy', { ns: 'equipmentTranslation' })} {t(options[selectedIndex], { ns: 'equipmentTranslation' })}
						</Button>
						<Button
							size="small"
							aria-controls={sortOpen ? 'split-button-menu' : undefined}
							aria-expanded={sortOpen ? 'true' : undefined}
							aria-label="select merge strategy"
							aria-haspopup="menu"
							onClick={() => setSortOpen(true)}
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
						>
							{({ TransitionProps, placement }) => (
								<Grow
									{...TransitionProps}
									style={{
										transformOrigin:
											placement === 'bottom' ? 'center top' : 'center bottom',
									}}
								>
									<Paper>
										<ClickAwayListener onClickAway={() => setSortOpen(false)}>
											<MenuList autoFocusItem>
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
						{isLoading ? (
							<GetSkeletonCards key={1} />
						) : (
							<GetNormalCards data={data} key={2} />
						)}
					</AnimatePresence>
				</div>
			</div>
		</>
	);
}

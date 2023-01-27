import {
	Box,
	Button,
	Checkbox,
	FormControlLabel,
	IconButton,
	InputAdornment,
	Popover,
	Stack,
	TextField,
} from '@mui/material';
import * as React from 'react';
import CloseIcon from '@mui/icons-material/Close';
import SearchIcon from '@mui/icons-material/Search';
import InputMask from 'react-input-mask';

export default function RentalAgreementSearchSelect({
	isLoading,
	handleSearch,
	searchValues,
	setSearchValues,
}) {
	const [anchorEl, setAnchorEl] = React.useState(null);

	const handleKeyDown = (event) => {
		if (event.key === 'Enter') {
			setAnchorEl(null);
			handleSearch(searchValues);
		}
	};

	const handleSearchChange = (event) => {
		if (event.target.type === 'checkbox') {
			setSearchValues({
				...searchValues,
				[event.target.name]: event.target.checked,
			});
		} else {
			setSearchValues({
				...searchValues,
				[event.target.name]: event.target.value,
			});
		}
	};

	const handleClick = (event) => {
		setAnchorEl(event.currentTarget);
	};

	const handleClose = () => {
		setAnchorEl(null);
	};

	const open = Boolean(anchorEl);

	return (
		<div>
			<Button
				variant="outlined"
				color={'inherit'}
				endIcon={<SearchIcon />}
				onClick={handleClick}
				disabled={isLoading}
				sx={{ height: '40px' }}
			>
				Search
			</Button>
			<Popover
				open={open}
				anchorEl={anchorEl}
				onClose={handleClose}
				anchorOrigin={{
					vertical: 'bottom',
					horizontal: 'center',
				}}
				transformOrigin={{
					vertical: 'top',
					horizontal: 'center',
				}}
			>
				<Stack
					direction={'column'}
					sx={{
						paddingTop: '10px',
						paddingBottom: '10px',
						paddingLeft: '15px',
						paddingRight: '15px',
						width: '14.5rem',
					}}
					spacing={2}
				>
					<TextField
						margin="dense"
						name={'surname'}
						label="Surname"
						variant="outlined"
						fullWidth
						value={searchValues.surname}
						onChange={handleSearchChange}
						onKeyDown={handleKeyDown}
						size="small"
						InputProps={{
							endAdornment: (
								<InputAdornment position="end">
									{searchValues.surname.trim().length !==
									0 ? (
										<IconButton
											color="default"
											onClick={() =>
												setSearchValues({
													...searchValues,
													surname: '',
												})
											}
											sx={{ marginRight: '-4px' }}
											disabled={isLoading}
										>
											<CloseIcon />
										</IconButton>
									) : (
										<Box
											component="span"
											className={'ClientSearchBox'}
										/>
									)}
								</InputAdornment>
							),
						}}
					/>
					<InputMask
						mask="999 999 999"
						value={searchValues.phone}
						disabled={false}
						maskChar=" "
						onChange={handleSearchChange}
						onKeyDown={handleKeyDown}
					>
						{() => (
							<TextField
								margin="dense"
								name={'phone'}
								label="Phone"
								variant="outlined"
								fullWidth
								size="small"
								InputProps={{
									endAdornment: (
										<InputAdornment position="end">
											{searchValues.phone.trim()
												.length !== 0 ? (
												<IconButton
													color="default"
													onClick={() =>
														setSearchValues({
															...searchValues,
															phone: '',
														})
													}
													sx={{ marginRight: '-4px' }}
													disabled={isLoading}
												>
													<CloseIcon />
												</IconButton>
											) : (
												<Box
													component="span"
													className={
														'ClientSearchBox'
													}
												/>
											)}
										</InputAdornment>
									),
									startAdornment: (
										<InputAdornment position="start">
											+48
										</InputAdornment>
									),
								}}
							/>
						)}
					</InputMask>
					<TextField
						margin="dense"
						name={'city'}
						label="City"
						variant="outlined"
						fullWidth
						value={searchValues.city}
						onChange={handleSearchChange}
						onKeyDown={handleKeyDown}
						size="small"
						InputProps={{
							endAdornment: (
								<InputAdornment position="end">
									{searchValues.city.trim().length !== 0 ? (
										<IconButton
											color="default"
											onClick={() =>
												setSearchValues({
													...searchValues,
													city: '',
												})
											}
											sx={{ marginRight: '-4px' }}
											disabled={isLoading}
										>
											<CloseIcon />
										</IconButton>
									) : (
										<Box
											component="span"
											className={'ClientSearchBox'}
										/>
									)}
								</InputAdornment>
							),
						}}
					/>
					<TextField
						margin="dense"
						name={'street'}
						label="Street"
						variant="outlined"
						fullWidth
						value={searchValues.street}
						onChange={handleSearchChange}
						onKeyDown={handleKeyDown}
						size="small"
						InputProps={{
							endAdornment: (
								<InputAdornment position="end">
									{searchValues.street.trim().length !== 0 ? (
										<IconButton
											color="default"
											onClick={() =>
												setSearchValues({
													...searchValues,
													street: '',
												})
											}
											sx={{ marginRight: '-4px' }}
											disabled={isLoading}
										>
											<CloseIcon />
										</IconButton>
									) : (
										<Box
											component="span"
											className={'ClientSearchBox'}
										/>
									)}
								</InputAdornment>
							),
						}}
					/>
					<FormControlLabel
						label="Only active"
						name={'onlyActive'}
						control={
							<Checkbox
								checked={searchValues.onlyActive}
								onChange={handleSearchChange}
								sx={{
									paddingLeft: '0px',
									paddingTop: '4px',
									paddingBottom: '4px',
								}}
							/>
						}
					/>
					<FormControlLabel
						label="Only unpaid"
						name={'onlyUnpaid'}
						control={
							<Checkbox
								checked={searchValues.onlyUnpaid}
								onChange={handleSearchChange}
								sx={{
									paddingLeft: '0px',
									paddingTop: '4px',
									paddingBottom: '4px',
								}}
							/>
						}
					/>
					<Button
						variant="contained"
						color={'primary'}
						sx={{ height: '2.7em' }}
						endIcon={<SearchIcon />}
						onClick={function () {
							handleSearch(searchValues);
							setAnchorEl(null);
						}}
					>
						Search
					</Button>
				</Stack>
			</Popover>
		</div>
	);
}

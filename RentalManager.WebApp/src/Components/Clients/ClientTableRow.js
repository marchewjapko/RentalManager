import * as React from 'react';
import {
	Box,
	Checkbox,
	Collapse,
	IconButton,
	InputAdornment,
	Stack,
	TableCell,
	TableRow,
	TextField,
} from '@mui/material';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import dayjs from 'dayjs';
import 'dayjs/locale/pl';

export default function ClientTableRow({
	row,
	handleEditClick,
	handleDeleteClick,
	setClient,
	client,
}) {
	const [openDetails, setOpenDetails] = React.useState(false);
	const handleOpenDetails = () => {
		setOpenDetails(!openDetails);
	};
	const getDate = (date) => {
		let formatData = dayjs(date)
			.format('dddd, DD MMMM YYYY')
			.replace(/(^|\s)\S/g, (l) => l.toUpperCase());
		return formatData;
	};

	return (
		<React.Fragment>
			<TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
				<TableCell>
					<Stack direction={'row'}>
						<Checkbox
							onChange={() => setClient(row)}
							checked={(client && row.id === client.id) || false}
						/>
						<IconButton
							size="small"
							onClick={handleOpenDetails}
							sx={{
								width: '30px',
								height: '30px',
								marginTop: 'auto',
								marginBottom: 'auto',
							}}
						>
							{openDetails ? (
								<KeyboardArrowUpIcon fontSize="small" />
							) : (
								<KeyboardArrowDownIcon fontSize="small" />
							)}
						</IconButton>
					</Stack>
				</TableCell>
				<TableCell
					component="th"
					scope="row"
					className={'ClientTableNameCell'}
				>
					{row.name + ' ' + row.surname}
				</TableCell>
				<TableCell align="right">
					<Box>
						<IconButton
							aria-label="delete"
							size="small"
							onClick={() => handleEditClick(row)}
						>
							<EditIcon fontSize="small" />
						</IconButton>
						<IconButton
							aria-label="delete"
							size="small"
							color={'error'}
							onClick={() => handleDeleteClick(row)}
						>
							<DeleteIcon fontSize="small" />
						</IconButton>
					</Box>
				</TableCell>
			</TableRow>
			<TableRow>
				<TableCell
					style={{ paddingBottom: 0, paddingTop: 0 }}
					colSpan={4}
				>
					<Collapse in={openDetails} timeout="auto" unmountOnExit>
						<Box sx={{ margin: 1 }}>
							<TextField
								margin="dense"
								label="Phone number"
								fullWidth
								variant="outlined"
								value={row.phoneNumber}
								InputProps={{
									readOnly: true,
									startAdornment: (
										<InputAdornment position="start">
											+48
										</InputAdornment>
									),
								}}
							/>
							<TextField
								margin="dense"
								label="Email"
								fullWidth
								variant="outlined"
								value={row.email}
								InputProps={{
									readOnly: true,
								}}
								disabled={
									row.email === null ||
									row.email.trim().length === 0
								}
							/>
							<TextField
								margin="dense"
								label="ID card"
								fullWidth
								variant="outlined"
								value={row.idCard}
								InputProps={{
									readOnly: true,
								}}
								disabled={
									row.idCard === null ||
									row.idCard.trim().length === 0
								}
							/>
							<TextField
								margin="dense"
								label="Address"
								fullWidth
								variant="outlined"
								value={row.city + ' ' + row.street}
								InputProps={{
									readOnly: true,
								}}
							/>
							<TextField
								margin="dense"
								label="Date added"
								fullWidth
								variant="outlined"
								value={getDate(row.dateAdded)}
								InputProps={{
									readOnly: true,
								}}
							/>
						</Box>
					</Collapse>
				</TableCell>
			</TableRow>
		</React.Fragment>
	);
}

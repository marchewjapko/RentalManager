import {
	Autocomplete,
	CircularProgress,
	Stack,
	TextField,
	Typography,
} from '@mui/material';
import EngineeringIcon from '@mui/icons-material/Engineering';
import * as React from 'react';
import { getAllEmployees } from '../../Actions/RestAPI/EmployeeActions';

export default function EmployeesSelect({ agreement, setAgreement }) {
	const [data, setData] = React.useState([]);
	const [open, setOpen] = React.useState(false);
	const [isLoading, setIsLoading] = React.useState(true);
	React.useEffect(() => {
		const getData = async () => {
			const result = await getAllEmployees();
			setData(result.data);
			setIsLoading(false);
		};
		getData();
	}, []);

	const handleChange = (event, newValue) => {
		setAgreement({
			...agreement,
			employee: newValue,
		});
	};

	return (
		<Stack spacing={2}>
			<Stack
				direction={'row'}
				className={'ClientUpdateDialogStack'}
				sx={{ paddingLeft: '17px', paddingTop: '20px' }}
			>
				<EngineeringIcon className={'DividerIcon'} />
				<Typography variant="h6" className={'MarginTopBottomAuto'}>
					Employee
				</Typography>
			</Stack>
			<Autocomplete
				value={agreement.employee}
				isOptionEqualToValue={(option, value) => option.id === value.id}
				onChange={(event, newValue) => handleChange(event, newValue)}
				open={open}
				onOpen={() => setOpen(true)}
				onClose={() => setOpen(false)}
				options={data}
				getOptionLabel={(option) => option.name + ' ' + option.surname}
				loading={isLoading}
				fullWidth={true}
				blurOnSelect={'touch'}
				renderInput={(params) => (
					<TextField
						{...params}
						label="Select employee"
						InputProps={{
							...params.InputProps,
							endAdornment: (
								<React.Fragment>
									{isLoading ? (
										<CircularProgress
											color="inherit"
											size={20}
										/>
									) : null}
									{params.InputProps.endAdornment}
								</React.Fragment>
							),
						}}
					/>
				)}
			/>
		</Stack>
	);
}

import { Autocomplete, CircularProgress, Stack, TextField, Typography } from '@mui/material';
import ConstructionIcon from '@mui/icons-material/Construction';
import * as React from 'react';
import { getAllRentalEquipment } from '../../Actions/RestAPI/RentalEquipmentActions';
import { useTranslation } from 'react-i18next';

export default function RentalEquipmentSelect({ agreement, setAgreement }) {
	const [data, setData] = React.useState([]);
	const [open, setOpen] = React.useState(false);
	const [isLoading, setIsLoading] = React.useState(true);
	const { t } = useTranslation(['equipmentTranslation']);

	React.useEffect(() => {
		const getData = async () => {
			const result = await getAllRentalEquipment();
			setData(result.hasOwnProperty('data') ? result.data : result);
			setIsLoading(false);
		};
		getData();
	}, []);

	const handleChange = (event, newValue) => {
		setAgreement({
			...agreement,
			rentalEquipment: newValue,
		});
	};

	return (
		<Stack spacing={2}>
			<Stack
				direction={'row'}
				className={'ClientUpdateDialogStack'}
				sx={{ paddingLeft: '17px', paddingTop: '20px' }}
			>
				<ConstructionIcon className={'DividerIcon'} />
				<Typography variant="h6" className={'MarginTopBottomAuto'}>
					{t('equipment')}
				</Typography>
			</Stack>
			<Autocomplete
				value={agreement.rentalEquipment}
				isOptionEqualToValue={(option, value) => option.id === value.id}
				onChange={(event, newValue) => handleChange(event, newValue)}
				multiple
				limitTags={2}
				open={open}
				onOpen={() => setOpen(true)}
				onClose={() => setOpen(false)}
				options={data}
				disableCloseOnSelect={true}
				getOptionLabel={(option) => option.name + ' (' + option.price + ' zł)'}
				loading={isLoading}
				fullWidth={true}
				renderInput={(params) => (
					<TextField
						{...params}
						label={t('selectEquipment')}
						InputProps={{
							...params.InputProps,
							endAdornment: (
								<React.Fragment>
									{isLoading ? (
										<CircularProgress color="inherit" size={20} />
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

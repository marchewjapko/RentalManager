import { InputAdornment, TextField } from '@mui/material';
import * as React from 'react';
import { useState } from 'react';
import InputMask from 'react-input-mask';

export default function ValidatedTextField({
	name,
	label,
	value,
	onChange,
	validationFunction,
	isReadonly,
	isRequired,
	mask,
}) {
	const [isError, setIsError] = useState(false);
	function getHelperText() {
		if (validationFunction(value) === 'invalidFormat' && isError) {
			return 'Invalid format';
		}
		if (isRequired) {
			return 'Required';
		}
	}
	const handleOnBlur = () => {
		if (validationFunction(value) !== '') {
			setIsError(true);
		} else {
			setIsError(false);
		}
	};
	if (mask) {
		return (
			<InputMask
				mask={mask}
				value={value}
				maskChar={''}
				onChange={onChange}
				onBlur={handleOnBlur}
			>
				{() => (
					<TextField
						name={name}
						margin="dense"
						label={label}
						variant="outlined"
						fullWidth
						error={isError}
						helperText={getHelperText()}
						InputProps={{
							readOnly: isReadonly,
							startAdornment:
								name === 'phoneNumber' ? (
									<InputAdornment position="start">+48</InputAdornment>
								) : null,
						}}
					/>
				)}
			</InputMask>
		);
	}
	return (
		<TextField
			name={name}
			margin="dense"
			label={label}
			variant="outlined"
			fullWidth
			value={value}
			onChange={onChange}
			onBlur={handleOnBlur}
			error={isError}
			helperText={getHelperText()}
			InputProps={{
				readOnly: isReadonly,
				startAdornment:
					name === 'phoneNumber' ? (
						<InputAdornment position="start">+48</InputAdornment>
					) : null,
			}}
		/>
	);
}

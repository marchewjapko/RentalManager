import { Box, Divider, IconButton, InputAdornment, Stack, TextField } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import SearchIcon from '@mui/icons-material/Search';
import * as React from 'react';
import { useTranslation } from 'react-i18next';

export default function SearchTextField({ isLoading, handleSearch }) {
	const [searchName, setSearchName] = React.useState('');
	const { t } = useTranslation(['generalTranslation']);
	const handleChangeName = (event) => {
		setSearchName(event.target.value);
	};
	const handleKeyDown = (event) => {
		if (event.key === 'Enter') {
			handleSearch(searchName);
		}
	};
	const clearSearch = () => {
		setSearchName('');
		handleSearch('');
	};
	return (
		<TextField
			value={searchName}
			onChange={handleChangeName}
			placeholder={t('search')}
			variant="outlined"
			size="small"
			className={'TableSearchInput'}
			disabled={isLoading}
			onKeyDown={handleKeyDown}
			InputProps={{
				endAdornment: (
					<InputAdornment position="end">
						<Stack direction="row">
							{searchName.trim().length !== 0 ? (
								<IconButton
									color="default"
									onClick={clearSearch}
									sx={{ marginRight: '-4px' }}
									disabled={isLoading}
								>
									<CloseIcon />
								</IconButton>
							) : (
								<Box
									component="span"
									sx={{ width: '40px', height: '40px', marginRight: '-4px' }}
								/>
							)}
							<Divider sx={{ height: 28, m: 0.5 }} orientation="vertical" />
							<IconButton
								color="default"
								onClick={() => handleSearch(searchName)}
								sx={{ marginRight: '-12px', marginLeft: '-4px' }}
								disabled={isLoading}
							>
								<SearchIcon />
							</IconButton>
						</Stack>
					</InputAdornment>
				),
			}}
		/>
	);
}

import {
	Checkbox,
	IconButton,
	Skeleton,
	Table,
	TableBody,
	TableCell,
	TableContainer,
	TableRow,
} from '@mui/material';
import * as React from 'react';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import SkeletonTablePagination from '../SkeletonTablePagination';
import { useTheme } from '@mui/material/styles';
import useMediaQuery from '@mui/material/useMediaQuery';
import { Scrollbars } from 'react-custom-scrollbars-2';

export default function SkeletonTableClients() {
	const theme = useTheme();
	const isSmallScreen = useMediaQuery(theme.breakpoints.down('sm'));
	return (
		<div>
			<TableContainer className={'ClientsTable'}>
				<Scrollbars
					autoHide
					autoHeight={true}
					autoHeightMin={0}
					autoHeightMax={isSmallScreen ? '30vh' : '40vh'}
					autoHideTimeout={750}
					autoHideDuration={500}
				></Scrollbars>
				<Table
					size={isSmallScreen ? 'small' : 'medium'}
					padding={isSmallScreen ? 'none' : 'normal'}
				>
					<TableBody>
						{[...Array(5).keys()].map((x) => (
							<TableRow key={x}>
								<TableCell sx={{ whiteSpace: 'nowrap' }}>
									<Checkbox checked={false} disabled />
									<IconButton size="small" disabled>
										<KeyboardArrowDownIcon fontSize="small" />
									</IconButton>
								</TableCell>
								<TableCell
									className={
										'SkeletonTableRow ClientTableNameCell'
									}
									sx={{ width: '49%' }}
								>
									<Skeleton
										variant="rounded"
										width={'100%'}
										height={35}
									/>
								</TableCell>
								<TableCell
									className={'SkeletonTableRow'}
									sx={{ width: '49%' }}
								>
									<Skeleton
										variant="rounded"
										width={'100%'}
										height={35}
									/>
								</TableCell>
								<TableCell
									align="right"
									sx={{
										width: 'min-content',
										whiteSpace: 'nowrap',
									}}
								>
									<IconButton
										aria-label="delete"
										size="small"
										disabled
									>
										<EditIcon fontSize="small" />
									</IconButton>
									<IconButton
										aria-label="delete"
										size="small"
										disabled
									>
										<DeleteIcon fontSize="small" />
									</IconButton>
								</TableCell>
							</TableRow>
						))}
					</TableBody>
				</Table>
			</TableContainer>
			<SkeletonTablePagination />
		</div>
	);
}

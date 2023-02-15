import {
	Box,
	IconButton,
	Skeleton,
	Stack,
	Table,
	TableBody,
	TableCell,
	TableContainer,
	TableHead,
	TableRow,
} from '@mui/material';
import * as React from 'react';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import SkeletonTablePagination from '../SkeletonTablePagination';

export default function SkeletonTableRentalEquipment() {
	return (
		<div>
			<TableContainer className={'RentalEquipmentTable'}>
				<Table>
					<TableBody>
						{[...Array(5).keys()].map((x) => (
							<TableRow key={x}>
								<TableCell className={'SkeletonTableRow'}>
									<Skeleton
										variant="rounded"
										width={'100%'}
										height={35}
									/>
								</TableCell>
								<TableCell className={'SkeletonTableRow'}>
									<Skeleton
										variant="rounded"
										width={'100%'}
										height={35}
									/>
								</TableCell>
								<TableCell
									align="right"
									sx={{ width: '100px' }}
								>
									<Box>
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
									</Box>
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

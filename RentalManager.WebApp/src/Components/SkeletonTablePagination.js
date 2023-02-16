import { TablePagination } from '@mui/material';

export default function SkeletonTablePagination() {
	return (
		<TablePagination
			rowsPerPageOptions={[0, { label: '-', value: -1 }]}
			component="div"
			count={0}
			rowsPerPage={-1}
			page={0}
			onPageChange={() => null}
			onRowsPerPageChange={() => null}
			SelectProps={{
				disabled: true,
			}}
		/>
	);
}

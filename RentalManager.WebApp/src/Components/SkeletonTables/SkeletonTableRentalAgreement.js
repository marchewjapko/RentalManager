import {
    Box, Checkbox,
    IconButton,
    Skeleton,
    Stack,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
} from "@mui/material";
import * as React from 'react';
import MoreHorizIcon from '@mui/icons-material/MoreHoriz';
import SkeletonTablePagination from "../SkeletonTablePagination";


export default function SkeletonTableRentalAgreement() {
    return (
        <div>
            <TableContainer className={"EmployeesTable"}>
                <Table stickyHeader>
                    <TableHead>
                        <TableRow>
                            <TableCell className={"TableRentalEquipmentColumnName"}>Client</TableCell>
                            <TableCell align="right" className={"TableRentalEquipmentColumnPrice"}>Date added</TableCell>
                            <TableCell align="right" sx={{width: "10px"}}/>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {[...Array(5).keys()].map((x) =>
                            <TableRow key={x}>
                                <TableCell className={"SkeletonTableRow"}>
                                    <Skeleton variant="rounded" width={'100%'} height={35}/>
                                </TableCell>
                                <TableCell className={"SkeletonTableRow"}>
                                    <Skeleton variant="rounded" width={'100%'} height={35}/>
                                </TableCell>
                                <TableCell align="right">
                                    <Box>
                                        <IconButton color={"inherit"} onClick={() => null} disabled>
                                            <MoreHorizIcon fontSize="inherit" />
                                        </IconButton>
                                    </Box>
                                </TableCell>
                            </TableRow>
                        )}
                    </TableBody>
                </Table>
            </TableContainer>
            <SkeletonTablePagination/>
        </div>
    );
}
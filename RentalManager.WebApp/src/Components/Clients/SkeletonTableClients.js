import {
    Box,
    IconButton,
    Skeleton,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow
} from "@mui/material";
import SearchIcon from "@mui/icons-material/Search";
import * as React from 'react';
import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import SkeletonTablePagination from "../SkeletonTablePagination";

export default function SkeletonTableClients() {
    return (
        <div>
            <TableContainer className={"ClientsTable"}>
                <Table stickyHeader>
                    <TableHead>
                        <TableRow>
                            <TableCell className={"ClientTableHeadDetails"}/>
                            <TableCell className={"ClientTableHeadName"}>Name</TableCell>
                            <TableCell align="right" className={"ClientTableHeadSurname"}>Surname</TableCell>
                            <TableCell align="right" className={"ClientTableHeadActions"}>
                                <IconButton color="default" disabled>
                                    <SearchIcon/>
                                </IconButton>
                            </TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {[...Array(5).keys()].map((x) =>
                            <TableRow key={x}>
                                <TableCell className={"ClientTableDetailsCell"}>
                                    <IconButton size="small" disabled>
                                        <KeyboardArrowDownIcon fontSize="small"/>
                                    </IconButton>
                                </TableCell>
                                <TableCell className={"SkeletonTableRow ClientTableNameCell"}>
                                    <Skeleton variant="rounded" width={'100%'} height={35}/>
                                </TableCell>
                                <TableCell className={"SkeletonTableRow"}>
                                    <Skeleton variant="rounded" width={'100%'} height={35}/>
                                </TableCell>
                                <TableCell align="right" className={"TableRentalEquipmentColumnActions"}>
                                    <Box>
                                        <IconButton aria-label="delete" size="small" disabled>
                                            <EditIcon fontSize="small"/>
                                        </IconButton>
                                        <IconButton aria-label="delete" size="small" disabled>
                                            <DeleteIcon fontSize="small"/>
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
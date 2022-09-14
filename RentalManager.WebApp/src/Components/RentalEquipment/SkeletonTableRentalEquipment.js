import {
    Box,
    IconButton, Skeleton, Stack,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead, TablePagination,
    TableRow, TextField
} from "@mui/material";
import SearchIcon from "@mui/icons-material/Search";
import * as React from 'react';
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import SkeletonTablePagination from "../SkeletonTablePagination";

export default function SkeletonTableRentalEquipment(props) {
    return (
        <div>
            <TableContainer className={"RentalEquipmentTable"}>
                <Table stickyHeader>
                    <TableHead>
                        <TableRow>
                            <TableCell className={"TableRentalEquipmentColumnName"}>Equipment name</TableCell>
                            <TableCell align="right" className={"TableRentalEquipmentColumnPrice"}>Monthly price</TableCell>
                            <TableCell align="right" className={"TableHeadActionsWithSearch"}>
                                <Stack direction="row" justifyContent="flex-end">
                                    <TextField
                                        value={props.searchPrase}
                                        onChange={() => null}
                                        placeholder="Search"
                                        variant="standard"
                                        size="small"
                                        className={"TableSearchInput"}
                                        InputProps={{
                                            style: {fontSize: '1em'},
                                        }}
                                        disabled
                                    />
                                    <IconButton color="default" disabled>
                                        <SearchIcon/>
                                    </IconButton>
                                </Stack>
                            </TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {[...Array(5).keys()].map((x) =>
                            <TableRow key={x}>
                                <TableCell className={"SkeletonTableRow"}>
                                    <Skeleton variant="rounded" width={'100%'} height={35} />
                                </TableCell>
                                <TableCell className={"SkeletonTableRow"}>
                                    <Skeleton variant="rounded" width={'100%'} height={35} />
                                </TableCell>
                                <TableCell align="right" className={"TableRentalEquipmentColumnActions"} >
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
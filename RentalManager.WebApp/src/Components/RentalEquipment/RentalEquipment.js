import * as React from 'react';
import "./RentalEquipment.js.css"
import {RentalEquipmentMock} from "../../Mocks/RentalEquipmentMock";
import {
    Box,
    IconButton,
    Paper,
    Stack,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableFooter,
    TableHead,
    TablePagination,
    TableRow,
    TextField
} from "@mui/material";
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import SearchIcon from '@mui/icons-material/Search';
import RentalEquipmentUpdateDialog from "./RentalEquipmentUpdateDialog";
import RentalEquipmentDeleteDialog from "./RentalEquipmentDeleteDialog";
import SimpleBarReact from "simplebar-react";
import "simplebar/src/simplebar.css";
import TablePaginationActions from "../TablePaginationActions";

export default function RentalEquipment() {
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(5);
    const [showEditDialog, setShowEditDialog] = React.useState(false);
    const [showDeleteDialog, setShowDeleteDialog] = React.useState(false);
    const [equipmentName, setEquipmentName] = React.useState("");
    const [equipmentPrice, setEquipmentPrice] = React.useState(0);
    const [data, setData] = React.useState(RentalEquipmentMock);
    const [searchName, setSearchName] = React.useState('');

    const handleKeyDown = (event) => {
        if (event.key === 'Enter')
            filterData();
    }

    const handleChangeName = (event) => {
        setSearchName(event.target.value);
        if (event.target.value.length === 0) {
            setData(RentalEquipmentMock.filter(x => x.name.includes('')));
        }
    };

    const filterData = () => {
        setData(RentalEquipmentMock.filter(x => x.name.includes(searchName)));
    }

    const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    const handleEditClick = (isShown, equipmentId, equipmentName, equipmentPrice) => {
        setEquipmentName(equipmentName)
        setEquipmentPrice(equipmentPrice)
        setShowEditDialog(isShown)
    }

    const handleDeleteClick = (isShown, equipmentId, equipmentName, equipmentPrice) => {
        setEquipmentName(equipmentName)
        setEquipmentPrice(equipmentPrice)
        setShowDeleteDialog(isShown)
    }

    return (
        <Paper elevation={5} className={"RentalEquipmentContainer"}>
            {showEditDialog ?
                <RentalEquipmentUpdateDialog handleEditClick={handleEditClick} equipmentName={equipmentName}
                                             equipmentPrice={equipmentPrice}/> : null}
            {showDeleteDialog ?
                <RentalEquipmentDeleteDialog handleDeleteClick={handleDeleteClick} equipmentName={equipmentName}
                                             equipmentPrice={equipmentPrice}/> : null}
            <TableContainer className={"RentalEquipmentTable"}>
                <SimpleBarReact className={"RentalEquipmentTable"}>
                    <Table stickyHeader aria-label="custom pagination table">
                        <TableHead>
                            <TableRow>
                                <TableCell className={"TableRentalEquipmentColumnName"}>Equipment name</TableCell>
                                <TableCell align="right" className={"TableRentalEquipmentColumnPrice"}>Monthly
                                    price</TableCell>
                                <TableCell align="right">
                                    <Stack direction="row" justifyContent="flex-end">
                                        <TextField
                                            value={searchName}
                                            onChange={handleChangeName}
                                            placeholder="Search"
                                            variant="standard"
                                            size="small"
                                            className={"TableRentalEquipmentSearchInput"}
                                            InputProps={{
                                                style: {fontSize: '0.875em'},
                                            }}
                                            onKeyDown={handleKeyDown}
                                        />
                                        <IconButton color="default" onClick={() => filterData()}>
                                            <SearchIcon/>
                                        </IconButton>
                                    </Stack>
                                </TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody className={"test"}>
                            {(rowsPerPage > 0
                                    ? data.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                    : data
                            ).map((row) => (
                                <TableRow key={row.name}>
                                    <TableCell component="th" scope="row">
                                        {row.name}
                                    </TableCell>
                                    <TableCell align="right">
                                        {row.monthlyPrice} zł
                                    </TableCell>
                                    <TableCell align="right" className={"TableRentalEquipmentColumnActions"}>
                                        <Box>
                                            <IconButton aria-label="delete" size="small"
                                                        onClick={() => handleEditClick(true, row.id, row.name, row.monthlyPrice)}>
                                                <EditIcon fontSize="small"/>
                                            </IconButton>
                                            <IconButton aria-label="delete" size="small" color={"error"}
                                                        onClick={() => handleDeleteClick(true, row.id, row.name, row.monthlyPrice)}>
                                                <DeleteIcon fontSize="small"/>
                                            </IconButton>
                                        </Box>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                        <TableFooter className={"RentalEquipmentTableFooter"}>
                            <TableRow>
                                <TablePagination
                                    rowsPerPageOptions={[5, 10, 25, {label: 'All', value: -1}]}
                                    colSpan={4}
                                    count={data.length}
                                    rowsPerPage={rowsPerPage}
                                    page={page}
                                    SelectProps={{
                                        inputProps: {
                                            'aria-label': 'salasld',
                                        },
                                        native: true,
                                    }}
                                    onPageChange={handleChangePage}
                                    onRowsPerPageChange={handleChangeRowsPerPage}
                                    ActionsComponent={TablePaginationActions}
                                />
                            </TableRow>
                        </TableFooter>
                    </Table>
                </SimpleBarReact>
            </TableContainer>
        </Paper>
    );
}

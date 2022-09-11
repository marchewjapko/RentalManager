import * as React from 'react';
import "./RentalEquipment.js.css"
import {RentalEquipmentMock} from "../../Mocks/RentalEquipmentMock";
import {
    Box,
    IconButton, InputAdornment,
    Paper,
    Table,
    TableBody,
    TableCell,
    TableContainer, TableFooter,
    TableHead, TablePagination,
    TableRow, TextField,
    useTheme, Stack
} from "@mui/material";
import KeyboardArrowLeft from '@mui/icons-material/KeyboardArrowLeft';
import KeyboardArrowRight from '@mui/icons-material/KeyboardArrowRight';
import PropTypes from "prop-types";
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import SearchIcon from '@mui/icons-material/Search';
import RentalEquipmentUpdateDialog from "./RentalEquipmentUpdateDialog";
import RentalEquipmentDeleteDialog from "./RentalEquipmentDeleteDialog";

import SimpleBarReact from "simplebar-react";
import "simplebar/src/simplebar.css";

function TablePaginationActions(props) {
    const theme = useTheme();
    const {count, page, rowsPerPage, onPageChange} = props;

    const handleBackButtonClick = (event) => {
        onPageChange(event, page - 1);
    };

    const handleNextButtonClick = (event) => {
        onPageChange(event, page + 1);
    };

    return (
        <Box sx={{flexShrink: 0, ml: 2.5}}>
            <IconButton
                onClick={handleBackButtonClick}
                disabled={page === 0}
                aria-label="previous page"
            >
                {theme.direction === 'rtl' ? <KeyboardArrowRight/> : <KeyboardArrowLeft/>}
            </IconButton>
            <IconButton
                onClick={handleNextButtonClick}
                disabled={page >= Math.ceil(count / rowsPerPage) - 1}
                aria-label="next page"
            >
                {theme.direction === 'rtl' ? <KeyboardArrowLeft/> : <KeyboardArrowRight/>}
            </IconButton>
        </Box>
    );
}

TablePaginationActions.propTypes = {
    count: PropTypes.number.isRequired,
    onPageChange: PropTypes.func.isRequired,
    page: PropTypes.number.isRequired,
    rowsPerPage: PropTypes.number.isRequired,
};

export default function RentalEquipment() {
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(5);
    const [showEditDialog, setShowEditDialog] = React.useState(false);
    const [showDeleteDialog, setShowDeleteDialog] = React.useState(false);
    const [equipmentEditName, setEquipmentEditName] = React.useState("");
    const [equipmentEditPrice, setEquipmentEditPrice] = React.useState(0);
    const [data, setData] = React.useState(RentalEquipmentMock);
    const [searchName, setSearchName] = React.useState('');

    const handleKeyDown = (event) => {
        if(event.key === 'Enter')
            filterData();
    }

    const handleChangeName = (event) => {
        setSearchName(event.target.value);
        if(event.target.value.length === 0) {
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
        setEquipmentEditName(equipmentName)
        setEquipmentEditPrice(equipmentPrice)
        setShowEditDialog(isShown)
    }

    const handleDeleteClick = (isShown, equipmentId, equipmentName, equipmentPrice) => {
        setEquipmentEditName(equipmentName)
        setEquipmentEditPrice(equipmentPrice)
        setShowDeleteDialog(isShown)
    }

    return (
        <Paper elevation={5} className={"RentalEquipmentContainer"}>
            {showEditDialog ? <RentalEquipmentUpdateDialog handleEditClick={handleEditClick} equipmentEditName={equipmentEditName} equipmentEditPrice={equipmentEditPrice}/> : null}
            {showDeleteDialog ? <RentalEquipmentDeleteDialog handleEditClick={handleDeleteClick} equipmentEditName={equipmentEditName} equipmentEditPrice={equipmentEditPrice}/> : null}
            <TableContainer className={"RentalEquipmentTable"}>
                <SimpleBarReact className={"RentalEquipmentTable"}>
                <Table stickyHeader aria-label="custom pagination table">
                    <TableHead>
                        <TableRow>
                            <TableCell className={"TableRentalEquipmentColumnName"}>Equipment name</TableCell>
                            <TableCell align="right" className={"TableRentalEquipmentColumnPrice"}>Monthly price</TableCell>
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
                                        <SearchIcon />
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
                                        <IconButton aria-label="delete" size="small" onClick={() => handleEditClick(true, row.id, row.name, row.monthlyPrice)}>
                                            <EditIcon fontSize="small" />
                                        </IconButton>
                                        <IconButton aria-label="delete" size="small" color={"error"} onClick={() => handleDeleteClick(true, row.id, row.name, row.monthlyPrice)}>
                                            <DeleteIcon fontSize="small" />
                                        </IconButton>
                                    </Box>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                    <TableFooter className={"RentalEquipmentTableFooter"}>
                        <TableRow>
                            <TablePagination
                                rowsPerPageOptions={[5, 10, 25, { label: 'All', value: -1 }]}
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

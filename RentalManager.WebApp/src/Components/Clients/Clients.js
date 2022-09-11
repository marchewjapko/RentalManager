import * as React from 'react';
import "./Clients.js.css"
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
import MoreHorizIcon from '@mui/icons-material/MoreHoriz';
import ClientsUpdateDialog from "./ClientsUpdateDialog";
import ClientsDeleteDialog from "./ClientsDeleteDialog";

import SimpleBarReact from "simplebar-react";
import "simplebar/src/simplebar.css";
import {ClientsMock} from "../../Mocks/ClientMock";

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

export default function Clients() {
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(5);
    const [showEditDialog, setShowEditDialog] = React.useState(false);
    const [showDeleteDialog, setShowDeleteDialog] = React.useState(false);
    const [ClientEditName, setClientEditName] = React.useState("");
    const [ClientEditSurname, setClientEditSurname] = React.useState("");
    const [data, setData] = React.useState(ClientsMock);
    const [searchName, setSearchName] = React.useState('');

    const handleKeyDown = (event) => {
        if(event.key === 'Enter')
            filterData();
    }

    const handleChangeName = (event) => {
        setSearchName(event.target.value);
        if(event.target.value.length === 0) {
            setData(ClientsMock.filter(x => x.name.includes('')));
        }
    };

    const filterData = () => {
        setData(ClientsMock.filter(x => x.name.includes(searchName)));
    }

    const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    const handleEditClick = (isShown, ClientId, ClientName, ClientSurname) => {
        setClientEditName(ClientName)
        setClientEditSurname(ClientSurname)
        setShowEditDialog(isShown)
    }

    const handleDeleteClick = (isShown, ClientId, ClientName, ClientSurname) => {
        setClientEditName(ClientName)
        setClientEditSurname(ClientSurname)
        setShowDeleteDialog(isShown)
    }

    return (
        <Paper elevation={5} className={"ClientsContainer"}>
            {showEditDialog ? <ClientsUpdateDialog handleEditClick={handleEditClick} clientEditName={ClientEditName} clientEditSurname={ClientEditSurname}/> : null}
            {showDeleteDialog ? <ClientsDeleteDialog handleEditClick={handleDeleteClick} clientEditName={ClientEditName} clientEditSurname={ClientEditSurname}/> : null}
            <TableContainer className={"ClientsTable"}>
                <SimpleBarReact className={"ClientsTable"}>
                    <Table stickyHeader aria-label="custom pagination table">
                        <TableHead>
                            <TableRow>
                                <TableCell className={"TableClientsColumnName"}>Name</TableCell>
                                <TableCell align="right" className={"TableClientsColumnSurname"}>Surname</TableCell>
                                <TableCell align="right">
                                    <Stack direction="row" justifyContent="flex-end">
                                        <TextField
                                            value={searchName}
                                            onChange={handleChangeName}
                                            placeholder="Search"
                                            variant="standard"
                                            size="small"
                                            className={"TableClientsSearchInput"}
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
                                <TableRow key={row.id}>
                                    <TableCell component="th" scope="row">
                                        {row.name}
                                    </TableCell>
                                    <TableCell align="right">
                                        {row.surname}
                                    </TableCell>
                                    <TableCell align="right" className={"TableClientsColumnActions"}>
                                        <Box>
                                            <IconButton aria-label="delete" size="small" onClick={() => handleDetailsClick(true, row.id, row.name, row.surname)}>
                                                <MoreHorizIcon fontSize="small" />
                                            </IconButton>
                                            <IconButton aria-label="delete" size="small" onClick={() => handleEditClick(true, row.id, row.name, row.surname)}>
                                                <EditIcon fontSize="small" />
                                            </IconButton>
                                            <IconButton aria-label="delete" size="small" color={"error"} onClick={() => handleDeleteClick(true, row.id, row.name, row.surname)}>
                                                <DeleteIcon fontSize="small" />
                                            </IconButton>
                                        </Box>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                        <TableFooter className={"ClientsTableFooter"}>
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

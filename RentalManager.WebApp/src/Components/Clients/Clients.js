import * as React from 'react';
import "./Clients.js.css"
import {
    IconButton,
    Paper,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableFooter,
    TableHead,
    TablePagination,
    TableRow,
} from "@mui/material";
import SearchIcon from '@mui/icons-material/Search';
import SimpleBarReact from "simplebar-react";
import "simplebar/src/simplebar.css";
import {ClientsMock} from "../../Mocks/ClientsMock";
import TablePaginationActions from "../TablePaginationActions";
import ClientTableRow from "./ClientTableRow";

export default function Clients() {
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(5);
    const [data, setData] = React.useState(ClientsMock);

    const filterData = () => {
        setData(ClientsMock);
    }

    const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    return (
        <Paper elevation={5} className={"ClientsContainer"}>
            <TableContainer className={"ClientsTable"}>
                <SimpleBarReact className={"ClientsTable"}>
                    <Table stickyHeader aria-label="custom pagination table">
                        <TableHead>
                            <TableRow>
                                <TableCell className={"ClientTableHeadDetails"}></TableCell>
                                <TableCell>Name</TableCell>
                                <TableCell align="right">Surname</TableCell>
                                <TableCell align="right">
                                    <IconButton color="default" onClick={() => filterData()}>
                                        <SearchIcon/>
                                    </IconButton>
                                </TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody className={"test"}>
                            {(rowsPerPage > 0
                                    ? data.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                    : data
                            ).map((row) => (
                                <ClientTableRow key={row.id} row={row}/>
                            ))}
                        </TableBody>
                        <TableFooter className={"ClientsTableFooter"}>
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

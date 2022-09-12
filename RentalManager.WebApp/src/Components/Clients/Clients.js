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
import {ClientsMock} from "../../Mocks/ClientsMock";
import TablePaginationActions from "../TablePaginationActions";
import ClientTableRow from "./ClientTableRow";
import {Scrollbars} from 'react-custom-scrollbars';
import {Link} from "react-router-dom";

export default function Clients() {
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(5);
    const [data, setData] = React.useState(ClientsMock);

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
                <Scrollbars autoHeight={true} autoHeightMin={0} autoHeightMax={650} autoHide autoHideTimeout={750} autoHideDuration={500}>
                    <Table stickyHeader>
                        <TableHead>
                            <TableRow>
                                <TableCell className={"ClientTableHeadDetails"}/>
                                <TableCell className={"ClientTableHeadName"}>Name</TableCell>
                                <TableCell align="right" className={"ClientTableHeadSurname"}>Surname</TableCell>
                                <TableCell align="right" className={"ClientTableHeadActions"}>
                                    <IconButton color="default">
                                        <SearchIcon/>
                                    </IconButton>
                                </TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
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
                                    onPageChange={handleChangePage}
                                    onRowsPerPageChange={handleChangeRowsPerPage}
                                    ActionsComponent={TablePaginationActions}
                                />
                            </TableRow>
                        </TableFooter>
                    </Table>
                </Scrollbars>
            </TableContainer>
            <Link to="/2">RentalEquipment</Link>
            <Link to="/3">Employees</Link>
        </Paper>
    );
}

import * as React from 'react';
import "./Clients.js.css"
import "../SharedStyles.css"
import {
    Alert,
    Dialog, DialogTitle,
    IconButton,
    Paper, Snackbar,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TablePagination,
    TableRow,
} from "@mui/material";
import SearchIcon from '@mui/icons-material/Search';
import ClientTableRow from "./ClientTableRow";
import {Scrollbars} from 'react-custom-scrollbars';
import {Link} from "react-router-dom";
import {getAllClients} from "../../Actions/ClientActions";
import SkeletonTableClients from "./SkeletonTableClients";
import ClientsUpdateDialog from "./ClientsUpdateDialog";
import ClientsDeleteDialog from "./ClientsDeleteDialog";

export default function Clients() {
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(5);
    const [isLoading, setIsLoading] = React.useState(true);
    const [data, setData] = React.useState();
    const [showSnackbar, setShowSnackbar] = React.useState(false);
    const [snackbarText, setSnackbarText] = React.useState("");
    const [showEditDialog, setShowEditDialog] = React.useState(false);
    const [showDeleteDialog, setShowDeleteDialog] = React.useState(false);
    const [focusedClient, setFocusedClient] = React.useState();

    const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    React.useEffect(() => {
        const getData = async () => {
            const result = await getAllClients();
            setData(result);
            setIsLoading(false)
        };
        getData();
    }, []);

    const handleEditClick = (client) => {
        setFocusedClient(client)
        setShowEditDialog(true)
    }

    const handleDeleteClick = (client) => {
        setFocusedClient(client)
        setShowDeleteDialog(true)
    }

    const handleCloseDialog = () => {
        setShowDeleteDialog(false)
        setShowEditDialog(false)
    }

    const handleDialogSuccess = async (mode) => {
        setIsLoading(true);
        setShowSnackbar(true);
        if(mode === "edit") {
            setSnackbarText("Client updated successfully")
        } else if(mode === "delete") {
            setSnackbarText("Client deleted successfully")
        } else {
            setSnackbarText("How did you do that?")
        }
        handleCloseDialog()
        const result = await getAllClients();
        setData(result);
        setIsLoading(false)
    }

    const closeSnackbars = () => {
        setShowSnackbar(false);
    }

    const editDialog = () => {
        return (
            <Dialog
                open={showEditDialog}
                maxWidth={"sm"}
                onClose={() => handleCloseDialog()}
            >
                <DialogTitle>{"Edit client"}</DialogTitle>
                <ClientsUpdateDialog client={focusedClient} handleCancelDialog={handleCloseDialog} handleDialogSuccess={handleDialogSuccess}/>
            </Dialog>
        );
    }

    const deleteDialog = () => {
        return (
            <Dialog
                open={showDeleteDialog}
                maxWidth={"sm"}
                onClose={() => handleCloseDialog()}
            >
                <DialogTitle>{"Delete client"}</DialogTitle>
                <ClientsDeleteDialog client={focusedClient} handleCancelDialog={handleCloseDialog} handleDialogSuccess={handleDialogSuccess}/>
            </Dialog>
        );
    }

    return (
        <div>
            <Snackbar open={showSnackbar} autoHideDuration={6000} onClose={closeSnackbars} anchorOrigin={{vertical: 'top', horizontal: 'center'}}>
                <Alert onClose={closeSnackbars} severity="success" sx={{ width: '100%' }}>
                    {snackbarText}
                </Alert>
            </Snackbar>
            <Paper className={"ComponentContainer"}>
                {isLoading ? (
                    <SkeletonTableClients/>
                ) : (
                    <div>
                        {editDialog()}
                        {deleteDialog()}
                    <TableContainer className={"ClientsTable"}>
                        <Scrollbars autoHeight={true} autoHeightMin={0} autoHeightMax={600} autoHide autoHideTimeout={750}
                                    autoHideDuration={500}>
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
                                        <ClientTableRow key={row.id} row={row} handleEditClick={handleEditClick} handleDeleteClick={handleDeleteClick}/>
                                    ))}
                                </TableBody>
                            </Table>
                        </Scrollbars>
                    </TableContainer>
                    <TablePagination
                    rowsPerPageOptions={[5, 10, 25, {label: 'All', value: -1}]}
                    component="div"
                    count={data.length}
                    rowsPerPage={rowsPerPage}
                    page={page}
                    onPageChange={handleChangePage}
                    onRowsPerPageChange={handleChangeRowsPerPage}
                    />
                    </div>
                )}
            </Paper>
            <Link to="/">Clients</Link>
            <Link to="/2">RentalEquipment</Link>
            <Link to="/3">Employees</Link>
        </div>
    );
}

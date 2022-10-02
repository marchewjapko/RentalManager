import * as React from 'react';
import "./Clients.js.css"
import "../SharedStyles.css"
import {
    Alert,
    Button,
    Dialog,
    DialogTitle,
    Paper,
    Snackbar,
    Stack,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TablePagination,
    TableRow,
} from "@mui/material";
import ClientTableRow from "./ClientTableRow";
import {Scrollbars} from 'react-custom-scrollbars-2';
import {filterClients, getAllClients} from "../../Actions/ClientActions";
import SkeletonTableClients from "./SkeletonTableClients";
import ClientsDialog from "./ClientsDialog";
import AddCircleRoundedIcon from "@mui/icons-material/AddCircleRounded";
import ClientsSearchSelect from "./ClientsSearchSelect";
import TempNavigation from "../Shared/TempNavigation";

export default function Clients() {
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(5);
    const [isLoading, setIsLoading] = React.useState(true);
    const [data, setData] = React.useState();
    const [showSnackbar, setShowSnackbar] = React.useState(false);
    const [showDialog, setShowDialog] = React.useState(false);
    const [focusedClient, setFocusedClient] = React.useState();
    const [dialogMode, setDialogMode] = React.useState("")

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

    const getDialogTitle = () => {
        switch (dialogMode) {
            case 'post':
                return 'Add client'
            case 'update':
                return 'Edit client'
            case 'delete':
                return 'Delete client'
        }
    }

    const getSnackbarTitle = () => {
        switch (dialogMode) {
            case 'post':
                return 'Client added successfully'
            case 'update':
                return 'Client updated successfully'
            case 'delete':
                return 'Client deleted successfully'
        }
    }

    const handleEditClick = (client) => {
        setFocusedClient(client)
        setDialogMode("update")
        setShowDialog(true)
    }

    const handleDeleteClick = (client) => {
        setFocusedClient(client)
        setDialogMode("delete")
        setShowDialog(true)
    }

    const handleAddClick = () => {
        setDialogMode("post")
        setFocusedClient({
            name: "",
            surname: "",
            phone: "",
            email: "",
            idCard: "",
            city: "",
            street: "",
            streetNumber: ""
        })
        setShowDialog(true)
    }

    const handleCloseDialog = () => {
        setShowDialog(false)
    }

    const handleDialogSuccess = async (mode) => {
        setIsLoading(true);
        setShowSnackbar(true);
        handleCloseDialog()
        const result = await getAllClients();
        setData(result);
        setIsLoading(false)
    }

    const closeSnackbars = () => {
        setShowSnackbar(false);
    }

    const dialog = () => {
        return (
            <Dialog
                open={showDialog}
                maxWidth={"sm"}
                onClose={() => handleCloseDialog()}
            >
                <DialogTitle>{getDialogTitle()}</DialogTitle>
                <ClientsDialog client={focusedClient}
                               handleCancelDialog={handleCloseDialog}
                               handleDialogSuccess={handleDialogSuccess}
                               mode={dialogMode}/>
            </Dialog>
        );
    }

    const handleSearch = async (searchValues) => {
        setIsLoading(true);
        const result = await filterClients(searchValues);
        setData(result);
        setIsLoading(false);
        setPage(0);
    }

    return (
        <div>
            <Snackbar open={showSnackbar} autoHideDuration={6000} onClose={closeSnackbars}
                      anchorOrigin={{vertical: 'top', horizontal: 'left'}}>
                <Alert onClose={closeSnackbars} severity="success" sx={{width: '100%'}}>
                    {getSnackbarTitle()}
                </Alert>
            </Snackbar>
            <Paper className={"ComponentContainer"}>
                <Stack direction={"row"} justifyContent="space-between" alignItems="center"
                       sx={{marginRight: "10px", marginBottom: "10px"}}>
                    <Button startIcon={<AddCircleRoundedIcon/>} color={"primary"} variant={"contained"}
                            onClick={handleAddClick} disabled={isLoading}>
                        Add client
                    </Button>
                    <ClientsSearchSelect isLoading={isLoading} handleSearch={handleSearch}/>
                </Stack>
                {isLoading ? (
                    <SkeletonTableClients/>
                ) : (
                    <div>
                        {dialog()}
                        <TableContainer className={"ClientsTable"}>
                            <Scrollbars autoHeight={true} autoHeightMin={0} autoHeightMax={600} autoHide
                                        autoHideTimeout={750}
                                        autoHideDuration={500}>
                                <Table stickyHeader>
                                    <TableHead>
                                        <TableRow>
                                            <TableCell className={"ClientTableHeadDetails"}/>
                                            <TableCell className={"ClientTableHeadName"}>Name</TableCell>
                                            <TableCell align="right"
                                                       className={"ClientTableHeadSurname"}>Surname</TableCell>
                                            <TableCell align="right" className={"ClientTableHeadActions"}/>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {(rowsPerPage > 0
                                                ? data.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                                : data
                                        ).map((row) => (
                                            <ClientTableRow key={row.id} row={row} handleEditClick={handleEditClick}
                                                            handleDeleteClick={handleDeleteClick}/>
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
            <TempNavigation/>
        </div>
    );
}

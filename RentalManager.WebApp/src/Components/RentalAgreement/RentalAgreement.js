import * as React from 'react';
// import "./Clients.js.css"
import "../SharedStyles.css"
import {
    Alert, Box,
    Button,
    Checkbox,
    Dialog,
    DialogTitle, IconButton,
    MenuItem,
    OutlinedInput,
    Paper,
    Select,
    Snackbar,
    Stack,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TablePagination,
    TableRow,
    TextField,
} from "@mui/material";
import {Scrollbars} from 'react-custom-scrollbars-2';
import {Link} from "react-router-dom";
import {filterClients, getAllClients} from "../../Actions/ClientActions";
// import SkeletonTableClients from "./SkeletonTableClients";
// import ClientsDialog from "./ClientsDialog";
import AddCircleRoundedIcon from "@mui/icons-material/AddCircleRounded";
// import ClientsSearchSelect from "./ClientsSearchSelect";
import TempNavigation from "../Shared/TempNavigation";
import {RentalAgreementMock} from "../../Mocks/RentalAgreementMock";
import SearchTextField from "../Shared/SearchTextField";
import SkeletonTableRentalEquipment from "../RentalEquipment/SkeletonTableRentalEquipment";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import MoreHorizIcon from '@mui/icons-material/MoreHoriz';
import dayjs from "dayjs";
import 'dayjs/locale/pl';

export default function RentalAgreement() {
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(5);
    const [isLoading, setIsLoading] = React.useState(false);
    const [data, setData] = React.useState(RentalAgreementMock);
    // const [showSnackbar, setShowSnackbar] = React.useState(false);
    // const [showDialog, setShowDialog] = React.useState(false);
    // const [focusedClient, setFocusedClient] = React.useState();
    // const [dialogMode, setDialogMode] = React.useState("")

    const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    // React.useEffect(() => {
    //     const getData = async () => {
    //         const result = await getAllClients();
    //         setData(result);
    //         setIsLoading(false)
    //     };
    //     getData();
    // }, []);

    // const getDialogTitle = () => {
    //     switch (dialogMode) {
    //         case 'post':
    //             return 'Add client'
    //         case 'update':
    //             return 'Edit client'
    //         case 'delete':
    //             return 'Delete client'
    //     }
    // }
    //
    // const getSnackbarTitle = () => {
    //     switch (dialogMode) {
    //         case 'post':
    //             return 'Client added successfully'
    //         case 'update':
    //             return 'Client updated successfully'
    //         case 'delete':
    //             return 'Client deleted successfully'
    //     }
    // }

    const handleEditClick = (client) => {
        // setFocusedClient(client)
        // setDialogMode("update")
        // setShowDialog(true)
    }

    const handleDeleteClick = (client) => {
        // setFocusedClient(client)
        // setDialogMode("delete")
        // setShowDialog(true)
    }
    const handleAddClick = () => {
        // setDialogMode("post")
        // setFocusedClient({
        //     name: "",
        //     surname: "",
        //     phone: "",
        //     email: "",
        //     idCard: "",
        //     city: "",
        //     street: "",
        //     streetNumber: ""
        // })
        // setShowDialog(true)
    }

    // const handleCloseDialog = () => {
    //     setShowDialog(false)
    // }
    //
    // const handleDialogSuccess = async (mode) => {
    //     setIsLoading(true);
    //     setShowSnackbar(true);
    //     handleCloseDialog()
    //     const result = await getAllClients();
    //     setData(result);
    //     setIsLoading(false)
    // }
    //
    // const closeSnackbars = () => {
    //     setShowSnackbar(false);
    // }



    // const dialog = () => {
    //     return (
    //         <Dialog
    //             open={showDialog}
    //             maxWidth={"sm"}
    //             onClose={() => handleCloseDialog()}
    //         >
    //             {}
    //             <DialogTitle>{getDialogTitle()}</DialogTitle>
    //             <ClientsDialog client={focusedClient}
    //                            handleCancelDialog={handleCloseDialog}
    //                            handleDialogSuccess={handleDialogSuccess}
    //                            mode={dialogMode}/>
    //         </Dialog>
    //     );
    // }

    // const handleSearch = async (searchValues) => {
    //     setIsLoading(true);
    //     const result = await filterClients(searchValues);
    //     setData(result);
    //     setIsLoading(false)
    // }
    const getDate = (date) => {
        let formatData = dayjs(date).format('dddd, DD MMMM YYYY').replace(/(^|\s)\S/g, l => l.toUpperCase())
        return formatData
    }
    dayjs.locale('pl')
    return (
        <div>
            <Paper className={"ComponentContainer"}>
                <Stack direction={"row"} justifyContent="space-between" alignItems="center"
                       sx={{marginRight: "10px", marginBottom: "10px"}}>
                    <Button startIcon={<AddCircleRoundedIcon/>} variant={"contained"}
                            onClick={handleAddClick} disabled={isLoading}>
                        Add agreement
                    </Button>
                    {/*<SearchTextField isLoading={isLoading} handleSearch={handleSearch}/>*/}
                </Stack>
                {/*<Snackbar open={showSnackbar} autoHideDuration={6000} onClose={closeSnackbars}*/}
                {/*          anchorOrigin={{vertical: 'top', horizontal: 'left'}}>*/}
                {/*    <Alert onClose={closeSnackbars} severity="success" sx={{width: '100%'}}>*/}
                {/*        {getSnackbarTitle()}*/}
                {/*    </Alert>*/}
                {/*</Snackbar>*/}
                {/*{isLoading ? (*/}
                {/*    <SkeletonTableRentalEquipment/>*/}
                {/*) : (*/}
                    <div>
                        {/*{dialog()}*/}
                        <TableContainer className={"RentalEquipmentTable"}>
                            <Scrollbars autoHeight={true} autoHeightMin={0} autoHeightMax={460} autoHide
                                        autoHideTimeout={750}
                                        autoHideDuration={500}>
                                <Table stickyHeader>
                                    <TableHead>
                                        <TableRow>
                                            <TableCell className={"TableRentalEquipmentColumnName"}>Client</TableCell>
                                            <TableCell align="right" className={"TableRentalEquipmentColumnPrice"}>Date added</TableCell>
                                            <TableCell align="right" sx={{width: "150px"}}/>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {(rowsPerPage > 0
                                                ? data.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                                : data
                                        ).map((row) => (
                                            <TableRow key={row.id}>
                                                <TableCell component="th" scope="row">
                                                    {row.client.name + ' ' + row.client.surname}
                                                </TableCell>
                                                <TableCell align="right">
                                                    {getDate(row.dateAdded)}
                                                </TableCell>
                                                <TableCell align="right">
                                                    <Box>
                                                        <IconButton aria-label="delete" size="small"
                                                                    onClick={() => handleEditClick(row)}>
                                                            <EditIcon fontSize="small"/>
                                                        </IconButton>
                                                        <IconButton aria-label="delete" size="small" color={"error"}
                                                                    onClick={() => handleDeleteClick(row)}>
                                                            <DeleteIcon fontSize="small"/>
                                                        </IconButton>
                                                        <IconButton aria-label="delete" size="small"
                                                                    onClick={() => handleDeleteClick(row)}>
                                                            <MoreHorizIcon fontSize="small"/>
                                                        </IconButton>
                                                    </Box>
                                                </TableCell>
                                            </TableRow>
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
                {/*)}*/}
            </Paper>
            <TempNavigation/>
        </div>
    );
}

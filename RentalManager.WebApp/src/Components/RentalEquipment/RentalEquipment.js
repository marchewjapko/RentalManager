import * as React from 'react';
import "./RentalEquipment.js.css"
import {RentalEquipmentMock} from "../../Mocks/RentalEquipmentMock";
import {
    Alert,
    Box, Dialog, DialogTitle,
    IconButton,
    Paper, Snackbar,
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
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import SearchIcon from '@mui/icons-material/Search';
import RentalEquipmentUpdateDialog from "./RentalEquipmentUpdateDialog";
import RentalEquipmentDeleteDialog from "./RentalEquipmentDeleteDialog";
import {Scrollbars} from 'react-custom-scrollbars';
import {Link} from "react-router-dom";
import {filterRentalEquipment, getAllRentalEquipment} from "../../Actions/RentalEquipmentActions";
import SkeletonTableRentalEquipment from "./SkeletonTableRentalEquipment";

export default function RentalEquipment() {
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(5);
    const [showEditDialog, setShowEditDialog] = React.useState(false);
    const [showDeleteDialog, setShowDeleteDialog] = React.useState(false);
    const [focusedRentalEquipment, setFocusedRentalEquipment] = React.useState();
    const [data, setData] = React.useState(RentalEquipmentMock);
    const [searchName, setSearchName] = React.useState('');
    const [isLoading, setIsLoading] = React.useState(true);
    const [showSnackbar, setShowSnackbar] = React.useState(false);
    const [snackbarText, setSnackbarText] = React.useState("");

    const handleSearch = async () => {
        setIsLoading(true);
        const result = await filterRentalEquipment(searchName);
        setData(result)
        setIsLoading(false);
    }

    const handleKeyDown = (event) => {
        if (event.key === 'Enter') {
            handleSearch();
        }
    }

    const handleChangeName = (event) => {
        setSearchName(event.target.value);
    };

    const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    const handleEditClick = (employee) => {
        setFocusedRentalEquipment(employee)
        setShowEditDialog(true)
    }

    const handleDeleteClick = (employee) => {
        setFocusedRentalEquipment(employee)
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
            setSnackbarText("Rental Equipment updated successfully")
        } else if(mode === "delete") {
            setSnackbarText("Rental Equipment deleted successfully")
        } else {
            setSnackbarText("How did you do that?")
        }
        handleCloseDialog()
        const result = await getAllRentalEquipment();
        setData(result);
        setIsLoading(false)
    }

    const closeSnackbars = () => {
        setShowSnackbar(false);
    }

    React.useEffect(() => {
        const getData = async () => {
            const result = await getAllRentalEquipment();
            setData(result);
            setIsLoading(false)
        };
        getData();
    }, []);

    const editDialog = () => {
        return (
            <Dialog
                open={showEditDialog}
                maxWidth={"sm"}
                onClose={() => handleCloseDialog()}
            >
                <DialogTitle>{"Edit rental equipment"}</DialogTitle>
                <RentalEquipmentUpdateDialog rentalEquipment={focusedRentalEquipment} handleCancelDialog={handleCloseDialog} handleDialogSuccess={handleDialogSuccess}/>
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
                <DialogTitle>{"Delete rental equipment"}</DialogTitle>
                <RentalEquipmentDeleteDialog rentalEquipment={focusedRentalEquipment} handleCancelDialog={handleCloseDialog} handleDialogSuccess={handleDialogSuccess}/>
            </Dialog>
        );
    }

    return (
        <div>
            <Paper className={"ComponentContainer"}>
                <Snackbar open={showSnackbar} autoHideDuration={6000} onClose={closeSnackbars} anchorOrigin={{vertical: 'top', horizontal: 'center'}}>
                    <Alert onClose={closeSnackbars} severity="success" sx={{ width: '100%' }}>
                        {snackbarText}
                    </Alert>
                </Snackbar>
                {isLoading ? (
                    <SkeletonTableRentalEquipment searchPrase={searchName}/>
                ) : (
                    <div>
                        {editDialog()}
                        {deleteDialog()}
                        <TableContainer className={"RentalEquipmentTable"}>
                            <Scrollbars autoHeight={true} autoHeightMin={0} autoHeightMax={460} autoHide autoHideTimeout={750}
                                        autoHideDuration={500}>
                                <Table stickyHeader>
                                    <TableHead>
                                        <TableRow>
                                            <TableCell className={"TableRentalEquipmentColumnName"}>Equipment name</TableCell>
                                            <TableCell align="right" className={"TableRentalEquipmentColumnPrice"}>Monthly
                                                price</TableCell>
                                            <TableCell align="right" className={"TableHeadActionsWithSearch"}>
                                                <Stack direction="row" justifyContent="flex-end">
                                                    <TextField
                                                        value={searchName}
                                                        onChange={handleChangeName}
                                                        placeholder="Search"
                                                        variant="standard"
                                                        size="small"
                                                        className={"TableSearchInput"}
                                                        InputProps={{
                                                            style: {fontSize: '1em'},
                                                        }}
                                                        onKeyDown={handleKeyDown}
                                                    />
                                                    <IconButton color="default" onClick={handleSearch}>
                                                        <SearchIcon/>
                                                    </IconButton>
                                                </Stack>
                                            </TableCell>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
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
                )}
            </Paper>
            <Link to="/">Client</Link>
            <Link to="/3">Employees</Link>
        </div>
    );
}

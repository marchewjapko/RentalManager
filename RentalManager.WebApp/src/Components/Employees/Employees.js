import * as React from 'react';
import "./Employees.js.css"
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
    TableRow,
    TextField
} from "@mui/material";
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import SearchIcon from '@mui/icons-material/Search';
import EmployeesUpdateDialog from "./EmployeesUpdateDialog";
import EmployeesDeleteDialog from "./EmployeesDeleteDialog";
import {Scrollbars} from 'react-custom-scrollbars-2';
import {Link} from "react-router-dom";
import {filterEmployees, getAllEmployees} from "../../Actions/EmployeeActions";
import SkeletonTableEmployees from "./SkeletonTableEmployees";

export default function Employees() {
    const [showEditDialog, setShowEditDialog] = React.useState(false);
    const [showDeleteDialog, setShowDeleteDialog] = React.useState(false);
    const [focusedEmployee, setFocusedEmployee] = React.useState();
    const [data, setData] = React.useState();
    const [searchName, setSearchName] = React.useState('');
    const [isLoading, setIsLoading] = React.useState(true);
    const [showSnackbar, setShowSnackbar] = React.useState(false);
    const [snackbarText, setSnackbarText] = React.useState("");

    const handleSearch = async () => {
        setIsLoading(true);
        const result = await filterEmployees(searchName);
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

    const handleEditClick = (employee) => {
        setFocusedEmployee(employee)
        setShowEditDialog(true)
    }

    const handleDeleteClick = (employee) => {
        setFocusedEmployee(employee)
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
            setSnackbarText("Employee updated successfully")
        } else if(mode === "delete") {
            setSnackbarText("Employee deleted successfully")
        } else {
            setSnackbarText("How did you do that?")
        }
        handleCloseDialog()
        const result = await getAllEmployees();
        setData(result);
        setIsLoading(false)
    }

    const closeSnackbars = () => {
        setShowSnackbar(false);
    }

    React.useEffect(() => {
        const getData = async () => {
            const result = await getAllEmployees();
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
                <DialogTitle>{"Edit employee"}</DialogTitle>
                <EmployeesUpdateDialog employee={focusedEmployee} handleCancelDialog={handleCloseDialog} handleDialogSuccess={handleDialogSuccess}/>
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
                <DialogTitle>{"Delete employee"}</DialogTitle>
                <EmployeesDeleteDialog employee={focusedEmployee} handleCancelDialog={handleCloseDialog} handleDialogSuccess={handleDialogSuccess}/>
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
                    <SkeletonTableEmployees searchPrase={searchName}/>
                ) : (
                    <div>
                        {editDialog()}
                        {deleteDialog()}
                        <TableContainer className={"EmployeesTable"}>
                            <Scrollbars autoHeight={true} autoHeightMin={0} autoHeightMax={460} autoHide
                                        autoHideTimeout={750} autoHideDuration={500}>
                                <Table stickyHeader>
                                    <TableHead>
                                        <TableRow>
                                            <TableCell className={"EmployeesTableHeadName"}>Name</TableCell>
                                            <TableCell align="right"
                                                       className={"EmployeesTableHeadSurname"}>Surname</TableCell>
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
                                                    <IconButton color="default"
                                                                onClick={handleSearch}>
                                                        <SearchIcon/>
                                                    </IconButton>
                                                </Stack>
                                            </TableCell>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {data.map((row) => (
                                            <TableRow key={row.name}>
                                                <TableCell component="th" scope="row">
                                                    {row.name}
                                                </TableCell>
                                                <TableCell align="right">
                                                    {row.surname}
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
                    </div>
                )}
            </Paper>
            <Link to="/">Clients</Link>
            <Link to="/2">RentalEquipment</Link>
        </div>
    );
}

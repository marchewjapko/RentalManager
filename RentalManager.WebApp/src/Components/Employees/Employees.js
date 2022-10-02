import * as React from 'react';
import "./Employees.js.css"
import {
    Alert,
    Box,
    Button,
    Dialog,
    DialogTitle,
    IconButton, Paper,
    Snackbar,
    Stack,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
} from "@mui/material";
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import {Scrollbars} from 'react-custom-scrollbars-2';
import {filterEmployees, getAllEmployees} from "../../Actions/EmployeeActions";
import SkeletonTableEmployees from "./SkeletonTableEmployees";
import AddCircleRoundedIcon from "@mui/icons-material/AddCircleRounded";
import SearchTextField from "../Shared/SearchTextField";
import EmployeeDialog from "./EmployeeDialog";
import TempNavigation from "../Shared/TempNavigation";

export default function Employees() {
    const [showDialog, setShowDialog] = React.useState(false);
    const [focusedEmployee, setFocusedEmployee] = React.useState();
    const [data, setData] = React.useState();
    const [isLoading, setIsLoading] = React.useState(true);
    const [showSnackbar, setShowSnackbar] = React.useState(false);
    const [dialogMode, setDialogMode] = React.useState("")

    const handleSearch = async (searchName) => {
        setIsLoading(true);
        const result = await filterEmployees(searchName);
        setData(result)
        setIsLoading(false);
    }

    const handleAddClick = () => {
        setDialogMode("post")
        setFocusedEmployee({name: "", surname: ""})
        setShowDialog(true)
    }

    const handleEditClick = (employee) => {
        setDialogMode("update")
        setFocusedEmployee(employee)
        setShowDialog(true)
    }

    const handleDeleteClick = (employee) => {
        setDialogMode("delete")
        setFocusedEmployee(employee)
        setShowDialog(true)
    }

    const handleCloseDialog = () => {
        setShowDialog(false)
    }

    const handleDialogSuccess = async (mode) => {
        setIsLoading(true);
        setShowSnackbar(true);
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

    const getDialogTitle = () => {
        switch (dialogMode) {
            case 'post':
                return 'Add employee'
            case 'update':
                return 'Edit employee'
            case 'delete':
                return 'Delete employee'
        }
    }

    const getSnackbarTitle = () => {
        switch (dialogMode) {
            case 'post':
                return 'Employee added successfully'
            case 'update':
                return 'Employee updated successfully'
            case 'delete':
                return 'Employee deleted successfully'
        }
    }

    const dialog = () => {
        return (
            <Dialog
                open={showDialog}
                maxWidth={"sm"}
                onClose={() => handleCloseDialog()}
            >
                <DialogTitle>{getDialogTitle()}</DialogTitle>
                <EmployeeDialog employee={focusedEmployee}
                                handleCancelDialog={handleCloseDialog}
                                handleDialogSuccess={handleDialogSuccess}
                                mode={dialogMode}/>
            </Dialog>
        );
    }

    return (
        <div>
            <Paper className={"ComponentContainer"}>
                <Stack direction={"row"} justifyContent="space-between" alignItems="center"
                       sx={{marginRight: "10px", marginBottom: "10px"}}>
                    <Button startIcon={<AddCircleRoundedIcon/>} variant={"contained"}
                            onClick={handleAddClick} disabled={isLoading}>
                        Add employee
                    </Button>
                    <SearchTextField isLoading={isLoading} handleSearch={handleSearch}/>
                </Stack>
                <Snackbar open={showSnackbar} autoHideDuration={6000} onClose={closeSnackbars}
                          anchorOrigin={{vertical: 'top', horizontal: 'left'}}>
                    <Alert onClose={closeSnackbars} severity="success" sx={{width: '100%'}}>
                        {getSnackbarTitle()}
                    </Alert>
                </Snackbar>
                {isLoading ? (
                    <SkeletonTableEmployees/>
                ) : (
                    <div>
                        {dialog()}
                        <TableContainer className={"EmployeesTable"}>
                            <Scrollbars autoHeight={true} autoHeightMin={0} autoHeightMax={460} autoHide
                                        autoHideTimeout={750} autoHideDuration={500}>
                                <Table stickyHeader>
                                    <TableHead>
                                        <TableRow>
                                            <TableCell className={"EmployeesTableHeadName"}>Name</TableCell>
                                            <TableCell align="right"
                                                       className={"EmployeesTableHeadSurname"}>Surname</TableCell>
                                            <TableCell align="right" sx={{width: "100px"}}/>
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
            <TempNavigation/>
        </div>
    );
}

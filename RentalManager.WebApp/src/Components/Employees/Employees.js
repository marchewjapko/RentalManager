import * as React from 'react';
import "./Employees.js.css"
import {EmployeesMock} from "../../Mocks/EmployeesMock";
import {
    Box,
    IconButton,
    Paper,
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
import {Scrollbars} from 'react-custom-scrollbars';
import {Link} from "react-router-dom";

export default function Employees() {
    const [showEditDialog, setShowEditDialog] = React.useState(false);
    const [showDeleteDialog, setShowDeleteDialog] = React.useState(false);
    const [employeeName, setEmployeeName] = React.useState("");
    const [employeeSurname, setEmployeeSurname] = React.useState(0);
    const [data, setData] = React.useState(EmployeesMock);
    const [searchName, setSearchName] = React.useState('');

    const handleKeyDown = (event) => {
        if (event.key === 'Enter')
            filterData();
    }

    const handleChangeName = (event) => {
        setSearchName(event.target.value);
        if (event.target.value.length === 0) {
            setData(EmployeesMock.filter(x => x.surname.includes('')));
        }
    };

    const filterData = () => {
        setData(EmployeesMock.filter(x => x.surname.toLowerCase().includes(searchName.toLowerCase())));
    }

    const handleEditClick = (isShown, employeeName, employeeSurname) => {
        setEmployeeName(employeeName)
        setEmployeeSurname(employeeSurname)
        setShowEditDialog(isShown)
    }

    const handleDeleteClick = (isShown, employeeName, employeeSurname) => {
        setEmployeeName(employeeName)
        setEmployeeSurname(employeeSurname)
        setShowDeleteDialog(isShown)
    }

    return (
        <Paper elevation={5} className={"EmployeesContainer"}>
            {showEditDialog ?
                <EmployeesUpdateDialog handleEditClick={handleEditClick} employeeName={employeeName}
                                       employeeSurname={employeeSurname}/> : null}
            {showDeleteDialog ?
                <EmployeesDeleteDialog handleDeleteClick={handleDeleteClick} employeeName={employeeName}
                                       employeeSurname={employeeSurname}/> : null}
            <TableContainer className={"EmployeesTable"}>
                <Scrollbars autoHeight={true} autoHeightMin={0} autoHeightMax={460} autoHide autoHideTimeout={750} autoHideDuration={500}>
                    <Table stickyHeader>
                        <TableHead>
                            <TableRow>
                                <TableCell className={"EmployeesTableHeadName"}>Name</TableCell>
                                <TableCell align="right" className={"EmployeesTableHeadSurname"}>Surname</TableCell>
                                <TableCell align="right" className={"EmployeesTableHeadActions"}>
                                    <Stack direction="row" justifyContent="flex-end">
                                        <TextField
                                            value={searchName}
                                            onChange={handleChangeName}
                                            placeholder="Search"
                                            variant="standard"
                                            size="small"
                                            className={"TableEmployeesSearchInput"}
                                            InputProps={{
                                                style: {fontSize: '0.875em'},
                                            }}
                                            onKeyDown={handleKeyDown}
                                        />
                                        <IconButton color="default" onClick={() => filterData()}>
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
                                    <TableCell align="right" className={"TableRentalEquipmentColumnActions"}>
                                        <Box>
                                            <IconButton aria-label="delete" size="small"
                                                        onClick={() => handleEditClick(true, row.name, row.surname)}>
                                                <EditIcon fontSize="small"/>
                                            </IconButton>
                                            <IconButton aria-label="delete" size="small" color={"error"}
                                                        onClick={() => handleDeleteClick(true, row.name, row.surname)}>
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
            <Link to="/">Clients</Link>
            <Link to="/2">RentalEquipment</Link>
        </Paper>
    );
}

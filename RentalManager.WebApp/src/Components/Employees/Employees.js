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
import {filterEmployees, getAllEmployees} from "../../Actions/EmployeeActions";
import SkeletonTableEmployees from "./SkeletonTableEmployees";

export default function Employees() {
    const [showEditDialog, setShowEditDialog] = React.useState(false);
    const [showDeleteDialog, setShowDeleteDialog] = React.useState(false);
    const [employeeName, setEmployeeName] = React.useState("");
    const [employeeSurname, setEmployeeSurname] = React.useState(0);
    const [data, setData] = React.useState();
    const [searchName, setSearchName] = React.useState('');
    const [isLoading, setIsLoading] = React.useState(true);

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

    React.useEffect(() => {
        const getData = async () => {
            const result = await getAllEmployees();
            setData(result);
            setIsLoading(false)
        };
        getData();
    }, []);
    return (
        <div>
            <Paper className={"ComponentContainer"}>
                {isLoading ? (
                    <SkeletonTableEmployees searchPrase={searchName}/>
                ) : (
                    <div>
                        {showEditDialog ?
                            <EmployeesUpdateDialog handleEditClick={handleEditClick} employeeName={employeeName}
                                                   employeeSurname={employeeSurname}/> : null}
                        {showDeleteDialog ?
                            <EmployeesDeleteDialog handleDeleteClick={handleDeleteClick} employeeName={employeeName}
                                                   employeeSurname={employeeSurname}/> : null}
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
                    </div>
                )}
            </Paper>
            <Link to="/">Clients</Link>
            <Link to="/2">RentalEquipment</Link>
        </div>
    );
}

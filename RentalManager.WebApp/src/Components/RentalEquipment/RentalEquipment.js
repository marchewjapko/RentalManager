import * as React from 'react';
import "./RentalEquipment.js.css"
import {RentalEquipmentMock} from "../../Mocks/RentalEquipmentMock";
import {
    Alert,
    Box,
    Button,
    Checkbox,
    Dialog,
    DialogTitle,
    IconButton,
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
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import {Scrollbars} from 'react-custom-scrollbars-2';
import {filterRentalEquipment, getAllRentalEquipment} from "../../Actions/RentalEquipmentActions";
import SkeletonTableRentalEquipment from "./SkeletonTableRentalEquipment";
import AddCircleRoundedIcon from '@mui/icons-material/AddCircleRounded';
import SearchTextField from "../Shared/SearchTextField";
import RentalEquipmentDialog from "./RentalEquipmentDialog";
import TempNavigation from "../Shared/TempNavigation";

export default function RentalEquipment({isCheckable, initialIds}) {
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(5);
    const [showDialog, setShowDialog] = React.useState(false);
    const [focusedRentalEquipment, setFocusedRentalEquipment] = React.useState();
    const [data, setData] = React.useState();
    const [isLoading, setIsLoading] = React.useState(true);
    const [showSnackbar, setShowSnackbar] = React.useState(false);
    const [dialogMode, setDialogMode] = React.useState("")
    const [checkedIds, setCheckedIds] = React.useState(initialIds ? initialIds : [0])

    const handleSearch = async (searchName) => {
        setIsLoading(true);
        const result = await filterRentalEquipment(searchName);
        setData(result)
        setIsLoading(false);
        setPage(0);
    }

    const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    const handleEditClick = (rentalEquipment) => {
        setFocusedRentalEquipment(rentalEquipment)
        setDialogMode("update")
        setShowDialog(true)
    }

    const handleDeleteClick = (rentalEquipment) => {
        setFocusedRentalEquipment(rentalEquipment)
        setDialogMode("delete")
        setShowDialog(true)
    }

    const handleAddClick = () => {
        setDialogMode("post")
        setFocusedRentalEquipment({id: 0, name: "", monthlyPrice: ""})
        setShowDialog(true)
    }

    const handleCloseDialog = () => {
        setShowDialog(false)
    }

    const handleDialogSuccess = async (mode, rentalEquipment) => {
        setIsLoading(true);
        setShowSnackbar(true);
        handleCloseDialog()
        const result = await getAllRentalEquipment();
        setData(result);
        setIsLoading(false)
        if(mode === 'delete' && checkedIds.includes(rentalEquipment.id)) {
            setCheckedIds(checkedIds.filter(x => x !== rentalEquipment.id))
        }
        if(mode === 'post') {
            setCheckedIds(x => [...x, rentalEquipment.id])
        }
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

    const getDialogTitle = () => {
        switch (dialogMode) {
            case 'post':
                return 'Add rental equipment'
            case 'update':
                return 'Edit rental equipment'
            case 'delete':
                return 'Delete rental equipment'
        }
    }

    const getSnackbarTitle = () => {
        switch (dialogMode) {
            case 'post':
                return 'Equipment added successfully'
            case 'update':
                return 'Equipment updated successfully'
            case 'delete':
                return 'Equipment deleted successfully'
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
                <RentalEquipmentDialog rentalEquipment={focusedRentalEquipment}
                                       handleCancelDialog={handleCloseDialog}
                                       handleDialogSuccess={handleDialogSuccess}
                                       mode={dialogMode}/>
            </Dialog>
        );
    }

    const handleCheckboxChange = (equipment) => {
        if (checkedIds.includes(equipment.id)) {
            setCheckedIds(checkedIds.filter(x => x !== equipment.id))
        } else {
            setCheckedIds(x => [...x, equipment.id])
        }
    }

    return (
        <div>
            <Paper className={"ComponentContainer"}>
                <Stack direction={"row"} justifyContent="space-between" alignItems="center"
                       sx={{marginRight: "10px", marginBottom: "10px"}}>
                    <Button startIcon={<AddCircleRoundedIcon/>} variant={"contained"}
                            onClick={handleAddClick} disabled={isLoading}>
                        Add equipment
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
                    <SkeletonTableRentalEquipment/>
                ) : (
                    <div>
                        {dialog()}
                        <TableContainer className={"RentalEquipmentTable"}>
                            <Scrollbars autoHeight={true} autoHeightMin={0} autoHeightMax={460} autoHide
                                        autoHideTimeout={750}
                                        autoHideDuration={500}>
                                <Table stickyHeader>
                                    <TableHead>
                                        <TableRow>
                                            {isCheckable ? <TableCell sx={{width: 0}}/> : null}
                                            <TableCell className={"TableRentalEquipmentColumnName"}>Equipment
                                                name</TableCell>
                                            <TableCell align="right" className={"TableRentalEquipmentColumnPrice"}>Monthly
                                                price</TableCell>
                                            <TableCell align="right" sx={{width: "100px"}}/>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {(rowsPerPage > 0
                                                ? data.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                                : data
                                        ).map((row) => (
                                            <TableRow key={row.name}>
                                                {isCheckable ? (
                                                    <TableCell component="th" scope="row">
                                                        <Checkbox onChange={() => handleCheckboxChange(row)}
                                                                  checked={checkedIds.includes(row.id)}
                                                                  sx={{height: "30px", width: "30px"}}/>
                                                    </TableCell>
                                                ) : null}
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
            <TempNavigation/>
        </div>
    );
}

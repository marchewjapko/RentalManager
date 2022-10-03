import * as React from 'react';
import {
    Box,
    Checkbox,
    Collapse,
    IconButton,
    InputAdornment,
    Stack,
    TableCell,
    TableRow,
    TextField
} from "@mui/material";
import KeyboardArrowUpIcon from "@mui/icons-material/KeyboardArrowUp";
import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import dayjs from 'dayjs';
import 'dayjs/locale/pl';
import {AdapterDayjs} from "@mui/x-date-pickers/AdapterDayjs";
import {DatePicker, LocalizationProvider} from "@mui/x-date-pickers";

export default function ClientTableRow({row, handleEditClick, handleDeleteClick, isCheckable, handleCheckboxChange, checkedRow}) {
    const [openDetails, setOpenDetails] = React.useState(false);

    const handleOpenDetails = () => {
        setOpenDetails(!openDetails);
    }

    return (<React.Fragment>
        <TableRow sx={{'& > *': {borderBottom: 'unset'}}}>
            <TableCell className={"ClientTableDetailsCell"}>
                <Stack direction={"row"}>
                    {isCheckable ? <Checkbox onChange={() => handleCheckboxChange(row)} checked={row.id === checkedRow}/> : null}
                    <IconButton size="small" onClick={handleOpenDetails}
                                sx={{width: "30px", height: "30px", marginTop: 'auto', marginBottom: "auto"}}>
                        {openDetails ? <KeyboardArrowUpIcon fontSize="small"/> :
                            <KeyboardArrowDownIcon fontSize="small"/>}
                    </IconButton>
                </Stack>
            </TableCell>
            <TableCell component="th" scope="row" className={"ClientTableNameCell"}>
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
        <TableRow>
            <TableCell style={{paddingBottom: 0, paddingTop: 0}} colSpan={4}>
                <Collapse in={openDetails} timeout="auto" unmountOnExit>
                    <Box sx={{margin: 1}}>
                        <TextField
                            margin="dense"
                            label="Phone number"
                            fullWidth
                            variant="outlined"
                            value={row.phone}
                            InputProps={{
                                readOnly: true, startAdornment: <InputAdornment position="start">+48</InputAdornment>
                            }}
                        />
                        <TextField
                            margin="dense"
                            label="Email"
                            fullWidth
                            variant="outlined"
                            value={row.email}
                            InputProps={{
                                readOnly: true
                            }}
                            disabled={row.email.trim().length === 0}
                        />
                        <TextField
                            margin="dense"
                            label="ID card"
                            fullWidth
                            variant="outlined"
                            value={row.idCard}
                            InputProps={{
                                readOnly: true
                            }}
                            disabled={row.idCard.trim().length === 0}
                        />
                        <TextField
                            margin="dense"
                            label="Address"
                            fullWidth
                            variant="outlined"
                            value={row.city + ' ' + row.street + ' ' + row.streetNumber}
                            InputProps={{
                                readOnly: true
                            }}
                        />
                        <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale={'pl'}>
                            <DatePicker
                                label="Date added"
                                disableOpenPicker={true}
                                className={"DialogDatePicker ClientHiddenRowDatePicker"}
                                value={dayjs(row.dateAdded)}
                                onChange={() => null}
                                minDate={dayjs(row.dateAdded)}
                                maxDate={dayjs(row.dateAdded)}
                                renderInput={(params) => <TextField {...params} />}
                            />
                        </LocalizationProvider>
                    </Box>
                </Collapse>
            </TableCell>
        </TableRow>
    </React.Fragment>);
}
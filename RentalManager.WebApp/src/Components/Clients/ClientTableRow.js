import * as React from 'react';
import ClientsUpdateDialog from "./ClientsUpdateDialog";
import ClientsDeleteDialog from "./ClientsDeleteDialog";
import {Box, Collapse, IconButton, TableCell, TableRow, TextField} from "@mui/material";
import KeyboardArrowUpIcon from "@mui/icons-material/KeyboardArrowUp";
import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";

export default function ClientTableRow(props) {
    const { row } = props;
    const [open, setOpen] = React.useState(false);

    const [showEditDialog, setShowEditDialog] = React.useState(false);
    const [showDeleteDialog, setShowDeleteDialog] = React.useState(false);
    const [ClientEditName, setClientEditName] = React.useState("");
    const [ClientEditSurname, setClientEditSurname] = React.useState("");

    const handleEditClick = (isShown, ClientId, ClientName, ClientSurname) => {
        setClientEditName(ClientName)
        setClientEditSurname(ClientSurname)
        setShowEditDialog(isShown)
    }

    const handleDeleteClick = (isShown, ClientId, ClientName, ClientSurname) => {
        setClientEditName(ClientName)
        setClientEditSurname(ClientSurname)
        setShowDeleteDialog(isShown)
    }

    return (
        <React.Fragment>
            {showEditDialog ? <ClientsUpdateDialog handleEditClick={handleEditClick} clientEditName={ClientEditName}
                                                   clientEditSurname={ClientEditSurname}/> : null}
            {showDeleteDialog ?
                <ClientsDeleteDialog handleDeleteClick={handleDeleteClick} clientEditName={ClientEditName}
                                     clientEditSurname={ClientEditSurname}/> : null}
            <TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
                <TableCell>
                    <IconButton
                        aria-label="expand row"
                        size="small"
                        onClick={() => setOpen(!open)}
                    >
                        {open ? <KeyboardArrowUpIcon fontSize="small" /> : <KeyboardArrowDownIcon fontSize="small" />}
                    </IconButton>
                </TableCell>
                <TableCell component="th" scope="row">
                    {row.name}
                </TableCell>
                <TableCell align="right">
                    {row.surname}
                </TableCell>
                <TableCell align="right" className={"TableClientsColumnActions"}>
                    <Box>
                        <IconButton aria-label="delete" size="small"
                                    onClick={() => handleEditClick(true, row.id, row.name, row.surname)}>
                            <EditIcon fontSize="small"/>
                        </IconButton>
                        <IconButton aria-label="delete" size="small" color={"error"}
                                    onClick={() => handleDeleteClick(true, row.id, row.name, row.surname)}>
                            <DeleteIcon fontSize="small"/>
                        </IconButton>
                    </Box>
                </TableCell>
            </TableRow>
            <TableRow>
                <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={4}>
                    <Collapse in={open} timeout="auto" unmountOnExit>
                        <Box sx={{ margin: 1 }}>
                            <TextField
                                margin="dense"
                                id="name"
                                label="Phone number"
                                type="email"
                                fullWidth
                                variant="outlined"
                                value={row.phone}
                                InputProps={{
                                    readOnly: true
                                }}
                            />
                            <TextField
                                margin="dense"
                                id="name"
                                label="Email"
                                type="email"
                                fullWidth
                                variant="outlined"
                                value={row.email}
                                InputProps={{
                                    readOnly: true
                                }}
                            />
                            <TextField
                                margin="dense"
                                id="name"
                                label="ID card"
                                type="email"
                                fullWidth
                                variant="outlined"
                                value={row.idCard}
                                InputProps={{
                                    readOnly: true
                                }}
                            />
                            <TextField
                                margin="dense"
                                id="name"
                                label="Address"
                                type="email"
                                fullWidth
                                variant="outlined"
                                value={row.city + ' ' + row.street + ' ' + row.streetNumber}
                                InputProps={{
                                    readOnly: true
                                }}
                            />
                        </Box>
                    </Collapse>
                </TableCell>
            </TableRow>
        </React.Fragment>
    );
}
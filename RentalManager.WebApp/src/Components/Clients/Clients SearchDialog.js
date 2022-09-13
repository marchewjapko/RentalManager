import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    IconButton,
    InputAdornment, Stack,
    TextField,
    Zoom
} from "@mui/material";
import CancelIcon from '@mui/icons-material/Cancel';
import DeleteIcon from '@mui/icons-material/Delete';
import * as React from 'react';

export default function ClientsSearchDialog ({handleDeleteClick, clientEditName, clientEditSurname}) {
    return (
        <Dialog
            open={true}
            keepMounted
            maxWidth={"lg"}
            onClose={() => handleDeleteClick(false)}
            aria-describedby="alert-dialog-slide-description"
        >
            <DialogTitle>{"Search client"}</DialogTitle>
            <DialogContent>
                <TextField
                    margin="dense"
                    id="name"
                    label="Name"
                    type="email"
                    fullWidth
                    variant="outlined"
                    value={clientEditName}
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    margin="dense"
                    id="name"
                    label="Surname"
                    type="email"
                    fullWidth
                    variant="outlined"
                    value={clientEditSurname}
                    InputProps={{
                        readOnly: true
                    }}
                />
            </DialogContent>
            <Stack direction="row" justifyContent="space-between" className={"DialogStack"}>
                <Button variant="contained" color={"error"} size="large" endIcon={<DeleteIcon />} onClick={() => handleDeleteClick(false)} className={"ClientsDialogButton"}>
                    Delete
                </Button>
                <Button variant="outlined" color={"primary"} size="large" endIcon={<CancelIcon />} onClick={() => handleDeleteClick(false)} className={"ClientsDialogButton"}>
                    Cancel
                </Button>
            </Stack>
        </Dialog>
    );
}
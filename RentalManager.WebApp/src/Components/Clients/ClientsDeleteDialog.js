import {
    Button,
    Dialog,
    DialogContent,
    DialogTitle,
    Stack,
    TextField
} from "@mui/material";
import CancelIcon from '@mui/icons-material/Cancel';
import DeleteIcon from '@mui/icons-material/Delete';
import * as React from 'react';

export default function ClientsDeleteDialog ({handleDeleteClick, clientEditName, clientEditSurname}) {
    return (
        <Dialog
            open={true}
            keepMounted
            maxWidth={"lg"}
            onClose={() => handleDeleteClick(false)}
        >
            <DialogTitle>{"Delete client"}</DialogTitle>
            <DialogContent>
                <TextField
                    margin="dense"
                    label="Name"
                    fullWidth
                    variant="outlined"
                    value={clientEditName}
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    margin="dense"
                    label="Surname"
                    fullWidth
                    variant="outlined"
                    value={clientEditSurname}
                    className={"DialogLowerTextField"}
                    InputProps={{
                        readOnly: true
                    }}
                />
            </DialogContent>
            <Stack direction="row" justifyContent="space-between" className={"DialogStack"}>
                <Button variant="contained" color={"error"} size="large" endIcon={<DeleteIcon />} onClick={() => handleDeleteClick(false)} className={"DialogButton"}>
                    Delete
                </Button>
                <Button variant="outlined" color={"primary"} size="large" endIcon={<CancelIcon />} onClick={() => handleDeleteClick(false)} className={"DialogButton"}>
                    Cancel
                </Button>
            </Stack>
        </Dialog>
    );
}
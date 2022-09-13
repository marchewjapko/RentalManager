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

export default function EmployeesDeleteDialog ({handleDeleteClick, employeeName, employeeSurname}) {
    return (
        <Dialog
            open={true}
            keepMounted
            maxWidth={"lg"}
            onClose={() => handleDeleteClick(false)}
        >
            <DialogTitle>{"Delete employee"}</DialogTitle>
            <DialogContent>
                <TextField
                    margin="dense"
                    label="Name"
                    fullWidth
                    variant="outlined"
                    value={employeeName}
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    margin="dense"
                    label="Surname"
                    fullWidth
                    variant="outlined"
                    className={"DialogLowerTextField"}
                    value={employeeSurname}
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
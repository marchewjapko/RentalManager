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

export default function RentalEquipmentDeleteDialog ({handleDeleteClick, equipmentName, equipmentPrice}) {
    return (
        <Dialog
            open={true}
            keepMounted
            maxWidth={"lg"}
            onClose={() => handleDeleteClick(false)}
            aria-describedby="alert-dialog-slide-description"
        >
            <DialogTitle>{"Delete rental equipment"}</DialogTitle>
            <DialogContent>
                <TextField
                    margin="dense"
                    id="name"
                    label="Equipment name"
                    type="email"
                    fullWidth
                    variant="outlined"
                    value={equipmentName}
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    margin="dense"
                    id="name"
                    label="Monthly price"
                    type="email"
                    fullWidth
                    variant="outlined"
                    value={equipmentPrice}
                    className={"RentalEquipmentLowerTextField"}
                    InputProps={{
                        endAdornment: <InputAdornment position="start">zł</InputAdornment>,
                        readOnly: true
                    }}
                />
            </DialogContent>
            <Stack direction="row" justifyContent="space-between" className={"RentalEquipmentDialogStack"}>
                <Button variant="contained" color={"error"} size="large" endIcon={<DeleteIcon />} onClick={() => handleDeleteClick(false)} className={"RenalEquipmentDialogButton"}>
                    Delete
                </Button>
                <Button variant="outlined" color={"primary"} size="large" endIcon={<CancelIcon />} onClick={() => handleDeleteClick(false)} className={"RenalEquipmentDialogButton"}>
                    Cancel
                </Button>
            </Stack>
        </Dialog>
    );
}
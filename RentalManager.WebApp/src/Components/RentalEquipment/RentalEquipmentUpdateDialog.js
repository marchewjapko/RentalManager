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
import DoneIcon from '@mui/icons-material/Done';
import * as React from 'react';

export default function RentalEquipmentUpdateDialog ({handleEditClick, equipmentName, equipmentPrice}) {
    const [equipmentDialogName, setEquipmentDialogName] = React.useState(equipmentName);
    const [equipmentDialogPrice, setEquipmentDialogPrice] = React.useState(equipmentPrice);

    const handleChangeName = (event) => {
        setEquipmentDialogName(event.target.value);
    };

    const handleChangePrice = (event) => {
        setEquipmentDialogPrice(event.target.value.replace(/\D/g, ""));
    };

    return (
        <Dialog
            open={true}
            keepMounted
            maxWidth={"lg"}
            onClose={() => handleEditClick(false)}
            aria-describedby="alert-dialog-slide-description"
        >
            <DialogTitle>{"Edit rental equipment"}</DialogTitle>
            <DialogContent>
                <TextField
                    margin="dense"
                    label="Equipment name"
                    fullWidth
                    variant="outlined"
                    value={equipmentDialogName}
                    onChange={handleChangeName}
                />
                <TextField
                    margin="dense"
                    label="Monthly price"
                    fullWidth
                    variant="outlined"
                    value={equipmentDialogPrice}
                    className={"RentalEquipmentLowerTextField"}
                    InputProps={{
                        endAdornment: <InputAdornment position="start">zł</InputAdornment>
                    }}
                    onChange={handleChangePrice}
                />
            </DialogContent>
            <Stack direction="row" justifyContent="space-between" className={"RentalEquipmentDialogStack"}>
                <Button variant="contained" color={"success"} size="large" endIcon={<DoneIcon />} onClick={() => handleEditClick(false)} className={"RenalEquipmentDialogButton"}>
                    Save
                </Button>
                <Button variant="outlined" color={"primary"} size="large" endIcon={<CancelIcon />} onClick={() => handleEditClick(false)} className={"RenalEquipmentDialogButton"}>
                    Cancel
                </Button>
            </Stack>
        </Dialog>
    );
}
import {
    Backdrop,
    Button, CircularProgress,
    DialogContent,
    InputAdornment, Stack,
    TextField,
} from "@mui/material";
import CancelIcon from '@mui/icons-material/Cancel';
import DoneIcon from '@mui/icons-material/Done';
import * as React from 'react';
import {updateRentalEquipment} from "../../Actions/RentalEquipmentActions";

export default function RentalEquipmentUpdateDialog ({handleCancelDialog, rentalEquipment, handleDialogSuccess}) {
    const [rentalEquipmentDialog, setRentalEquipmentDialog] = React.useState(rentalEquipment);
    const [isLoading, setIsLoading] = React.useState(false)

    const handleChangeName = (event) => {
        setRentalEquipmentDialog({
            ...rentalEquipmentDialog,
            name: event.target.value
        });
    };

    const handleChangePrice = (event) => {
        setRentalEquipmentDialog({
            ...rentalEquipmentDialog,
            monthlyPrice: event.target.value.replace(/\D/g, "")
        });
    };

    const handleSave = async () => {
        setIsLoading(true);
        await updateRentalEquipment(rentalEquipment);
        setIsLoading(false);
        handleDialogSuccess("edit")
    }

    return (
        <div>
            <DialogContent>
                <Backdrop
                    sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
                    open={isLoading}
                >
                    <CircularProgress/>
                </Backdrop>
                <TextField
                    margin="dense"
                    label="Equipment name"
                    fullWidth
                    variant="outlined"
                    value={rentalEquipmentDialog.name}
                    onChange={handleChangeName}
                />
                <TextField
                    margin="dense"
                    label="Monthly price"
                    fullWidth
                    variant="outlined"
                    value={rentalEquipmentDialog.monthlyPrice}
                    className={"DialogLowerTextField"}
                    InputProps={{
                        endAdornment: <InputAdornment position="start">zł</InputAdornment>
                    }}
                    onChange={handleChangePrice}
                />
            </DialogContent>
            <Stack direction="row" justifyContent="space-between" className={"DialogStack"}>
                <Button variant="contained" color={"success"} size="large" endIcon={<DoneIcon />} onClick={handleSave} className={"DialogButton"}>
                    Save
                </Button>
                <Button variant="outlined" color={"primary"} size="large" endIcon={<CancelIcon />} onClick={() => handleCancelDialog(false)} className={"DialogButton"}>
                    Cancel
                </Button>
            </Stack>
        </div>
    );
}
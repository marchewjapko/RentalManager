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
import ValidateClient from "../../Actions/ValidateClient";
import ValidateRentalEquipment from "../../Actions/ValidateRentalEquipment";
import {updateClient} from "../../Actions/ClientActions";

export default function RentalEquipmentUpdateDialog ({handleCancelDialog, rentalEquipment, handleDialogSuccess}) {
    const [rentalEquipmentDialog, setRentalEquipmentDialog] = React.useState(rentalEquipment);
    const [isLoading, setIsLoading] = React.useState(false)
    const [errorCodes, setErrorCodes] = React.useState('')

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
        const res = ValidateRentalEquipment(rentalEquipmentDialog);
        if (res.length === 0) {
            setIsLoading(true);
            await updateRentalEquipment(rentalEquipmentDialog);
            setIsLoading(false);
            handleDialogSuccess("edit")
        } else {
            setErrorCodes(res)
        }
    }

    const validateForm = () => {
        const res = ValidateRentalEquipment(rentalEquipmentDialog);
        setErrorCodes(res)
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
                    onBlur={validateForm}
                    error={errorCodes.includes("noName")}
                    helperText="Required"
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
                    onBlur={validateForm}
                    error={errorCodes.includes("noPrice")}
                    helperText="Required"
                />
            </DialogContent>
            <Stack direction="row" justifyContent="space-between" className={"DialogStack"}>
                <Button variant="contained" color={"success"} size="large" endIcon={<DoneIcon />} onClick={handleSave} className={"DialogButton"} disabled={errorCodes.length !== 0}>
                    Save
                </Button>
                <Button variant="outlined" color={"primary"} size="large" endIcon={<CancelIcon />} onClick={() => handleCancelDialog(false)} className={"DialogButton"}>
                    Cancel
                </Button>
            </Stack>
        </div>
    );
}
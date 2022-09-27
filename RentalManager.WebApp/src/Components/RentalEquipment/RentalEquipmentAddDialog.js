import {Backdrop, Button, CircularProgress, DialogContent, InputAdornment, Stack, TextField,} from "@mui/material";
import CancelIcon from '@mui/icons-material/Cancel';
import DoneIcon from '@mui/icons-material/Done';
import * as React from 'react';
import {addRentalEquipment} from "../../Actions/RentalEquipmentActions";
import ValidateRentalEquipment from "../../Actions/ValidateRentalEquipment";

export default function RentalEquipmentAddDialog({handleCancelDialog, handleDialogSuccess}) {
    const [rentalEquipmentDialog, setRentalEquipmentDialog] = React.useState({id: 0, name: "", monthlyPrice: "", dateAdded: "0000-01-01"});
    const [isLoading, setIsLoading] = React.useState(false)
    const [validationState, setValidationState] = React.useState({name: false, price: false})

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
        await addRentalEquipment(rentalEquipmentDialog);
        setIsLoading(false);
        handleDialogSuccess("edit")
    }

    const validateName = () => {
        const res = ValidateRentalEquipment(rentalEquipmentDialog);
        if(res.includes("noName")) {
            setValidationState({
                ...validationState,
                name: true
            })
        } else {
            setValidationState({
                ...validationState,
                name: false
            })
        }
    }

    const validatePrice = () => {
        const res = ValidateRentalEquipment(rentalEquipmentDialog);
        if(res.includes("noPrice")) {
            setValidationState({
                ...validationState,
                price: true
            })
        } else {
            setValidationState({
                ...validationState,
                price: false
            })
        }
    }

    return (
        <div>
            <DialogContent>
                <Backdrop
                    sx={{color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1}}
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
                    onBlur={validateName}
                    error={validationState.name}
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
                    onBlur={validatePrice}
                    error={validationState.price}
                    helperText="Required"
                />
            </DialogContent>
            <Stack direction="row" justifyContent="space-between" className={"DialogStack"}>
                <Button variant="contained" color={"success"} size="large" endIcon={<DoneIcon/>} onClick={handleSave}
                        className={"DialogButton"} disabled={rentalEquipmentDialog.monthlyPrice.trim().length === 0 || rentalEquipmentDialog.name.trim().length === 0}>
                    Save
                </Button>
                <Button variant="outlined" color={"primary"} size="large" endIcon={<CancelIcon/>}
                        onClick={() => handleCancelDialog(false)} className={"DialogButton"}>
                    Cancel
                </Button>
            </Stack>
        </div>
    );
}
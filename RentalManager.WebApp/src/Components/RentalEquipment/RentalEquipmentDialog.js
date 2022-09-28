import * as React from 'react';
import ValidateRentalEquipment from "../../Actions/ValidateRentalEquipment";
import {addRentalEquipment, deleteRentalEquipment, updateRentalEquipment} from "../../Actions/RentalEquipmentActions";
import {Backdrop, Button, CircularProgress, DialogContent, InputAdornment, Stack, TextField} from "@mui/material";
import DoneIcon from "@mui/icons-material/Done";
import CancelIcon from "@mui/icons-material/Cancel";
import DeleteIcon from "@mui/icons-material/Delete";

export default function RentalEquipmentDialog({handleCancelDialog, rentalEquipment, handleDialogSuccess, mode}) {
    const [rentalEquipmentDialog, setRentalEquipmentDialog] = React.useState(rentalEquipment);
    const [isLoading, setIsLoading] = React.useState(false)
    const [validationState, setValidationState] = React.useState({name: false, price: false})

    const handleChangeName = (event) => {
        setRentalEquipmentDialog({
            ...rentalEquipmentDialog, name: event.target.value
        });
    };

    const handleChangePrice = (event) => {
        setRentalEquipmentDialog({
            ...rentalEquipmentDialog, monthlyPrice: event.target.value.replace(/\D/g, "")
        });
    };

    const handleSave = async () => {
        const res = ValidateRentalEquipment(rentalEquipmentDialog);
        if (res.length === 0) {
            setIsLoading(true);
            switch (mode) {
                case 'delete':
                    await deleteRentalEquipment(rentalEquipment.id);
                    break;
                case 'update':
                    await updateRentalEquipment(rentalEquipmentDialog);
                    break;
                case 'post':
                    await addRentalEquipment(rentalEquipmentDialog);
                    break;
            }
            setIsLoading(false);
            handleDialogSuccess(mode)
        }
    }

    const validateName = () => {
        const res = ValidateRentalEquipment(rentalEquipmentDialog);
        if (res.includes("noName")) {
            setValidationState({
                ...validationState, name: true
            })
        } else {
            setValidationState({
                ...validationState, name: false
            })
        }
    }

    const validatePrice = () => {
        const res = ValidateRentalEquipment(rentalEquipmentDialog);
        if (res.includes("noPrice")) {
            setValidationState({
                ...validationState, price: true
            })
        } else {
            setValidationState({
                ...validationState, price: false
            })
        }
    }

    return (<div>
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
                InputProps={mode === 'delete' ? {readOnly: true} : null}
            />
            <TextField
                margin="dense"
                label="Monthly price"
                fullWidth
                variant="outlined"
                value={rentalEquipmentDialog.monthlyPrice}
                className={"DialogLowerTextField"}
                InputProps={mode === 'delete' ? {
                    readOnly: true, endAdornment: <InputAdornment position="start">zł</InputAdornment>
                } : {endAdornment: <InputAdornment position="start">zł</InputAdornment>}}
                onChange={handleChangePrice}
                onBlur={validatePrice}
                error={validationState.price}
                helperText="Required"
            />
        </DialogContent>
        <Stack direction="row" justifyContent="space-between" className={"DialogStack"}>
            {mode === 'delete' ? (<Button variant="contained" color={"error"} size="large" endIcon={<DeleteIcon/>}
                                          onClick={handleSave}
                                          className={"DialogButton"}>
                Delete
            </Button>) : (<Button variant="contained" color={"success"} size="large" endIcon={<DoneIcon/>}
                                  onClick={handleSave}
                                  className={"DialogButton"}
                                  disabled={ValidateRentalEquipment(rentalEquipmentDialog).length !== 0}>
                Save
            </Button>)}
            <Button variant="outlined" color={"primary"} size="large" endIcon={<CancelIcon/>}
                    onClick={() => handleCancelDialog(false)} className={"DialogButton"}>
                Cancel
            </Button>
        </Stack>
    </div>);
}
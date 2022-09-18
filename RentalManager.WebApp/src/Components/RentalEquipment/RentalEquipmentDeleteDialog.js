import {Backdrop, Button, CircularProgress, DialogContent, InputAdornment, Stack, TextField,} from "@mui/material";
import CancelIcon from '@mui/icons-material/Cancel';
import DeleteIcon from '@mui/icons-material/Delete';
import * as React from 'react';
import {deleteRentalEquipment} from "../../Actions/RentalEquipmentActions";

export default function RentalEquipmentDeleteDialog({handleCancelDialog, rentalEquipment, handleDialogSuccess}) {
    const [isLoading, setIsLoading] = React.useState(false)

    const handleDelete = async () => {
        setIsLoading(true);
        await deleteRentalEquipment(rentalEquipment.id);
        setIsLoading(false);
        handleDialogSuccess("delete")
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
                    id="name"
                    label="Equipment name"
                    type="email"
                    fullWidth
                    variant="outlined"
                    value={rentalEquipment.name}
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
                    value={rentalEquipment.monthlyPrice}
                    className={"DialogLowerTextField"}
                    InputProps={{
                        endAdornment: <InputAdornment position="start">zł</InputAdornment>,
                        readOnly: true
                    }}
                />
            </DialogContent>
            <Stack direction="row" justifyContent="space-between" className={"DialogStack"}>
                <Button variant="contained" color={"error"} size="large" endIcon={<DeleteIcon/>} onClick={handleDelete}
                        className={"DialogButton"}>
                    Delete
                </Button>
                <Button variant="outlined" color={"primary"} size="large" endIcon={<CancelIcon/>}
                        onClick={() => handleCancelDialog(false)} className={"DialogButton"}>
                    Cancel
                </Button>
            </Stack>
        </div>
    );
}
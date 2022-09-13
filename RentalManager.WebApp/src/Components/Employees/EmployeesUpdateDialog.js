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

export default function EmployeesUpdateDialog ({handleEditClick, employeeName, employeeSurname}) {
    const [equipmentDialogName, setEquipmentDialogName] = React.useState(employeeName);
    const [equipmentDialogSurname, setEquipmentDialogSurname] = React.useState(employeeSurname);

    const handleChangeName = (event) => {
        setEquipmentDialogName(event.target.value);
    };

    const handleChangeSurname = (event) => {
        setEquipmentDialogSurname(event.target.value);
    };

    return (
        <Dialog
            open={true}
            keepMounted
            maxWidth={"lg"}
            onClose={() => handleEditClick(false)}
        >
            <DialogTitle>{"Edit employee"}</DialogTitle>
            <DialogContent>
                <TextField
                    margin="dense"
                    label="Name"
                    fullWidth
                    variant="outlined"
                    value={equipmentDialogName}
                    onChange={handleChangeName}
                />
                <TextField
                    margin="dense"
                    label="Surname"
                    fullWidth
                    variant="outlined"
                    value={equipmentDialogSurname}
                    onChange={handleChangeSurname}
                />
            </DialogContent>
            <Stack direction="row" justifyContent="space-between" className={"DialogStack"}>
                <Button variant="contained" color={"success"} size="large" endIcon={<DoneIcon />} onClick={() => handleEditClick(false)} className={"DialogButton"}>
                    Save
                </Button>
                <Button variant="outlined" color={"primary"} size="large" endIcon={<CancelIcon />} onClick={() => handleEditClick(false)} className={"DialogButton"}>
                    Cancel
                </Button>
            </Stack>
        </Dialog>
    );
}
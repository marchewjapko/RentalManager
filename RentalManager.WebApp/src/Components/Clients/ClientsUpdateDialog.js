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

export default function ClientsUpdateDialog ({handleEditClick, clientEditName, clientEditSurname}) {
    const [ClientEditDialogName, setClientDialogEditName] = React.useState(clientEditName);
    const [ClientEditDialogSurname, setClientDialogEditSurname] = React.useState(clientEditSurname);

    const handleChangeName = (event) => {
        setClientDialogEditName(event.target.value);
    };

    const handleChangeSurname = (event) => {
        setClientDialogEditSurname(event.target.value.replace(/\D/g, ""));
    };

    return (
        <Dialog
            open={true}
            keepMounted
            maxWidth={"lg"}
            onClose={() => handleEditClick(false)}
            aria-describedby="alert-dialog-slide-description"
        >
            <DialogTitle>{"Edit client"}</DialogTitle>
            <DialogContent>
                <TextField
                    margin="dense"
                    label="Name"
                    fullWidth
                    variant="outlined"
                    value={ClientEditDialogName}
                    onChange={handleChangeName}
                />
                <TextField
                    margin="dense"
                    label="Surname"
                    fullWidth
                    variant="outlined"
                    value={ClientEditDialogSurname}
                    className={"DialogLowerTextField"}
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
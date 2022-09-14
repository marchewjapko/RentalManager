import {
    Backdrop,
    Button, CircularProgress,
    DialogContent,
    Stack,
    TextField
} from "@mui/material";
import CancelIcon from '@mui/icons-material/Cancel';
import DoneIcon from '@mui/icons-material/Done';
import * as React from 'react';
import {updateEmployee} from "../../Actions/EmployeeActions";

export default function EmployeesUpdateDialog ({handleCancelDialog, employee, handleDialogSuccess}) {
    const [employeeDialog, setEmployeeDialog] = React.useState(employee);
    const [isLoading, setIsLoading] = React.useState(false)

    const handleChangeName = (event) => {
        setEmployeeDialog({
            ...employeeDialog,
            name: event.target.value
        });
    };

    const handleChangeSurname = (event) => {
        setEmployeeDialog({
            ...employeeDialog,
            surname: event.target.value
        });
    };

    const handleSave = async () => {
        setIsLoading(true);
        await updateEmployee(employeeDialog);
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
                    label="Name"
                    fullWidth
                    variant="outlined"
                    value={employeeDialog.name}
                    onChange={handleChangeName}
                />
                <TextField
                    margin="dense"
                    label="Surname"
                    fullWidth
                    variant="outlined"
                    className={"DialogLowerTextField"}
                    value={employeeDialog.surname}
                    onChange={handleChangeSurname}
                />
            </DialogContent>
            <Stack direction="row" justifyContent="space-between" className={"DialogStack"}>
                <Button variant="contained" color={"success"} size="large" endIcon={<DoneIcon />} onClick={handleSave} className={"DialogButton"}>
                    Save
                </Button>
                <Button variant="outlined" color={"primary"} size="large" endIcon={<CancelIcon />} onClick={() => handleCancelDialog()} className={"DialogButton"}>
                    Cancel
                </Button>
            </Stack>
        </div>
    );
}
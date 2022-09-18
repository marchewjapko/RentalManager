import {Backdrop, Button, CircularProgress, DialogContent, Stack, TextField} from "@mui/material";
import CancelIcon from '@mui/icons-material/Cancel';
import DoneIcon from '@mui/icons-material/Done';
import * as React from 'react';
import {updateEmployee} from "../../Actions/EmployeeActions";
import ValidateEmployee from "../../Actions/ValidateEmployee";

export default function EmployeesUpdateDialog({handleCancelDialog, employee, handleDialogSuccess}) {
    const [employeeDialog, setEmployeeDialog] = React.useState(employee);
    const [isLoading, setIsLoading] = React.useState(false)
    const [errorCodes, setErrorCodes] = React.useState('')

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

    const validateForm = () => {
        const res = ValidateEmployee(employeeDialog);
        setErrorCodes(res)
    }

    const handleSave = async () => {
        const res = ValidateEmployee(employeeDialog);
        if (res.length === 0) {
            setIsLoading(true);
            await updateEmployee(employeeDialog);
            setIsLoading(false);
            handleDialogSuccess("edit")
        } else {
            setErrorCodes(res)
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
                    label="Name"
                    fullWidth
                    variant="outlined"
                    value={employeeDialog.name}
                    onChange={handleChangeName}
                    onBlur={validateForm}
                    error={errorCodes.includes("noName")}
                    helperText="Required"
                />
                <TextField
                    margin="dense"
                    label="Surname"
                    fullWidth
                    variant="outlined"
                    className={"DialogLowerTextField"}
                    value={employeeDialog.surname}
                    onChange={handleChangeSurname}
                    onBlur={validateForm}
                    error={errorCodes.includes("noSurname")}
                    helperText="Required"
                />
            </DialogContent>
            <Stack direction="row" justifyContent="space-between" className={"DialogStack"}>
                <Button variant="contained" color={"success"} size="large" endIcon={<DoneIcon/>} onClick={handleSave}
                        className={"DialogButton"} disabled={errorCodes.length !== 0}>
                    Save
                </Button>
                <Button variant="outlined" color={"primary"} size="large" endIcon={<CancelIcon/>}
                        onClick={() => handleCancelDialog()} className={"DialogButton"}>
                    Cancel
                </Button>
            </Stack>
        </div>
    );
}
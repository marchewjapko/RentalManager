import * as React from 'react';
import ValidateEmployee from "../../Actions/ValidateEmployee";
import {addEmployee, deleteEmployee, updateEmployee} from "../../Actions/EmployeeActions";
import {Backdrop, Button, CircularProgress, DialogContent, Stack, TextField} from "@mui/material";
import DoneIcon from "@mui/icons-material/Done";
import CancelIcon from "@mui/icons-material/Cancel";
import DeleteIcon from "@mui/icons-material/Delete";

export default function EmployeeDialog({handleCancelDialog, employee, handleDialogSuccess, mode}) {
    const [employeeDialog, setEmployeeDialog] = React.useState(employee);
    const [isLoading, setIsLoading] = React.useState(false)
    const [validationState, setValidationState] = React.useState({name: false, surname: false})

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

    const validateName = () => {
        const res = ValidateEmployee(employeeDialog);
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

    const validateSurname = () => {
        const res = ValidateEmployee(employeeDialog);
        if (res.includes("noSurname")) {
            setValidationState({
                ...validationState, surname: true
            })
        } else {
            setValidationState({
                ...validationState, surname: false
            })
        }
    }

    const handleSave = async () => {
        const res = ValidateEmployee(employeeDialog);
        if (res.length === 0) {
            setIsLoading(true);
            switch (mode) {
                case 'delete':
                    await deleteEmployee(employeeDialog.id);
                    break;
                case 'update':
                    await updateEmployee(employeeDialog);
                    break;
                case 'post':
                    await addEmployee(employeeDialog);
                    break;
            }
            setIsLoading(false);
            handleDialogSuccess(mode)
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
                    onBlur={validateName}
                    error={validationState.name}
                    helperText="Required"
                    InputProps={mode === 'delete' ? {readOnly: true} : null}
                />
                <TextField
                    margin="dense"
                    label="Surname"
                    fullWidth
                    variant="outlined"
                    className={"DialogLowerTextField"}
                    value={employeeDialog.surname}
                    onChange={handleChangeSurname}
                    onBlur={validateSurname}
                    error={validationState.surname}
                    helperText="Required"
                    InputProps={mode === 'delete' ? {readOnly: true} : null}
                />
            </DialogContent>
            <Stack direction="row" justifyContent="space-between" className={"DialogStack"}>
                {mode === 'delete' ? (
                    <Button variant="contained" color={"error"} size="large" endIcon={<DeleteIcon/>} onClick={handleSave}
                            className={"DialogButton"}>
                        Delete
                    </Button>) : (
                    <Button variant="contained" color={"success"} size="large" endIcon={<DoneIcon/>} onClick={handleSave}
                            className={"DialogButton"}
                            disabled={ValidateEmployee(employeeDialog).length !== 0}>
                        Save
                    </Button>)}
                <Button variant="outlined" color={"primary"} size="large" endIcon={<CancelIcon/>}
                        onClick={() => handleCancelDialog()} className={"DialogButton"}>
                    Cancel
                </Button>
            </Stack>
        </div>
    );
}
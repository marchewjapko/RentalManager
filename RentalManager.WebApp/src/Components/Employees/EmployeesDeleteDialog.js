import {
    Backdrop,
    Button, CircularProgress,
    DialogContent,
    Stack,
    TextField,
} from "@mui/material";
import CancelIcon from '@mui/icons-material/Cancel';
import DeleteIcon from '@mui/icons-material/Delete';
import * as React from 'react';
import {deleteEmployee} from "../../Actions/EmployeeActions";

export default function EmployeesDeleteDialog ({handleCancelDialog, employee, handleDialogSuccess}) {
    const [isLoading, setIsLoading] = React.useState(false)

    const handleDelete = async () => {
        setIsLoading(true);
        await deleteEmployee(employee.id);
        setIsLoading(false);
        handleDialogSuccess("delete")
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
                    value={employee.name}
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    margin="dense"
                    label="Surname"
                    fullWidth
                    variant="outlined"
                    className={"DialogLowerTextField"}
                    value={employee.surname}
                    InputProps={{
                        readOnly: true
                    }}
                />
            </DialogContent>
            <Stack direction="row" justifyContent="space-between" className={"DialogStack"}>
                <Button variant="contained" color={"error"} size="large" endIcon={<DeleteIcon />} onClick={handleDelete} className={"DialogButton"}>
                    Delete
                </Button>
                <Button variant="outlined" color={"primary"} size="large" endIcon={<CancelIcon />} onClick={() => handleCancelDialog()} className={"DialogButton"}>
                    Cancel
                </Button>
            </Stack>
        </div>
    );
}
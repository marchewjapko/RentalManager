import Grid from "@mui/material/Unstable_Grid2";
import {
    DialogContent, Divider, InputAdornment,
    Stack,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableRow,
    TextField,
    Typography
} from "@mui/material";
import EngineeringIcon from "@mui/icons-material/Engineering";
import ConstructionIcon from "@mui/icons-material/Construction";
import * as React from 'react';
import EmployeesSelect from "../Employees/EmployeesSelect";
import RentalEquipmentSelect from "../RentalEquipment/RentalEquipmentSelect";
import AttachMoneyIcon from '@mui/icons-material/AttachMoney';

export default function RentalAgreementDialogAgreementTab({mode, agreement, handleChangeEmployee, handleChangeEquipment}) {
    const [agreementDialog, setAgreementDialog] = React.useState(agreement);
    console.log(agreement)

    const handleChange = (event) => {
        const newAgreement = {
            ...agreementDialog,
            [event.target.name]: event.target.value
        }
        setAgreementDialog(newAgreement);
    }

    const agreementDetailsInputs = () => {
        return (
            <div>
                <Divider/>
                <Stack direction={"row"} className={"ClientUpdateDialogStack"}>
                    <AttachMoneyIcon sx={{marginRight: 1, marginTop: "auto", marginBottom: "auto"}}/>
                    <Typography variant="h6" sx={{marginTop: "auto", marginBottom: "auto"}}>
                        Costs
                    </Typography>
                </Stack>
                <Grid container spacing={2}>
                    <Grid xs={12} md={6}>
                        <TextField
                            name="transportFrom"
                            margin="dense"
                            label="Transport from"
                            variant="outlined"
                            fullWidth
                            value={agreementDialog.transportFrom}
                            onChange={handleChange}
                            // onBlur={validateName}
                            // error={validationState.name}
                            helperText="Required"
                            InputProps={mode === 'delete' || mode === 'info' ? {
                                readOnly: true,
                                endAdornment: <InputAdornment position="start">zł</InputAdornment>
                            } : {endAdornment: <InputAdornment position="start">zł</InputAdornment>}}
                        />
                    </Grid>
                    <Grid xs={12} md={6}>
                        <TextField
                            name="transportTo"
                            margin="dense"
                            label="Transport to"
                            variant="outlined"
                            fullWidth
                            value={agreementDialog.transportTo ? agreementDialog.transportTo : ''}
                            onChange={handleChange}
                            // onBlur={validateName}
                            // error={validationState.name}
                            helperText="Required"
                            InputProps={mode === 'delete' || mode === 'info' ? {
                                readOnly: true,
                                endAdornment: <InputAdornment position="start">zł</InputAdornment>
                            } : {endAdornment: <InputAdornment position="start">zł</InputAdornment>}}
                        />
                    </Grid>
                    <Grid xs={12} md={6}>
                        <TextField
                            name="deposit"
                            margin="dense"
                            label="Deposit"
                            variant="outlined"
                            fullWidth
                            value={agreementDialog.deposit}
                            onChange={handleChange}
                            // onBlur={validateName}
                            // error={validationState.name}
                            helperText="Required"
                            InputProps={mode === 'delete' || mode === 'info' ? {
                                readOnly: true,
                                endAdornment: <InputAdornment position="start">zł</InputAdornment>
                            } : {endAdornment: <InputAdornment position="start">zł</InputAdornment>}}
                        />
                    </Grid>
                </Grid>
            </div>
        );
    }

    if(mode === 'info' || mode === 'delete') {
        return (
            <DialogContent>
                <Stack spacing={2}>
                    <Grid container spacing={2}>
                        <Grid xs={12} md={6}>
                            <div>
                                <Stack direction={"row"} className={"ClientUpdateDialogStack"} sx={{paddingLeft: "17px", marginBottom: "24px"}}>
                                    <EngineeringIcon sx={{marginRight: 1, marginTop: "auto", marginBottom: "auto"}}/>
                                    <Typography variant="h6" sx={{marginTop: "auto", marginBottom: "auto"}}>
                                        Employee
                                    </Typography>
                                </Stack>
                                <TextField
                                    margin="dense"
                                    label="Name & surname"
                                    fullWidth
                                    variant="outlined"
                                    value={agreement.employee.name + ' ' + agreement.employee.surname}
                                    InputProps={mode === 'delete' || mode === 'info' ? {readOnly: true} : null}
                                />
                            </div>
                        </Grid>
                        <Grid xs={12} md={6}>
                            <div>
                                <Stack direction={"row"} className={"ClientUpdateDialogStack"} sx={{paddingLeft: "17px", marginBottom: "24px"}}>
                                    <ConstructionIcon sx={{marginRight: 1, marginTop: "auto", marginBottom: "auto"}}/>
                                    <Typography variant="h6" sx={{marginTop: "auto", marginBottom: "auto"}}>
                                        Rental Equipment
                                    </Typography>
                                </Stack>
                                <TableContainer sx={{width: "100%"}}>
                                    <Table>
                                        <TableBody>
                                            {agreement.rentalEquipment.map((row) => (
                                                <TableRow key={row.name}>
                                                    <TableCell component="th" scope="row">
                                                        {row.name}
                                                    </TableCell>
                                                    <TableCell align="right">
                                                        {row.monthlyPrice + ' zł'}
                                                    </TableCell>
                                                </TableRow>
                                            ))}
                                        </TableBody>
                                    </Table>
                                </TableContainer>
                            </div>
                        </Grid>
                    </Grid>
                    {agreementDetailsInputs()}
                </Stack>
            </DialogContent>
        );
    }
    return (
        <DialogContent>
            <Stack spacing={2}>
                <Grid container spacing={2}>
                    <Grid xs={12} md={6}>
                        <EmployeesSelect selectedEmployee={agreement.employee} handleChangeEmployee={handleChangeEmployee}/>
                    </Grid>
                    <Grid xs={12} md={6}>
                        <RentalEquipmentSelect selectedEquipment={agreement.rentalEquipment} handleChangeEquipment={handleChangeEquipment}/>
                    </Grid>
                </Grid>
                {agreementDetailsInputs()}
            </Stack>
        </DialogContent>
    );
}
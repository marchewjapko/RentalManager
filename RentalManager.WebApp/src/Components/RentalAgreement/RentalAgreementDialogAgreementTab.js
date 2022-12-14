import Grid from "@mui/material/Unstable_Grid2";
import {
    DialogContent,
    Divider,
    InputAdornment,
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
import CalendarMonthIcon from '@mui/icons-material/CalendarMonth';
import {AdapterDayjs} from "@mui/x-date-pickers/AdapterDayjs";
import {DatePicker, LocalizationProvider} from "@mui/x-date-pickers";
import CommentIcon from '@mui/icons-material/Comment';

export default function RentalAgreementDialogAgreementTab({mode, agreement, setAgreement}) {
    const handleChange = (event) => {
        let newValue = event.target.value
        if (['transportTo', 'transportFrom', 'deposit'].includes(event.target.name)) {
            newValue = newValue.replace(/\D/g, "")
        }
        const newAgreement = {
            ...agreement,
            [event.target.name]: newValue
        }
        setAgreement(newAgreement);
    }

    const handleChangeDateAdded = (event) => {
        setAgreement({
            ...agreement,
            dateAdded: event
        })
    }

    const handleChangeValidUntil = (event) => {
        setAgreement({
            ...agreement,
            validUntil: event
        })
    }

    return (
        <DialogContent>
            <Stack spacing={2}>
                {mode === 'info' || mode === 'delete' ? (
                    <Grid container spacing={2}>
                        <Grid xs={12} md={6}>
                            <div>
                                <Stack direction={"row"} className={"ClientUpdateDialogStack"}
                                       sx={{paddingLeft: "17px", marginBottom: "24px"}}>
                                    <EngineeringIcon className={"DividerIcon"}/>
                                    <Typography variant="h6" className={"MarginTopBottomAuto"}>
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
                            <Stack direction={"row"} className={"ClientUpdateDialogStack"}
                                   sx={{paddingLeft: "17px", marginBottom: "24px"}}>
                                <ConstructionIcon className={"DividerIcon"}/>
                                <Typography variant="h6" className={"MarginTopBottomAuto"}>
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
                                                    {row.monthlyPrice + ' z??'}
                                                </TableCell>
                                            </TableRow>
                                        ))}
                                    </TableBody>
                                </Table>
                            </TableContainer>
                        </Grid>
                    </Grid>
                ) : (
                    <Grid container spacing={2} sx={{paddingLeft: "8px", paddingRight: "8px", paddingBottom: "8px"}}>
                        <Grid xs={12} md={6}>
                            <EmployeesSelect agreement={agreement}
                                             setAgreement={setAgreement}/>
                        </Grid>
                        <Grid xs={12} md={6}>
                            <RentalEquipmentSelect agreement={agreement}
                                                   setAgreement={setAgreement}/>
                        </Grid>
                    </Grid>
                )}
                <Divider/>
                <Stack direction={"row"} className={"DialogTopStack"}>
                    <AttachMoneyIcon className={"DividerIcon"}/>
                    <Typography variant="h6" className={"MarginTopBottomAuto"}>
                        Costs
                    </Typography>
                </Stack>
                <Grid container spacing={2}>
                    <Grid xs={12} md={6}>
                        <TextField
                            name="transportTo"
                            margin="dense"
                            label="Transport to"
                            variant="outlined"
                            fullWidth
                            value={agreement.transportTo ? agreement.transportTo : ''}
                            onChange={handleChange}
                            // onBlur={validateName}
                            // error={validationState.name}
                            helperText="Required"
                            InputProps={{
                                readOnly: mode === 'delete' || mode === 'info',
                                endAdornment: <InputAdornment position="start">z??</InputAdornment>
                            }}
                        />
                    </Grid>
                    <Grid xs={12} md={6}>
                        <TextField
                            name="transportFrom"
                            margin="dense"
                            label="Transport from"
                            variant="outlined"
                            fullWidth
                            value={agreement.transportFrom ? agreement.transportFrom : ''}
                            onChange={handleChange}
                            // onBlur={validateName}
                            // error={validationState.name}
                            InputProps={{
                                readOnly: mode === 'delete' || mode === 'info',
                                endAdornment: <InputAdornment position="start">z??</InputAdornment>
                            }}
                        />
                    </Grid>
                    <Grid xs={12} md={6}>
                        <TextField
                            name="deposit"
                            margin="dense"
                            label="Deposit"
                            variant="outlined"
                            fullWidth
                            value={agreement.deposit ? agreement.deposit : ''}
                            onChange={handleChange}
                            // onBlur={validateName}
                            // error={validationState.name}
                            helperText="Required"
                            InputProps={{
                                readOnly: mode === 'delete' || mode === 'info',
                                endAdornment: <InputAdornment position="start">z??</InputAdornment>
                            }}
                        />
                    </Grid>
                </Grid>
                <Divider/>
                <Stack direction={"row"} className={"DialogTopStack"}>
                    <CalendarMonthIcon className={"DividerIcon"}/>
                    <Typography variant="h6" className={"MarginTopBottomAuto"}>
                        Dates
                    </Typography>
                </Stack>
                <Grid container spacing={2}>
                    <Grid xs={6}>
                        <LocalizationProvider dateAdapter={AdapterDayjs}>
                            <DatePicker
                                name={"dateAdded"}
                                label="Date added"
                                value={agreement.dateAdded}
                                onChange={handleChangeDateAdded}
                                readOnly={mode === 'info' || mode === 'delete'}
                                renderInput={(params) => <TextField {...params} fullWidth helperText="Required"/>}
                            />
                        </LocalizationProvider>
                    </Grid>
                    <Grid xs={6}>
                        <LocalizationProvider dateAdapter={AdapterDayjs}>
                            <DatePicker
                                name={"validUntil"}
                                label="Valid until"
                                value={agreement.validUntil}
                                onChange={handleChangeValidUntil}
                                readOnly={mode === 'info' || mode === 'delete'}
                                renderInput={(params) => <TextField {...params} fullWidth helperText="Required"/>}
                            />
                        </LocalizationProvider>
                    </Grid>
                </Grid>
                <Divider/>
                <Stack direction={"row"} className={"DialogTopStack"}>
                    <CommentIcon className={"DividerIcon"}/>
                    <Typography variant="h6" className={"MarginTopBottomAuto"}>
                        Comment
                    </Typography>
                </Stack>
                <Grid container spacing={2}>
                    <Grid xs={12}>
                        <TextField
                            name="comment"
                            margin="dense"
                            label="Comment"
                            variant="outlined"
                            multiline
                            fullWidth
                            value={agreement.comment ? agreement.comment : ''}
                            onChange={handleChange}
                            // onBlur={validateName}
                            // error={validationState.name}
                            InputProps={{
                                readOnly: mode === 'delete' || mode === 'info'
                            }}
                        />
                    </Grid>
                </Grid>
            </Stack>
        </DialogContent>
    );
}
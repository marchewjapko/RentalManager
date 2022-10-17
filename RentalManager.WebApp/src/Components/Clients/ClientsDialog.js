import * as React from 'react';
import ValidateClient from "../../Actions/ValidateClient";
import {addClient, deleteClient, updateClient} from "../../Actions/ClientActions";
import {Scrollbars} from "react-custom-scrollbars-2";
import {
    Backdrop,
    Button,
    CircularProgress,
    DialogContent,
    Divider,
    InputAdornment,
    Stack,
    TextField,
    Typography
} from "@mui/material";
import PersonIcon from "@mui/icons-material/Person";
import Grid from "@mui/material/Unstable_Grid2";
import InputMask from "react-input-mask";
import ContactPhoneIcon from "@mui/icons-material/ContactPhone";
import HomeIcon from "@mui/icons-material/Home";
import DoneIcon from "@mui/icons-material/Done";
import CancelIcon from "@mui/icons-material/Cancel";
import ValidateEmployee from "../../Actions/ValidateEmployee";
import DeleteIcon from "@mui/icons-material/Delete";

export default function ClientsDialog({handleCancelDialog, client, setClient, handleDialogSuccess, mode, showDialogButtons}) {
    const [isLoading, setIsLoading] = React.useState(false)
    const [validationState, setValidationState] = React.useState({
        name: false,
        surname: false,
        idCard: false,
        phone: '',
        email: false,
        city: false,
        streetNumber: false
    })

    const handleChange = (event) => {
        const newClient = {
            ...client,
            [event.target.name]: event.target.value
        }
        setClient(newClient);
    }

    const validateName = () => {
        const res = ValidateClient(client);
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
        const res = ValidateClient(client);
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

    const validateIdCard = () => {
        const res = ValidateClient(client);
        if (res.includes("invalidIdCard")) {
            setValidationState({
                ...validationState, idCard: true
            })
        } else {
            setValidationState({
                ...validationState, idCard: false
            })
        }
    }

    const validatePhone = () => {
        const res = ValidateClient(client);
        if (res.includes("noPhone")) {
            setValidationState({
                ...validationState, phone: "noPhone"
            })
        } else if (res.includes("invalidPhone")) {
            setValidationState({
                ...validationState, phone: "invalidPhone"
            })
        } else {
            setValidationState({
                ...validationState, phone: ""
            })
        }
    }

    const validateEmail = () => {
        const res = ValidateClient(client);
        if (res.includes("invalidEmail")) {
            setValidationState({
                ...validationState, email: true
            })
        } else {
            setValidationState({
                ...validationState, email: false
            })
        }
    }

    const validateCity = () => {
        const res = ValidateClient(client);
        if (res.includes("noCity")) {
            setValidationState({
                ...validationState, city: true
            })
        } else {
            setValidationState({
                ...validationState, city: false
            })
        }
    }

    const validateStreetNumber = () => {
        const res = ValidateClient(client);
        if (res.includes("noStreetNumber")) {
            setValidationState({
                ...validationState, streetNumber: true
            })
        } else {
            setValidationState({
                ...validationState, streetNumber: false
            })
        }
    }

    const handleSave = async () => {
        const res = ValidateEmployee(client);
        if (res.length === 0) {
            setIsLoading(true);
            switch (mode) {
                case 'delete':
                    await deleteClient(client.id);
                    break;
                case 'update':
                    await updateClient(client);
                    break;
                case 'post':
                    await addClient(client);
                    break;
            }
            setIsLoading(false);
            handleDialogSuccess(mode, client)
        }
    }

    const handleReset = () => {
        setClient(client);
        setValidationState({
            name: false,
            surname: false,
            idCard: false,
            phone: '',
            email: false,
            city: false,
            streetNumber: false
        })
    }

    return (
        <div>
            <Scrollbars autoHeight={true} autoHeightMin={200} autoHeightMax={'57vh'} autoHide
                        autoHideTimeout={750} autoHideDuration={500}>
                <DialogContent>
                    <Backdrop
                        sx={{color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1}}
                        open={isLoading}
                    >
                        <CircularProgress/>
                    </Backdrop>
                    <Stack spacing={2}>
                        <Stack direction={"row"} className={"DialogTopStack"}>
                            <PersonIcon className={"DividerIcon"}/>
                            <Typography variant="h6" className={"MarginTopBottomAuto"}>
                                Personal information
                            </Typography>
                        </Stack>
                        <Grid container spacing={2}>
                            <Grid xs={5}>
                                <TextField
                                    name="name"
                                    margin="dense"
                                    label="Name"
                                    variant="outlined"
                                    fullWidth
                                    value={client.name}
                                    onChange={handleChange}
                                    onBlur={validateName}
                                    error={validationState.name}
                                    helperText="Required"
                                    InputProps={{readOnly: mode === 'delete' || mode === 'info'}}
                                />
                            </Grid>
                            <Grid xs={7}>
                                <TextField
                                    name="surname"
                                    margin="dense"
                                    label="Surname"
                                    variant="outlined"
                                    fullWidth
                                    value={client.surname}
                                    onChange={handleChange}
                                    onBlur={validateSurname}
                                    error={validationState.surname}
                                    helperText="Required"
                                    InputProps={{readOnly: mode === 'delete' || mode === 'info'}}
                                />
                            </Grid>
                            <Grid xs={5} md={5}>
                                <InputMask
                                    mask="aaa 999999"
                                    value={client.idCard}
                                    disabled={false}
                                    maskChar=" "
                                    onChange={handleChange}
                                    onBlur={validateIdCard}
                                >
                                    {() => (
                                        <TextField
                                            name="idCard"
                                            margin="dense"
                                            label="ID Card"
                                            variant="outlined"
                                            fullWidth
                                            value={client.idCard}
                                            error={validationState.idCard}
                                            helperText={validationState.idCard ? "Invalid ID card" : ''}
                                            InputProps={{readOnly: mode === 'delete' || mode === 'info'}}
                                        />
                                    )}
                                </InputMask>
                            </Grid>
                        </Grid>
                        <Divider/>
                        <Stack direction={"row"} className={"ClientUpdateDialogStack"}>
                            <ContactPhoneIcon className={"DividerIcon"}/>
                            <Typography variant="h6" className={"MarginTopBottomAuto"}>
                                Contact information
                            </Typography>
                        </Stack>
                        <Grid container spacing={2} columns={{xs: 6, sm: 12}}>
                            <Grid xs={6} md={5}>
                                <InputMask
                                    mask="999 999 999"
                                    value={client.phone}
                                    disabled={false}
                                    maskChar=" "
                                    onChange={handleChange}
                                    onBlur={validatePhone}
                                >
                                    {() => (
                                        <TextField
                                            name="phone"
                                            margin="dense"
                                            label="Phone number"
                                            variant="outlined"
                                            fullWidth
                                            value={client.phone}
                                            error={validationState.phone !== ""}
                                            helperText={validationState.phone === "invalidPhone" ? "Invalid phone" : "Required"}
                                            InputProps={{
                                                readOnly: mode === 'delete' || mode === 'info',
                                                startAdornment: <InputAdornment position="start">+48</InputAdornment>
                                            }}
                                        />
                                    )}
                                </InputMask>
                            </Grid>
                            <Grid xs={6} md={7}>
                                <TextField
                                    name="email"
                                    margin="dense"
                                    label="Email"
                                    variant="outlined"
                                    fullWidth
                                    value={client.email}
                                    onChange={handleChange}
                                    onBlur={validateEmail}
                                    error={validationState.email}
                                    helperText={validationState.email ? "Invalid email" : ''}
                                    InputProps={{readOnly: mode === 'delete' || mode === 'info'}}
                                />
                            </Grid>
                        </Grid>
                        <Divider/>
                        <Stack direction={"row"} className={"ClientUpdateDialogStack"}>
                            <HomeIcon className={"DividerIcon"}/>
                            <Typography variant="h6" className={"MarginTopBottomAuto"}>
                                Address
                            </Typography>
                        </Stack>
                        <Grid container spacing={2} columns={{xs: 24, sm: 12}}>
                            <Grid xs={24} md={5}>
                                <TextField
                                    name="city"
                                    margin="dense"
                                    label="City"
                                    variant="outlined"
                                    fullWidth
                                    value={client.city}
                                    onChange={handleChange}
                                    onBlur={validateCity}
                                    error={validationState.city}
                                    helperText="Required"
                                    InputProps={{readOnly: mode === 'delete' || mode === 'info'}}
                                />
                            </Grid>
                            <Grid xs={13} md={7}>
                                <TextField
                                    name="street"
                                    margin="dense"
                                    label="Street"
                                    variant="outlined"
                                    fullWidth
                                    value={client.street}
                                    onChange={handleChange}
                                    InputProps={{readOnly: mode === 'delete' || mode === 'info'}}
                                />
                            </Grid>
                            <Grid xs={11} md={5}>
                                <TextField
                                    name="streetNumber"
                                    margin="dense"
                                    label="Street number"
                                    variant="outlined"
                                    fullWidth
                                    value={client.streetNumber}
                                    onChange={handleChange}
                                    onBlur={validateStreetNumber}
                                    error={validationState.streetNumber}
                                    helperText="Required"
                                    InputProps={{readOnly: mode === 'delete' || mode === 'info'}}
                                />
                            </Grid>
                        </Grid>
                    </Stack>
                </DialogContent>
            </Scrollbars>
            {mode !== 'info' && showDialogButtons ? (
                <Stack direction="row" justifyContent="space-between" className={"DialogStack"}>
                    {mode === 'delete' ? (
                        <Button variant="contained" color={"error"} size="large" endIcon={<DeleteIcon/>}
                                onClick={handleSave}
                                className={"DialogButton"}>
                            Delete
                        </Button>) : (<Button variant="contained" color={"success"} size="large" endIcon={<DoneIcon/>}
                                              onClick={handleSave}
                                              className={"DialogButton"}
                                              disabled={ValidateClient(client).length !== 0}>
                        Save
                    </Button>)}
                    <Button variant="outlined" color={"primary"} size="large" endIcon={<CancelIcon/>}
                            onClick={() => handleCancelDialog()} className={"DialogButton"}>
                        Cancel
                    </Button>
                </Stack>
            ) : null}
        </div>
    );
}
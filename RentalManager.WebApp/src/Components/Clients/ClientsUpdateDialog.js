import {
    Backdrop,
    Button,
    CircularProgress,
    DialogContent,
    Divider, FormHelperText,
    InputAdornment,
    Stack,
    TextField,
    Typography,
} from "@mui/material";
import Grid from '@mui/material/Unstable_Grid2';
import CancelIcon from '@mui/icons-material/Cancel';
import DoneIcon from '@mui/icons-material/Done';
import * as React from 'react';
import {Scrollbars} from 'react-custom-scrollbars-2';
import 'dayjs/locale/pl';
import {updateClient} from "../../Actions/ClientActions";
import PersonIcon from '@mui/icons-material/Person';
import ContactPhoneIcon from '@mui/icons-material/ContactPhone';
import HomeIcon from '@mui/icons-material/Home';
import ValidateClient from "../../Actions/ValidateClient";

export default function ClientsUpdateDialog({handleCancelDialog, client, handleDialogSuccess}) {
    const [clientDialog, setClientDialog] = React.useState(client);
    const [isLoading, setIsLoading] = React.useState(false)
    const [errorCodes, setErrorCodes] = React.useState('')

    const handleChangeName = (event) => {
        setClientDialog({
            ...clientDialog,
            name: event.target.value
        });
    };

    const handleChangeSurname = (event) => {
        setClientDialog({
            ...clientDialog,
            surname: event.target.value
        });
    };

    const handleChangePhone = (event) => {
        if(event.target.value.replace(/[^0-9]/g, '').length < 10)
            setClientDialog({
                ...clientDialog,
                phone: event.target.value
            });
    };

    const handleChangeEmail = (event) => {
        setClientDialog({
            ...clientDialog,
            email: event.target.value
        });
    };

    const handleChangeIdCard = (event) => {
        setClientDialog({
            ...clientDialog,
            idCard: event.target.value.toUpperCase()
        });
    };

    const handleChangeCity = (event) => {
        setClientDialog({
            ...clientDialog,
            city: event.target.value
        });
    };

    const handleChangeStreet = (event) => {
        setClientDialog({
            ...clientDialog,
            street: event.target.value
        });
    };

    const handleChangeStreetNumber = (event) => {
        setClientDialog({
            ...clientDialog,
            streetNumber: event.target.value
        });
    };

    const validateForm = () => {
        const res = ValidateClient(clientDialog);
        setErrorCodes(res)
    }

    const handleSave = async () => {
        const res = ValidateClient(clientDialog);
        if(res.length === 0) {
            setIsLoading(true);
            await updateClient(clientDialog);
            setIsLoading(false);
            handleDialogSuccess("edit")
        } else {
            setErrorCodes(res)
        }
    }

    return (
        <div>
            <Scrollbars autoHeight={true} autoHeightMin={200} autoHeightMax={'50vh'} autoHide
                        autoHideTimeout={750} autoHideDuration={500}>
                <DialogContent
                    className={"ClientUpdateDialogContainer"}>
                    <Backdrop
                        sx={{color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1}}
                        open={isLoading}
                    >
                        <CircularProgress/>
                    </Backdrop>
                    <Stack spacing={2}>
                        <Stack direction={"row"} className={"ClientUpdateDialogStack"}>
                            <PersonIcon sx={{marginRight: 1, marginTop: "auto", marginBottom: "auto"}}/>
                            <Typography variant="h6" sx={{marginTop: "auto", marginBottom: "auto"}}>
                                Personal information
                            </Typography>
                        </Stack>
                        <Grid container spacing={2} columns={{xs: 6, sm: 12}}>
                            <Grid xs={6} md={5}>
                                <TextField
                                    id="name"
                                    margin="dense"
                                    label="Name"
                                    variant="outlined"
                                    fullWidth
                                    value={clientDialog.name}
                                    className={""}
                                    onChange={handleChangeName}
                                    onBlur={validateForm}
                                    error={errorCodes.includes("noName")}
                                />
                                <FormHelperText id="component-error-text" error>{errorCodes.includes("noName") ? "Required" : ''}</FormHelperText>
                            </Grid>
                            <Grid xs={6} md={7}>
                                <TextField
                                    margin="dense"
                                    label="Surname"
                                    variant="outlined"
                                    fullWidth
                                    value={clientDialog.surname}
                                    onChange={handleChangeSurname}
                                    onBlur={validateForm}
                                    error={errorCodes.includes("noSurname")}
                                />
                                <FormHelperText id="component-error-text" error>{errorCodes.includes("noSurname") ? "Required" : ''}</FormHelperText>
                            </Grid>
                            <Grid xs={6} md={5}>
                                <TextField
                                    margin="dense"
                                    label="ID Card"
                                    variant="outlined"
                                    fullWidth
                                    value={clientDialog.idCard}
                                    onChange={handleChangeIdCard}
                                    onBlur={validateForm}
                                    error={errorCodes.includes("invalidIdCard")}
                                />
                                <FormHelperText id="component-error-text" error>{errorCodes.includes("invalidIdCard") ? "Invalid ID card" : ''}</FormHelperText>
                            </Grid>
                        </Grid>
                        <Divider/>
                        <Stack direction={"row"} className={"ClientUpdateDialogStack"}>
                            <ContactPhoneIcon sx={{marginRight: 1, marginTop: "auto", marginBottom: "auto"}}/>
                            <Typography variant="h6" sx={{marginTop: "auto", marginBottom: "auto"}}>
                                Contact information
                            </Typography>
                        </Stack>
                        <Grid container spacing={2} columns={{xs: 6, sm: 12}}>
                            <Grid xs={6} md={5}>
                                <TextField
                                    margin="dense"
                                    label="Phone number"
                                    variant="outlined"
                                    fullWidth
                                    value={clientDialog.phone}
                                    onChange={handleChangePhone}
                                    InputProps={{
                                        startAdornment: <InputAdornment position="start">+48</InputAdornment>,
                                    }}
                                    onBlur={validateForm}
                                    error={errorCodes.includes("invalidPhone") || errorCodes.includes("noPhone")}
                                />
                                <FormHelperText id="component-error-text" error>{errorCodes.includes("invalidPhone") ? "Invalid phone" : errorCodes.includes("noPhone") ? "Required" : ''}</FormHelperText>
                            </Grid>
                            <Grid xs={6} md={7}>
                                <TextField
                                    margin="dense"
                                    label="Email"
                                    variant="outlined"
                                    fullWidth
                                    value={clientDialog.email}
                                    onChange={handleChangeEmail}
                                    onBlur={validateForm}
                                    error={errorCodes.includes("invalidEmail")}
                                />
                                <FormHelperText id="component-error-text" error>{errorCodes.includes("invalidEmail") ? "Invalid email" : ''}</FormHelperText>
                            </Grid>
                        </Grid>
                        <Divider/>
                        <Stack direction={"row"} className={"ClientUpdateDialogStack"}>
                            <HomeIcon sx={{marginRight: 1, marginTop: "auto", marginBottom: "auto"}}/>
                            <Typography variant="h6" sx={{marginTop: "auto", marginBottom: "auto"}}>
                                Address
                            </Typography>
                        </Stack>
                        <Grid container spacing={2} columns={{xs: 12, sm: 12}}>
                            <Grid xs={12} md={5}>
                                <TextField
                                    margin="dense"
                                    label="City"
                                    variant="outlined"
                                    fullWidth
                                    value={clientDialog.city}
                                    onChange={handleChangeCity}
                                    onBlur={validateForm}
                                    error={errorCodes.includes("noCity")}
                                />
                                <FormHelperText id="component-error-text" error>{errorCodes.includes("noCity") ? "Required" : ''}</FormHelperText>
                            </Grid>
                            <Grid xs={7} md={7}>
                                <TextField
                                    margin="dense"
                                    label="Street"
                                    variant="outlined"
                                    fullWidth
                                    value={clientDialog.street}
                                    onChange={handleChangeStreet}
                                />
                            </Grid>
                            <Grid xs={5} md={5}>
                                <TextField
                                    margin="dense"
                                    label="Street number"
                                    variant="outlined"
                                    fullWidth
                                    value={clientDialog.streetNumber}
                                    onChange={handleChangeStreetNumber}
                                    onBlur={validateForm}
                                    error={errorCodes.includes("noStreetNumber")}
                                />
                                <FormHelperText id="component-error-text" error>{errorCodes.includes("noStreetNumber") ? "Required" : ''}</FormHelperText>
                            </Grid>
                        </Grid>
                    </Stack>
                </DialogContent>
            </Scrollbars>
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
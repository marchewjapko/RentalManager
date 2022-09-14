import {
    Backdrop,
    Button, CircularProgress,
    DialogContent,
    Stack,
    TextField,
} from "@mui/material";
import CancelIcon from '@mui/icons-material/Cancel';
import DoneIcon from '@mui/icons-material/Done';
import * as React from 'react';
import {Scrollbars} from 'react-custom-scrollbars-2';
import {DatePicker, LocalizationProvider} from "@mui/x-date-pickers";
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import dayjs from 'dayjs';
import 'dayjs/locale/pl';
import {updateClient} from "../../Actions/ClientActions";

export default function ClientsUpdateDialog ({handleCancelDialog, client, handleDialogSuccess}) {
    const [clientDialog, setClientDialog] = React.useState(client);
    const [isLoading, setIsLoading] = React.useState(false)

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
            idCard: event.target.value
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

    const handleSave = async () => {
        setIsLoading(true);
        await updateClient(clientDialog);
        setIsLoading(false);
        handleDialogSuccess("edit")
    }

    return (
        <div>
            <Scrollbars autoHeight={true} autoHeightMin={200} autoHeightMax={'50vh'} autoHide
                        autoHideTimeout={750} autoHideDuration={500}>
            <DialogContent
                className={"ClientDialogContainer"}>
                <Backdrop
                    sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
                    open={isLoading}
                >
                    <CircularProgress/>
                </Backdrop>

                    <TextField
                        id="name"
                        margin="dense"
                        label="Name"
                        variant="outlined"
                        fullWidth
                        value={clientDialog.name}
                        className={""}
                        onChange={handleChangeName}
                    />
                    <TextField
                        margin="dense"
                        label="Surname"
                        variant="outlined"
                        fullWidth
                        value={clientDialog.surname}
                        className={"DialogLowerTextField"}
                        onChange={handleChangeSurname}
                    />
                    <TextField
                        margin="dense"
                        label="Phone number"
                        variant="outlined"
                        fullWidth
                        value={clientDialog.phone}
                        className={"DialogLowerTextField"}
                        onChange={handleChangePhone}
                    />
                    <TextField
                        margin="dense"
                        label="Email"
                        variant="outlined"
                        fullWidth
                        value={clientDialog.email}
                        className={"DialogLowerTextField"}
                        onChange={handleChangeEmail}
                    />
                    <TextField
                        margin="dense"
                        label="ID Card"
                        variant="outlined"
                        fullWidth
                        value={clientDialog.idCard}
                        className={"DialogLowerTextField"}
                        onChange={handleChangeIdCard}
                    />
                    <TextField
                        margin="dense"
                        label="City"
                        variant="outlined"
                        fullWidth
                        value={clientDialog.city}
                        className={"DialogLowerTextField"}
                        onChange={handleChangeCity}
                    />
                    <TextField
                        margin="dense"
                        label="Street"
                        variant="outlined"
                        fullWidth
                        value={clientDialog.street}
                        className={"DialogLowerTextField"}
                        onChange={handleChangeStreet}
                    />
                    <TextField
                        margin="dense"
                        label="Street number"
                        variant="outlined"
                        fullWidth
                        value={clientDialog.streetNumber}
                        className={"DialogLowerTextField"}
                        onChange={handleChangeStreetNumber}
                    />
                    <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale={'pl'}>
                        <DatePicker
                            label="Date added"
                            className={"DialogLowerTextField DialogDatePicker"}
                            value={dayjs(client.dateAdded)}
                            minDate={dayjs(client.dateAdded)}
                            maxDate={dayjs(client.dateAdded)}
                            onChange={() => null}
                            renderInput={(params) => <TextField {...params} />}
                        />
                    </LocalizationProvider>
            </DialogContent>
        </Scrollbars>
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
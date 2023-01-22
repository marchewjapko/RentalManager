import {AppBar, Backdrop, Button, CircularProgress, Stack, Tab, Tabs,} from "@mui/material";
import * as React from 'react';
import SwipeableViews from 'react-swipeable-views';
import ClientsDialog from "../Clients/ClientsDialog";
import "./RentalAgreement.js.css"
import DescriptionIcon from '@mui/icons-material/Description';
import PersonIcon from '@mui/icons-material/Person';
import Clients from "../Clients/Clients";
import RentalAgreementDialogAgreementTab from "./RentalAgreementDialogAgreementTab";
import DeleteIcon from "@mui/icons-material/Delete";
import DoneIcon from "@mui/icons-material/Done";
import CancelIcon from "@mui/icons-material/Cancel";
import ValidateClient from "../../Actions/ValidateClient";
import {Scrollbars} from "react-custom-scrollbars-2";
import ValidateRentalAgreement from "../../Actions/ValidateRentalAgreement";
import {addClient, deleteClient, updateClient} from "../../Actions/ClientActions";
import {addRentalAgreement, deleteRentalAgreement, updateRentalAgreement} from "../../Actions/RentalAgreementActions";

export default function RentalAgreementDialog({agreement, mode, handleCancelDialog, handleDialogSuccess}) {
    const [value, setValue] = React.useState(0);
    const [client, setClient] = React.useState(agreement.client ? agreement.client : null);
    const [agreementDialog, setAgreementDialog] = React.useState(agreement ? agreement : null)
    const [isLoading, setIsLoading] = React.useState(false)

    const handleChange = (event, newValue) => {
        setValue(newValue);
    };

    const handleChangeIndex = (index) => {
        setValue(index);
    };

    const handleSave = async () => {
        const resAgreement = ValidateRentalAgreement(agreementDialog);
        if(client) {
            const resClient = ValidateClient(client)
            if (resAgreement.length === 0 && resClient.length === 0) {
                setIsLoading(true);
                switch (mode) {
                    case 'delete':
                        await Promise.all([deleteClient(client.id), deleteRentalAgreement(agreementDialog.id)]);
                        break;
                    case 'update':
                        await Promise.all([updateClient(client), updateRentalAgreement(agreementDialog)]);
                        break;
                    case 'post':
                        let postAgreement = {...agreementDialog}
                        postAgreement.client = client
                        postAgreement.dateAdded = postAgreement.dateAdded.format("DD.MM.YYYY")
                        postAgreement.validUntil = postAgreement.validUntil.format("DD.MM.YYYY")
                        await addRentalAgreement(postAgreement);
                        break;
                }
                setIsLoading(false);
                handleDialogSuccess(mode)
            }
        }
    }

    return (
        <div>
            <Backdrop
                sx={{color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1}}
                open={isLoading}
            >
                <CircularProgress/>
            </Backdrop>
            <AppBar position="static">
                <Tabs
                    value={value}
                    onChange={handleChange}
                    indicatorColor="primary"
                    textColor="inherit"
                    variant="fullWidth"
                    centered={true}
                >
                    <Tab icon={<PersonIcon/>} label="Client"/>
                    <Tab icon={<DescriptionIcon/>} label="Agreement"/>
                </Tabs>
            </AppBar>
            <SwipeableViews
                index={value}
                onChangeIndex={handleChangeIndex}
            >
                <div className={"rentalAgreementSlide"}>
                    {mode === 'post' ? (
                        <Clients isCheckable={true} client={client} setClient={setClient}/>
                    ) : (
                        <ClientsDialog client={client}
                                       handleCancelDialog={() => null}
                                       handleDialogSuccess={() => null}
                                       mode={mode === 'delete' ? 'info' : mode}
                                       setClient={setClient}/>
                    )}
                </div>
                <div className={"rentalAgreementSlide"}>
                    <Scrollbars autoHeight={true} autoHeightMin={0} autoHeightMax={'57vh'} autoHide
                                autoHideTimeout={750}
                                autoHideDuration={500}>
                    <RentalAgreementDialogAgreementTab mode={mode} agreement={agreementDialog}
                                                       setAgreement={setAgreementDialog}/>
                    </Scrollbars>
                </div>
            </SwipeableViews>
            <Stack direction="row" justifyContent="space-between" className={"DialogStack"}>
                {mode !== 'info' ? (
                    mode === 'delete' ? (
                        <Button variant="contained" color={"error"} size="large" endIcon={<DeleteIcon/>}
                                onClick={handleSave}
                                className={"DialogButton"}>
                            Delete
                        </Button>) : (
                        <Button variant="contained" color={"success"} size="large" endIcon={<DoneIcon/>}
                                onClick={handleSave}
                                className={"DialogButton"}
                                disabled={!client || ValidateClient(client).length !== 0 || ValidateRentalAgreement(agreementDialog).length !== 0}>
                            Save
                        </Button>)
                ) : null}
                <Button variant="outlined" color={"primary"} size="large" endIcon={<CancelIcon/>}
                        onClick={() => handleCancelDialog(false)} className={"DialogButton"}>
                    Cancel
                </Button>
            </Stack>
        </div>
    );
}
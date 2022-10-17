import {AppBar, Button, Stack, Tab, Tabs,} from "@mui/material";
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

export default function RentalAgreementDialog({agreement, mode}) {
    const [value, setValue] = React.useState(0);
    const [client, setClient] = React.useState(agreement.client ? agreement.client : {});
    const [agreementDialog, setAgreementDialog] = React.useState(agreement ? agreement : null)

    const handleChange = (event, newValue) => {
        console.log("value:", newValue)
        setValue(newValue);
    };

    const handleChangeIndex = (index) => {
        console.log("index:", index)
        setValue(index);
    };
    return (
        <div>
            <AppBar position="static">
                <Tabs
                    value={value}
                    onChange={handleChange}
                    indicatorColor="primary"
                    textColor="inherit"
                    variant="fullWidth"
                    centered={true}
                >
                    <Tab icon={<DescriptionIcon/>} label="Agreement"/>
                    <Tab icon={<PersonIcon/>} label="Client"/>
                </Tabs>
            </AppBar>
            <SwipeableViews
                index={value}
                onChangeIndex={handleChangeIndex}
            >
                {/*{value === 0 ? (*/}
                {/*    <div className={"rentalAgreementSlide"}>*/}
                {/*        <Scrollbars autoHeight={true} autoHeightMin={0} autoHeightMax={'57vh'} autoHide*/}
                {/*                    autoHideTimeout={750}*/}
                {/*                    autoHideDuration={500}>*/}
                {/*            <RentalAgreementDialogAgreementTab mode={mode} agreement={agreementDialog}*/}
                {/*                                               setAgreement={setAgreementDialog}/>*/}
                {/*        </Scrollbars>*/}
                {/*    </div>*/}
                {/*) : <div/>}*/}

                {/*{value === 1 ? (*/}
                {/*    <div className={"rentalAgreementSlide"}>*/}
                {/*        {mode === 'post' ? (*/}
                {/*            <Clients isCheckable={true} client={client} setClient={setClient}/>*/}
                {/*        ) : (*/}
                {/*            <ClientsDialog client={client}*/}
                {/*                           handleCancelDialog={() => null}*/}
                {/*                           handleDialogSuccess={() => null}*/}
                {/*                           mode={mode === 'delete' ? 'info' : mode}*/}
                {/*                           setClient={setClient}/>*/}
                {/*        )}*/}
                {/*    </div>*/}
                {/*) : <div/>}*/}

                <div className={"rentalAgreementSlide"}>
                    <Scrollbars autoHeight={true} autoHeightMin={0} autoHeightMax={'57vh'} autoHide
                                autoHideTimeout={750}
                                autoHideDuration={500}>
                    <RentalAgreementDialogAgreementTab mode={mode} agreement={agreementDialog}
                                                       setAgreement={setAgreementDialog}/>
                    </Scrollbars>
                </div>
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
            </SwipeableViews>
            <Stack direction="row" justifyContent="space-between" className={"DialogStack"}>
                {mode === 'delete' ? (
                    <Button variant="contained" color={"error"} size="large" endIcon={<DeleteIcon/>}
                            onClick={() => null}
                            className={"DialogButton"}>
                        Delete
                    </Button>) : (
                    <Button variant="contained" color={"success"} size="large" endIcon={<DoneIcon/>}
                            onClick={() => null}
                            className={"DialogButton"}
                            disabled={mode !== 'post' && ValidateClient(client).length !== 0}>
                        Save
                    </Button>)}
                <Button variant="outlined" color={"primary"} size="large" endIcon={<CancelIcon/>}
                        onClick={() => console.log("agreementDialog:", agreementDialog)} className={"DialogButton"}>
                    Cancel
                </Button>
            </Stack>
        </div>
    );
}
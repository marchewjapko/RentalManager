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

export default function RentalAgreementDialog({agreement, mode}) {
    const [value, setValue] = React.useState(0);
    const [client, setClient] = React.useState(agreement.client ? agreement.client : null);
    const [employee, setEmployee] = React.useState(agreement.employee ? agreement.employee : null)
    const [rentalEquipment, setRentalEquipment] = React.useState(agreement.rentalEquipment ? agreement.rentalEquipment : null)

    const handleChange = (event, newValue) => {
        setValue(newValue);
    };

    const handleChangeIndex = (index) => {
        setValue(index);
    };

    const handleChangeClient = (updatedClient) => {
        setClient(updatedClient)
    }
    const clientTab = () => {
        if (mode === 'post') {
            return (<Clients isCheckable={true} handleChangeClient={handleChangeClient}/>);
        }
        return (<ClientsDialog client={agreement.client}
                               handleCancelDialog={() => null}
                               handleDialogSuccess={() => null}
                               mode={mode === 'delete' ? 'info' : mode}
                               isResettable={true}
                               handleChangeClient={handleChangeClient}/>);
    }
    const handleChangeEmployee = (employee) => {
        setEmployee(employee)
    }

    const handleChangeEquipment = (equipment) => {
        setRentalEquipment(equipment)
    }
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
                <div className={"rentalAgreementSlide"}>
                    <RentalAgreementDialogAgreementTab mode={mode} agreement={agreement}
                                                       handleChangeEmployee={handleChangeEmployee}
                                                       handleChangeEquipment={handleChangeEquipment}/>
                </div>
                <div className={"rentalAgreementSlide"}>
                    {clientTab(mode)}
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
                        onClick={() => console.log("AAAAAAAAAA", client)} className={"DialogButton"}>
                    Cancel
                </Button>
            </Stack>
        </div>
    );
}
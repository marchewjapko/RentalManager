import {AppBar, Tab, Tabs} from "@mui/material";
import * as React from 'react';
import SwipeableViews from 'react-swipeable-views';
import ClientsDialog from "../Clients/ClientsDialog";
import "./RentalAgreement.js.css"
import EmployeeDialog from "../Employees/EmployeeDialog";
import Clients from "../Clients/Clients";
import Employees from "../Employees/Employees";

export default function RentalAgreementDialog({agreement, mode, handleCancelDialog}) {
    const [value, setValue] = React.useState(0);

    const handleChange = (event, newValue) => {
        setValue(newValue);
    };

    const handleChangeIndex = (index) => {
        setValue(index);
    };

    const clientTab = (mode) => {
        if (mode === 'update') {
            return (
                <Clients isCheckable={true} initialClient={agreement.client}/>
            );
        } else if (mode === 'info') {
            return (
                <ClientsDialog client={agreement.client}
                               handleCancelDialog={() => null}
                               handleDialogSuccess={() => null}
                               mode={mode}
                               isResettable={true}/>
            );
        }
    }

    const employeeTab = () => {
        if (mode === 'update') {
            return (
                <Employees isCheckable={true} initialEmployee={agreement.employee}/>
            );
        } else if (mode === 'info') {
            return (
                <EmployeeDialog employee={agreement.employee}
                                handleCancelDialog={() => null}
                                handleDialogSuccess={() => null}
                                mode={mode}
                                isResettable={true}/>
            );
        }

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
                >
                    <Tab label="Agreement"/>
                    <Tab label="Client"/>
                    <Tab label="Equipment"/>
                    <Tab label="Employee"/>
                </Tabs>
            </AppBar>
            <SwipeableViews
                axis={false ? 'x-reverse' : 'x'}
                index={value}
                onChangeIndex={handleChangeIndex}
            >
                <div className={"rentalAgreementSlide"}>
                    LOL
                </div>
                <div className={"rentalAgreementSlide"}>
                    {clientTab(mode)}
                </div>
                <div className={"rentalAgreementSlide"}>
                    LOL
                </div>
                <div className={"rentalAgreementSlide"}>
                    {employeeTab()}
                </div>
            </SwipeableViews>
        </div>
    );
}
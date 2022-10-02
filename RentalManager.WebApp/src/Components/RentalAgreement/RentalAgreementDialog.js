import {AppBar, Tab, Tabs} from "@mui/material";
import * as React from 'react';
import SwipeableViews from 'react-swipeable-views';
import ClientsDialog from "../Clients/ClientsDialog";
import "./RentalAgreement.js.css"

function TabPanel({index, children}) {
    return (
        <div
            role="tabpanel"
            id={`full-width-tabpanel-${index}`}
            aria-labelledby={`full-width-tab-${index}`}
        >
            {children}
        </div>
    );
}

export default function RentalAgreementDialog({agreement, mode}) {
    const [value, setValue] = React.useState(0);

    const handleChange = (event, newValue) => {
        setValue(newValue);
    };

    const handleChangeIndex = (index) => {
        setValue(index);
    };

    const clientTab = () => {
        return (
            <ClientsDialog client={agreement.client}
                           handleCancelDialog={() => null}
                           handleDialogSuccess={() => null}
                           mode={mode}
                           isResettable={true}/>
        );
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
                    {clientTab()}
                </div>
                <div className={"rentalAgreementSlide"}>
                    LOL
                </div>
                <div className={"rentalAgreementSlide"}>
                    LOL
                </div>
            </SwipeableViews>
        </div>
    );
}
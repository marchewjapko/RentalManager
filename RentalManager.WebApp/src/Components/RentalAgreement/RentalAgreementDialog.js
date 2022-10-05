import {AppBar, Stack, Tab, Tabs, Typography} from "@mui/material";
import * as React from 'react';
import SwipeableViews from 'react-swipeable-views';
import ClientsDialog from "../Clients/ClientsDialog";
import "./RentalAgreement.js.css"
import EmployeeDialog from "../Employees/EmployeeDialog";
import ConstructionIcon from '@mui/icons-material/Construction';
import DescriptionIcon from '@mui/icons-material/Description';
import PersonIcon from '@mui/icons-material/Person';
import EngineeringIcon from '@mui/icons-material/Engineering';
import Clients from "../Clients/Clients";
import Employees from "../Employees/Employees";
import RentalEquipmentDialog from "../RentalEquipment/RentalEquipmentDialog";
import RentalEquipment from "../RentalEquipment/RentalEquipment";
import RentalEquipmentSelect from "../RentalEquipment/RentalEquipmentSelect";
import EmployeesSelect from "../Employees/EmployeesSelect";

export default function RentalAgreementDialog({agreement, mode, handleCancelDialog}) {
    const [value, setValue] = React.useState(0);

    const handleChange = (event, newValue) => {
        setValue(newValue);
    };

    const handleChangeIndex = (index) => {
        setValue(index);
    };

    const clientTab = (mode) => {
        if (mode === 'post') {
            return (<Clients isCheckable={true}/>);
        }
        return (<ClientsDialog client={agreement.client}
                               handleCancelDialog={() => null}
                               handleDialogSuccess={() => null}
                               mode={mode === 'delete' ? 'info' : mode}
                               isResettable={true}/>);
    }

    const employeeTab = () => {
        if (mode === 'post') {
            return (<Employees isCheckable={true}/>);
        }
        if(mode === 'update') {
            return (
                <EmployeesSelect selectedEmployee={agreement.employee}/>
            );
        }
        return (<EmployeeDialog employee={agreement.employee}
                                handleCancelDialog={() => null}
                                handleDialogSuccess={() => null}
                                mode={mode === 'delete' ? 'info' : mode}
                                isResettable={true}/>);
    }

    const rentalEquipmentTab = () => {
        if (mode === 'info') {
            return (<Stack spacing={2}>
                <Stack direction={"row"} className={"ClientUpdateDialogStack"}
                       sx={{paddingLeft: "17px", paddingTop: "20px"}}>
                    <ConstructionIcon sx={{marginRight: 1, marginTop: "auto", marginBottom: "auto"}}/>
                    <Typography variant="h6" sx={{marginTop: "auto", marginBottom: "auto"}}>
                        Equipment information
                    </Typography>
                </Stack>
                <div className={"NoTopMargin"}>
                    {agreement.rentalEquipment.map((x) => (<RentalEquipmentDialog rentalEquipment={x}
                                                                                  handleCancelDialog={() => null}
                                                                                  handleDialogSuccess={() => null}
                                                                                  mode={mode}
                                                                                  key={x.id}/>))}
                </div>
            </Stack>);
        }
        if (mode === 'post') {
            return (<RentalEquipment isCheckable={true}/>);
        }
        if (mode === 'update') {
            return (<RentalEquipmentSelect selectedEquipment={agreement.rentalEquipment}/>);
        }
        return (<RentalEquipment isCheckable={true} initialIds={agreement.rentalEquipment.map(x => x.id)}/>);
    }

    console.log("Agreement:", agreement)

    return (<div>
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
                <Tab icon={<EngineeringIcon/>} label="Employee"/>
            </Tabs>
        </AppBar>
        <SwipeableViews
            axis={'x'}
            index={value}
            onChangeIndex={handleChangeIndex}
        >
            <div className={"rentalAgreementSlide"}>
                {rentalEquipmentTab()}
            </div>
            <div className={"rentalAgreementSlide"}>
                {clientTab(mode)}
            </div>
            <div className={"rentalAgreementSlide"}>
                {employeeTab()}
            </div>
        </SwipeableViews>
    </div>);
}
import {
    Autocomplete,
    CircularProgress, DialogContent,
    Stack,
    TextField,
    Typography
} from "@mui/material";
import EngineeringIcon from '@mui/icons-material/Engineering';
import * as React from 'react';
import {getAllEmployees} from "../../Actions/EmployeeActions";


export default function EmployeesSelect({selectedEmployee, handleChangeEmployee}) {
    const [value, setValue] = React.useState(null);
    const [data, setData] = React.useState([]);
    const [open, setOpen] = React.useState(false);
    const [isLoading, setIsLoading] = React.useState(true);

    React.useEffect(() => {
        const getData = async () => {
            const result = await getAllEmployees();
            setData(result);
            setIsLoading(false)
            setValue(selectedEmployee ? selectedEmployee : null)
        };
        getData();
    }, []);

    const handleChange = (event, newValue) => {
        setValue(newValue)
        handleChangeEmployee(newValue)
    }

    return (
        <Stack spacing={2}>
            <Stack direction={"row"} className={"ClientUpdateDialogStack"} sx={{paddingLeft: "17px", paddingTop: "20px"}}>
                <EngineeringIcon sx={{marginRight: 1, marginTop: "auto", marginBottom: "auto"}}/>
                <Typography variant="h6" sx={{marginTop: "auto", marginBottom: "auto"}}>
                    Employee
                </Typography>
            </Stack>
            <Autocomplete
                value={value}
                isOptionEqualToValue={(option, value) => option.id === value.id}
                onChange={(event, newValue) => handleChange(event, newValue)}
                open={open}
                onOpen={() => setOpen(true)}
                onClose={() => setOpen(false)}
                options={data}
                getOptionLabel={(option) => option.name + ' ' + option.surname}
                loading={isLoading}
                fullWidth={true}
                renderInput={(params) => (
                    <TextField {...params} label="Select employee" InputProps={{
                        ...params.InputProps,
                        endAdornment: (
                            <React.Fragment>
                                {isLoading ? <CircularProgress color="inherit" size={20} /> : null}
                                {params.InputProps.endAdornment}
                            </React.Fragment>
                        ),
                    }}/>
                )}
            />
        </Stack>
    );
}
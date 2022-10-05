import {
    Autocomplete,
    CircularProgress,
    Stack,
    TextField,
    Typography
} from "@mui/material";
import EngineeringIcon from '@mui/icons-material/Engineering';
import * as React from 'react';
import {getAllEmployees} from "../../Actions/EmployeeActions";


export default function EmployeesSelect({selectedEmployee}) {
    const [value, setValue] = React.useState(null);
    const [data, setData] = React.useState([]);
    const [open, setOpen] = React.useState(false);
    const [isLoading, setIsLoading] = React.useState(true);

    React.useEffect(() => {
        const getData = async () => {
            const result = await getAllEmployees();
            setData(result);
            setIsLoading(false)
            setValue(selectedEmployee)
        };
        getData();
    }, []);

    const handleChange = (event, newValue) => {
        setValue(newValue)
    }

    return (
        <Stack spacing={2}>
            <Stack direction={"row"} className={"ClientUpdateDialogStack"} sx={{paddingLeft: "17px", paddingTop: "20px"}}>
                <EngineeringIcon sx={{marginRight: 1, marginTop: "auto", marginBottom: "auto"}}/>
                <Typography variant="h6" sx={{marginTop: "auto", marginBottom: "auto"}}>
                    Employee information
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
                disableCloseOnSelect={true}
                getOptionLabel={(option) => option.name + ' ' + option.surname}
                loading={isLoading}
                renderInput={(params) => (
                    <TextField {...params} label="Selected employee" InputProps={{
                        ...params.InputProps,
                        endAdornment: (
                            <React.Fragment>
                                {isLoading ? <CircularProgress color="inherit" size={20} /> : null}
                                {params.InputProps.endAdornment}
                            </React.Fragment>
                        ),
                    }}/>
                )}
                sx={{ width: '75%', paddingLeft: "17px" }}
            />
        </Stack>
    );
}
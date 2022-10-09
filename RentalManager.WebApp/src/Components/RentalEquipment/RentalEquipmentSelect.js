import {
    Autocomplete,
    CircularProgress, DialogContent,
    Stack,
    TextField,
    Typography
} from "@mui/material";
import ConstructionIcon from '@mui/icons-material/Construction';
import * as React from 'react';
import {getAllRentalEquipment} from "../../Actions/RentalEquipmentActions";


export default function RentalEquipmentSelect({selectedEquipment, handleChangeEquipment}) {
    const [value, setValue] = React.useState([]);
    const [data, setData] = React.useState([]);
    const [open, setOpen] = React.useState(false);
    const [isLoading, setIsLoading] = React.useState(true);

    React.useEffect(() => {
        const getData = async () => {
            const result = await getAllRentalEquipment();
            setData(result);
            setIsLoading(false)
            setValue(selectedEquipment ? selectedEquipment : [])
        };
        getData();
    }, []);

    const handleChange = (event, newValue) => {
        setValue(newValue)
        handleChangeEquipment(newValue)
    }

    return (
        <Stack spacing={2}>
            <Stack direction={"row"} className={"ClientUpdateDialogStack"} sx={{paddingLeft: "17px", paddingTop: "20px"}}>
                <ConstructionIcon sx={{marginRight: 1, marginTop: "auto", marginBottom: "auto"}}/>
                <Typography variant="h6" sx={{marginTop: "auto", marginBottom: "auto"}}>
                    Equipment
                </Typography>
            </Stack>
            <Autocomplete
                value={value}
                isOptionEqualToValue={(option, value) => option.id === value.id}
                onChange={(event, newValue) => handleChange(event, newValue)}
                multiple
                limitTags={2}
                open={open}
                onOpen={() => setOpen(true)}
                onClose={() => setOpen(false)}
                options={data}
                disableCloseOnSelect={true}
                getOptionLabel={(option) => option.name + ' (' + option.monthlyPrice + ' zł)'}
                loading={isLoading}
                fullWidth={true}
                renderInput={(params) => (
                    <TextField {...params} label="Select equipment" InputProps={{
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
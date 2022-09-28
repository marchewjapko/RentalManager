import {
    Box,
    Button,
    FormControl,
    IconButton,
    InputAdornment,
    MenuItem,
    OutlinedInput,
    Select,
    Stack,
    TextField
} from "@mui/material";
import * as React from 'react';
import CloseIcon from "@mui/icons-material/Close";
import SearchIcon from "@mui/icons-material/Search";
import InputMask from "react-input-mask";

export default function ClientsSearchSelect({isLoading, handleSearch}) {
    const [open, setOpen] = React.useState(false);
    const [searchValues, setSearchValues] = React.useState({
        surname: "",
        phone: "",
        city: "",
        street: ""
    })

    const handleKeyDown = (event) => {
        if (event.key === 'Enter') {
            handleSearch(searchValues);
        }
    }

    const handleSearchSurname = (event) => {
        setSearchValues({
            ...searchValues, surname: event.target.value
        })
    }

    const handleSearchPhone = (event) => {
        setSearchValues({
            ...searchValues, phone: event.target.value
        })
    }

    const handleSearchCity = (event) => {
        setSearchValues({
            ...searchValues, city: event.target.value
        })
    }

    const handleSearchStreet = (event) => {
        setSearchValues({
            ...searchValues, street: event.target.value
        })
    }

    const handleClose = () => {
        setOpen(false);
    };

    const handleOpen = () => {
        setOpen(true);
    };

    return (
        <FormControl sx={{width: "10rem"}}>
            <Select
                multiple
                value={[""]}
                renderValue={() => "Search options"}
                disabled={isLoading}
                open={open}
                onClose={handleClose}
                onOpen={handleOpen}
                sx={{width: "100%"}}>
                <Stack direction={"column"}
                       sx={{
                           paddingTop: "10px",
                           paddingBottom: "10px",
                           paddingLeft: "15px",
                           paddingRight: "15px",
                           width: "14.5rem"
                       }}
                       spacing={2}>
                    <TextField
                        margin="dense"
                        label="Surname"
                        variant="outlined"
                        fullWidth
                        value={searchValues.surname}
                        onChange={handleSearchSurname}
                        onKeyDown={handleKeyDown}
                        InputProps={{
                            endAdornment: <InputAdornment position="end">
                                {searchValues.surname.trim().length !== 0 ? (
                                    <IconButton color="default" onClick={() => setSearchValues({
                                        ...searchValues, surname: ""
                                    })} sx={{marginRight: "-4px"}}
                                                disabled={isLoading}>
                                        <CloseIcon/>
                                    </IconButton>
                                ) : (<Box component="span"
                                          sx={{width: "40px", height: "40px", marginRight: "-4px"}}/>)}
                            </InputAdornment>
                        }}
                    />
                    <InputMask
                        mask="999 999 999"
                        value={searchValues.phone}
                        disabled={false}
                        maskChar=" "
                        onChange={handleSearchPhone}
                        onKeyDown={handleKeyDown}
                    >
                        {() => (
                            <TextField
                                margin="dense"
                                label="Phone"
                                variant="outlined"
                                fullWidth
                                InputProps={{
                                    endAdornment: <InputAdornment position="end">
                                        {searchValues.phone.trim().length !== 0 ? (
                                            <IconButton color="default" onClick={() => setSearchValues({
                                                ...searchValues, phone: ""
                                            })} sx={{marginRight: "-4px"}}
                                                        disabled={isLoading}>
                                                <CloseIcon/>
                                            </IconButton>
                                        ) : (<Box component="span"
                                                  sx={{width: "40px", height: "40px", marginRight: "-4px"}}/>)}
                                    </InputAdornment>,
                                    startAdornment: <InputAdornment position="start">+48</InputAdornment>
                                }}
                            />
                        )}
                    </InputMask>
                    <TextField
                        margin="dense"
                        label="City"
                        variant="outlined"
                        fullWidth
                        value={searchValues.city}
                        onChange={handleSearchCity}
                        onKeyDown={handleKeyDown}
                        InputProps={{
                            endAdornment: <InputAdornment position="end">
                                {searchValues.city.trim().length !== 0 ? (
                                    <IconButton color="default" onClick={() => setSearchValues({
                                        ...searchValues, city: ""
                                    })} sx={{marginRight: "-4px"}}
                                                disabled={isLoading}>
                                        <CloseIcon/>
                                    </IconButton>
                                ) : (<Box component="span"
                                          sx={{width: "40px", height: "40px", marginRight: "-4px"}}/>)}
                            </InputAdornment>
                        }}
                    />
                    <TextField
                        margin="dense"
                        label="Street"
                        variant="outlined"
                        fullWidth
                        value={searchValues.street}
                        onChange={handleSearchStreet}
                        onKeyDown={handleKeyDown}
                        InputProps={{
                            endAdornment: <InputAdornment position="end">
                                {searchValues.street.trim().length !== 0 ? (
                                    <IconButton color="default" onClick={() => setSearchValues({
                                        ...searchValues, street: ""
                                    })} sx={{marginRight: "-4px"}}
                                                disabled={isLoading}>
                                        <CloseIcon/>
                                    </IconButton>
                                ) : (<Box component="span"
                                          sx={{width: "40px", height: "40px", marginRight: "-4px"}}/>)}
                            </InputAdornment>
                        }}
                    />
                    <Button variant="contained" color={"primary"} endIcon={<SearchIcon />} onClick={function(){ handleSearch(searchValues); setOpen(false);}}>
                        Search
                    </Button>
                </Stack>
            </Select>
        </FormControl>
    );
}
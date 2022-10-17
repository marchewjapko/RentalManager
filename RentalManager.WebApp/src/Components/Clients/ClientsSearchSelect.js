import {Box, Button, IconButton, InputAdornment, Popover, Stack, TextField} from "@mui/material";
import * as React from 'react';
import CloseIcon from "@mui/icons-material/Close";
import SearchIcon from "@mui/icons-material/Search";
import InputMask from "react-input-mask";

export default function ClientsSearchSelect({isLoading, handleSearch}) {
    const [anchorEl, setAnchorEl] = React.useState(null);
    const [searchValues, setSearchValues] = React.useState({
        surname: "",
        phone: "",
        city: "",
        street: ""
    })

    const handleKeyDown = (event) => {
        if (event.key === 'Enter') {
            setAnchorEl(null);
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

    const handleClick = (event) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    const open = Boolean(anchorEl);

    return (
        <div>
            <Button variant="outlined" color={"inherit"} endIcon={<SearchIcon/>} onClick={handleClick}
                    disabled={isLoading} sx={{height: "40px"}}>
                Search options
            </Button>
            <Popover
                open={open}
                anchorEl={anchorEl}
                onClose={handleClose}
                anchorOrigin={{
                    vertical: 'bottom',
                    horizontal: 'center',
                }}
                transformOrigin={{
                    vertical: 'top',
                    horizontal: 'center',
                }}
            >
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
                        size="small"
                        InputProps={{
                            endAdornment: <InputAdornment position="end">
                                {searchValues.surname.trim().length !== 0 ? (
                                    <IconButton color="default" onClick={() => setSearchValues({
                                        ...searchValues, surname: ""
                                    })} sx={{marginRight: "-4px"}}
                                                disabled={isLoading}>
                                        <CloseIcon/>
                                    </IconButton>
                                ) : (<Box component="span" className={"ClientSearchBox"}/>)}
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
                                size="small"
                                InputProps={{
                                    endAdornment: <InputAdornment position="end">
                                        {searchValues.phone.trim().length !== 0 ? (
                                            <IconButton color="default" onClick={() => setSearchValues({
                                                ...searchValues, phone: ""
                                            })} sx={{marginRight: "-4px"}}
                                                        disabled={isLoading}>
                                                <CloseIcon/>
                                            </IconButton>
                                        ) : (<Box component="span" className={"ClientSearchBox"}/>)}
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
                        size="small"
                        InputProps={{
                            endAdornment: <InputAdornment position="end">
                                {searchValues.city.trim().length !== 0 ? (
                                    <IconButton color="default" onClick={() => setSearchValues({
                                        ...searchValues, city: ""
                                    })} sx={{marginRight: "-4px"}}
                                                disabled={isLoading}>
                                        <CloseIcon/>
                                    </IconButton>
                                ) : (<Box component="span" className={"ClientSearchBox"}/>)}
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
                        size="small"
                        InputProps={{
                            endAdornment: <InputAdornment position="end">
                                {searchValues.street.trim().length !== 0 ? (
                                    <IconButton color="default" onClick={() => setSearchValues({
                                        ...searchValues, street: ""
                                    })} sx={{marginRight: "-4px"}}
                                                disabled={isLoading}>
                                        <CloseIcon/>
                                    </IconButton>
                                ) : (<Box component="span" className={"ClientSearchBox"}/>)}
                            </InputAdornment>
                        }}
                    />
                    <Button variant="contained" color={"primary"} sx={{height: "2.7em"}} endIcon={<SearchIcon/>}
                            onClick={function () {
                                handleSearch(searchValues);
                                setAnchorEl(null);
                            }}>
                        Search
                    </Button>
                </Stack>
            </Popover>
        </div>
    );
}
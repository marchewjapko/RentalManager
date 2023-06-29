import './AppBar.css'
import '../Shared.css'
import {IconButton, Paper, Stack, useTheme} from "@mui/material";
import MenuIcon from '@mui/icons-material/Menu';
import {ReactComponent as DarkAppName} from "../../Images/AppName-DarkMode.svg";
import {ReactComponent as BrightAppName} from "../../Images/AppName-BrightMode.svg";
import NavigationMenu from "../NavigationMenu/NavigationMenu";

function hexToRGB(hex: string, alpha: number) {
    const r = parseInt(hex.slice(1, 3), 16),
        g = parseInt(hex.slice(3, 5), 16),
        b = parseInt(hex.slice(5, 7), 16);

    if (alpha) {
        return "rgba(" + r + ", " + g + ", " + b + ", " + alpha + ")";
    } else {
        return "rgb(" + r + ", " + g + ", " + b + ")";
    }
}


export default function AppBar() {
    const displayMode = localStorage.getItem("DISPLAY_MODE")
    const theme = useTheme();
    return (
        <>
            <Paper elevation={12} className={'app-bar'} square
                   sx={{backgroundColor: hexToRGB(theme.palette.background.paper, 0.5)}}>
                <Stack direction="row" alignItems="center" justifyContent="space-between"
                       sx={{height: '100%', padding: '20px'}}>
                    <Stack direction="row" alignItems="center" justifyContent="space-between"
                           sx={{height: '100%'}} gap={2}>
                        <NavigationMenu/>
                        {displayMode === 'light' ? (
                            <div>
                                <BrightAppName className={'app-name'}/>
                            </div>
                        ) : (
                            <div>
                                <DarkAppName className={'app-name'}/>
                            </div>
                        )}
                    </Stack>
                    <div>Item 2</div>
                </Stack>
            </Paper>
        </>
    );
}
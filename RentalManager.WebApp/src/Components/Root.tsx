import {Outlet} from "react-router-dom";
import AppBar from "./AppBar/AppBar";

export default function Root() {
    return (
        <>
            <AppBar/>
            <div id="detail">
                <Outlet/>
            </div>
        </>
    );
}
import {Outlet, Link, useLoaderData,} from "react-router-dom";

import {
    OpenAPI,
    ClientService,
    ClientDto,
} from "../references/codegen";
import {useEffect, useState} from "react";
import {Button} from "@mui/material";

OpenAPI.BASE = localStorage.getItem("API_URL") ?? process.env.REACT_APP_API_URL

export async function loader(): Promise<ClientDto[]> {
    return ClientService.getClient({});
}

export default function Root() {
    const [clients, setClients] = useState(useLoaderData() as ClientDto[])
    console.log(clients)

    const handleButtonClick = async () => {
        if (clients.length > 0) {
            setClients([])
        } else {
            const newClients = await ClientService.getClient({});
            setClients(newClients)
        }
    }

    return (
        <>
            <div id="sidebar">
                <h1>React Router Contacts</h1>
                <div>
                    <form id="search-form" role="search">
                        <input
                            id="q"
                            aria-label="Search contacts"
                            placeholder="Search"
                            type="search"
                            name="q"
                        />
                        <div
                            id="search-spinner"
                            aria-hidden
                            hidden={true}
                        />
                        <div
                            className="sr-only"
                            aria-live="polite"
                        ></div>
                    </form>
                    <form method="post">
                        <button type="submit">New</button>
                    </form>
                </div>
                <Button onClick={handleButtonClick}>
                    ALA
                </Button>
                <nav>
                    <ul>
                        <li>
                            <Link to={`App`}>Your Name</Link>
                        </li>
                        {clients.map(x => (
                            <div key={x.id}>
                                {x.name}
                            </div>
                        ))}
                    </ul>
                </nav>
            </div>
            <div id="detail">
                <Outlet/>
            </div>
        </>
    );
}
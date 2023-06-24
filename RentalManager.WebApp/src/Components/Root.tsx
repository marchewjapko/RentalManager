import {Outlet, Link, useLoaderData,} from "react-router-dom";

import {
    OpenAPI,
    ClientService,
    ClientDto,
} from "../references/codegen";
import {useEffect, useState} from "react";
import {Button} from "@mui/material";
import axios from "axios";
import {createWriteStream} from "fs";
import {promisify} from "util";
import * as stream from "stream";

OpenAPI.BASE = localStorage.getItem("API_URL") ?? process.env.REACT_APP_API_URL

export async function loader(): Promise<ClientDto[]> {
    return ClientService.getClient({});
}

export default function Root() {
    // const [clients, setClients] = useState(useLoaderData() as ClientDto[])
    // console.log(clients)
    //
    // const handleButtonClick = async () => {
    //     if (clients.length > 0) {
    //         setClients([])
    //     } else {
    //         const newClients = await ClientService.getClient({});
    //         setClients(newClients)
    //     }
    // }

    const client = axios.create({
        baseURL: "http://127.0.0.1:8000"
    });

    const handleClick = () => {
        // client.get('/documents/generate_document', {
        //     params: {
        //         "name": "name",
        //         "address": "address",
        //         "id_card": "id_card",
        //         "phone_number": "phone_number",
        //     }, responseType: 'blob'
        // }).then(x => {
        //     window.open(URL.createObjectURL(x.data));
        // })
        client.post('/documents/generate_document', {
          "name": "LOLOL",
          "price": 100
        }, {
            params: {
                "name": "name",
                "address": "address",
                "id_card": "id_card",
                "phone_number": "phone_number",
            }, responseType: 'blob'
        }).then(response => {
            const href = window.URL.createObjectURL(response.data);

            const anchorElement = document.createElement('a');

            anchorElement.href = href;
            anchorElement.download = "test.pdf";

            document.body.appendChild(anchorElement);
            anchorElement.click();

            document.body.removeChild(anchorElement);
            window.URL.revokeObjectURL(href);
        })
            .catch(error => {
                console.log('error: ', error);
            });
    }

    return (
        <>
            <Button onClick={handleClick}>
                CLICK ME
            </Button>
            {/*<div id="sidebar">*/}
            {/*    <h1>React Router Contacts</h1>*/}
            {/*    <div>*/}
            {/*        <form id="search-form" role="search">*/}
            {/*            <input*/}
            {/*                id="q"*/}
            {/*                aria-label="Search contacts"*/}
            {/*                placeholder="Search"*/}
            {/*                type="search"*/}
            {/*                name="q"*/}
            {/*            />*/}
            {/*            <div*/}
            {/*                id="search-spinner"*/}
            {/*                aria-hidden*/}
            {/*                hidden={true}*/}
            {/*            />*/}
            {/*            <div*/}
            {/*                className="sr-only"*/}
            {/*                aria-live="polite"*/}
            {/*            ></div>*/}
            {/*        </form>*/}
            {/*        <form method="post">*/}
            {/*            <button type="submit">New</button>*/}
            {/*        </form>*/}
            {/*    </div>*/}
            {/*    <Button onClick={handleButtonClick}>*/}
            {/*        ALA*/}
            {/*    </Button>*/}
            {/*    <nav>*/}
            {/*        <ul>*/}
            {/*            <li>*/}
            {/*                <Link to={`App`}>Your Name</Link>*/}
            {/*            </li>*/}
            {/*            {clients.map(x => (*/}
            {/*                <div key={x.id}>*/}
            {/*                    {x.name}*/}
            {/*                </div>*/}
            {/*            ))}*/}
            {/*        </ul>*/}
            {/*    </nav>*/}
            {/*</div>*/}
            {/*<div id="detail">*/}
            {/*    <Outlet/>*/}
            {/*</div>*/}
        </>
    );
}
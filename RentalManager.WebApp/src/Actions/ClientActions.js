import {ClientsMock} from "../Mocks/ClientsMock";
export const getAllClients = () =>
    new Promise((resolve) => {
        setTimeout(() => resolve(Object.values(ClientsMock)), 2500);
    });

export const updateClient = (client) =>
    new Promise((resolve) => {
        setTimeout(() => resolve("Client updated!"), 2500);
    });

export const deleteClient = (id) =>
    new Promise((resolve) => {
        setTimeout(() => resolve("Client deleted!"), 2500);
    });
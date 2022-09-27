import {ClientsMock} from "../Mocks/ClientsMock";

export const getAllClients = () => new Promise((resolve) => {
    setTimeout(() => (resolve(Object.values(ClientsMock)), console.log("Got all clients")), 2500);
});

export const updateClient = (client) => new Promise((resolve) => {
    setTimeout(() => (resolve("Client updated!"), console.log("Updated client:", client)), 2500);
});

export const deleteClient = (id) => new Promise((resolve) => {
    setTimeout(() => (resolve("Client deleted!"), console.log("Deleted client:", id)), 2500);
});
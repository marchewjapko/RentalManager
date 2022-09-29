import {ClientsMock} from "../Mocks/ClientsMock";

export const getAllClients = () => new Promise((resolve) => {
    setTimeout(() => (resolve(Object.values(ClientsMock)), console.log("Got all clients")), 2500);
});

export const addClient = (client) => new Promise((resolve) => {
    setTimeout(() => (resolve("Client added!"), console.log("Added client:", client)), 2500);
});

export const updateClient = (client) => new Promise((resolve) => {
    setTimeout(() => (resolve("Client updated!"), console.log("Updated client:", client)), 2500);
});

export const deleteClient = (id) => new Promise((resolve) => {
    setTimeout(() => (resolve("Client deleted!"), console.log("Deleted client:", id)), 2500);
});

export const filterClients = (searchParams) => new Promise((resolve) => {
    setTimeout(() => (resolve(Object.values(ClientsMock.filter(x => x.surname.toLowerCase().includes(searchParams.surname.toLowerCase()) && x.phone.toLowerCase().includes(searchParams.phone.toLowerCase()) && x.city.toLowerCase().includes(searchParams.city.toLowerCase()) && x.street.toLowerCase().includes(searchParams.street.toLowerCase())))), console.log("Filtered clients, params:", searchParams)), 2500);
});
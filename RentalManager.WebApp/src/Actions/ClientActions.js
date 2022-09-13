import {ClientsMock} from "../Mocks/ClientsMock";
export const getAllClients = () =>
    new Promise((resolve) => {
        setTimeout(() => resolve(Object.values(ClientsMock)), 2500);
    });
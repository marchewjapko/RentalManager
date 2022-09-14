import {RentalEquipmentMock} from "../Mocks/RentalEquipmentMock";
export const getAllRentalEquipment = () =>
    new Promise((resolve) => {
        setTimeout(() => resolve(Object.values(RentalEquipmentMock)), 2500);
    });

export const filterRentalEquipment = (name) =>
    new Promise((resolve) => {
        setTimeout(() => resolve(Object.values(RentalEquipmentMock.filter(x => x.name.toLowerCase().includes(name.toLowerCase())))), 2500);
    });

export const updateRentalEquipment = (client) =>
    new Promise((resolve) => {
        setTimeout(() => resolve("Rental equipment updated!"), 2500);
    });

export const deleteRentalEquipment = (id) =>
    new Promise((resolve) => {
        setTimeout(() => resolve("Rental equipment deleted!"), 2500);
    });
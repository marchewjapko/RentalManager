import {RentalEquipmentMock} from "../Mocks/RentalEquipmentMock";
export const getAllRentalEquipment = () =>
    new Promise((resolve) => {
        setTimeout(() => resolve(Object.values(RentalEquipmentMock)), 2500);
    });

export const filterEmployees = (name) =>
    new Promise((resolve) => {
        setTimeout(() => resolve(Object.values(RentalEquipmentMock.filter(x => x.name.toLowerCase().includes(name.toLowerCase())))), 2500);
    });
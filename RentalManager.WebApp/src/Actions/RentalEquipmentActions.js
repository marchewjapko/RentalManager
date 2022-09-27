import {RentalEquipmentMock} from "../Mocks/RentalEquipmentMock";

export const getAllRentalEquipment = () => new Promise((resolve) => {
    setTimeout(() => (resolve(Object.values(RentalEquipmentMock)), console.log("Got all rental equipment")), 2500);
});

export const addRentalEquipment = (rentalEquipment) => new Promise((resolve) => {
    setTimeout(() => (resolve("Rental equipment added!"), console.log("Added rental equipment:", rentalEquipment)), 2500);
});

export const filterRentalEquipment = (name) => new Promise((resolve) => {
    setTimeout(() => (resolve(Object.values(RentalEquipmentMock.filter(x => x.name.toLowerCase().includes(name.toLowerCase())))), console.log("Filtered rental equipments")), 2500);
});

export const updateRentalEquipment = (rentalEquipment) => new Promise((resolve) => {
    setTimeout(() => (resolve("Rental equipment updated!"), console.log("Updated rental equipment:", rentalEquipment)), 2500);
});

export const deleteRentalEquipment = (id) => new Promise((resolve) => {
    setTimeout(() => (resolve("Rental equipment deleted!"), console.log("Deleted rental equipment:", id)), 2500);
});
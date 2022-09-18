import {EmployeesMock} from "../Mocks/EmployeesMock";

export const getAllEmployees = () =>
    new Promise((resolve) => {
        setTimeout(() => resolve(Object.values(EmployeesMock)), 2500);
    });

export const filterEmployees = (name) =>
    new Promise((resolve) => {
        setTimeout(() => resolve(Object.values(EmployeesMock.filter(x => x.surname.toLowerCase().includes(name.toLowerCase())))), 2500);
    });

export const updateEmployee = (employee) =>
    new Promise((resolve) => {
        setTimeout(() => resolve("Employee updated!"), 2500);
    });

export const deleteEmployee = (id) =>
    new Promise((resolve) => {
        setTimeout(() => resolve("Employee deleted!"), 2500);
    });
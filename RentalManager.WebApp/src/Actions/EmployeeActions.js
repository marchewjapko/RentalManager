import {EmployeesMock} from "../Mocks/EmployeesMock";
export const getAllEmployees = () =>
    new Promise((resolve) => {
        setTimeout(() => resolve(Object.values(EmployeesMock)), 2500);
    });

export const filterEmployees = (name) =>
    new Promise((resolve) => {
        setTimeout(() => resolve(Object.values(EmployeesMock.filter(x => x.surname.toLowerCase().includes(name.toLowerCase())))), 2500);
    });
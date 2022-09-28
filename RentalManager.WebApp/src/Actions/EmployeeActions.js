import {EmployeesMock} from "../Mocks/EmployeesMock";

export const getAllEmployees = () => new Promise((resolve) => {
    setTimeout(() => (resolve(Object.values(EmployeesMock)), console.log("Got all employees")), 2500);
});

export const addEmployee = (employee) => new Promise((resolve) => {
    setTimeout(() => (resolve("Employee added!"), console.log("Added employee:", employee)), 2500);
});

export const filterEmployees = (name) => new Promise((resolve) => {
    setTimeout(() => (resolve(Object.values(EmployeesMock.filter(x => x.surname.toLowerCase().includes(name.toLowerCase())))), console.log("Filtered employees")), 2500);
});

export const updateEmployee = (employee) => new Promise((resolve) => {
    setTimeout(() => (resolve("Employee updated!"), console.log("Updated employee:", employee)), 2500);
});

export const deleteEmployee = (id) => new Promise((resolve) => {
    setTimeout(() => (resolve("Employee deleted!"), console.log("Deleted employee:", id)), 2500);
});
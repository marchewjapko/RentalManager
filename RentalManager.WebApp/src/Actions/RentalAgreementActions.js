import {RentalAgreementMock} from "../Mocks/RentalAgreementMock";
import dayjs from "dayjs";

export const getAllAgreements = () => new Promise((resolve) => {
    setTimeout(() => (resolve(Object.values(RentalAgreementMock)), console.log("Got all agreements")), 2500);
});

export const updateRentalAgreement = (rentalAgreement) => new Promise((resolve) => {
    setTimeout(() => (resolve("Rental agreement updated!"), console.log("Updated agreement equipment:", rentalAgreement)), 2500);
});

export const deleteRentalAgreement = (id) => new Promise((resolve) => {
    setTimeout(() => (resolve("Rental agreement deleted!"), console.log("Deleted agreement equipment:", id)), 2500);
});

export const addRentalAgreement = (rentalAgreement) => new Promise((resolve) => {
    setTimeout(() => (resolve("Rental agreement added!"), console.log("Added rental agreement:", rentalAgreement)), 2500);
});

export const filterAgreements = (searchParams) => new Promise((resolve) => {
    setTimeout(() => (resolve(Object.values(RentalAgreementMock.filter(x => x.client.surname.toLowerCase().includes(searchParams.surname.toLowerCase()) && x.client.phone.toLowerCase().includes(searchParams.phone.toLowerCase()) && x.client.city.toLowerCase().includes(searchParams.city.toLowerCase()) && x.client.street.toLowerCase().includes(searchParams.street.toLowerCase()) && ((searchParams.onlyUnpaid && dayjs(x.validUntil).diff(dayjs(), 'day') < 0) || !searchParams.onlyUnpaid) && (x.isActive === searchParams.onlyActive || !searchParams.onlyActive)))), console.log("Filtered clients, params:", searchParams)), 2500);
});
import {RentalAgreementMock} from "../Mocks/RentalAgreementMock";

export const getAllAgreements = () => new Promise((resolve) => {
    setTimeout(() => (resolve(Object.values(RentalAgreementMock)), console.log("Got all agreements")), 2500);
});
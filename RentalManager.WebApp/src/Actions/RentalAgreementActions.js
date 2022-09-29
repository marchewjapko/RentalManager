import {RentalAgreementMock} from "../Mocks/RentalAgreementMock";

// export const getAllClients = () => new Promise((resolve) => {
//     setTimeout(() => (resolve(Object.values(RentalAgreementMock)), console.log("Got all rental agreements")), 1);
// });

export const getAllClients = () =>  {
    return RentalAgreementMock
};
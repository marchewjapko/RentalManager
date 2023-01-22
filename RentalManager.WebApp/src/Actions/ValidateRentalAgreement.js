import dayjs from "dayjs";

export default function ValidateRentalAgreement(rentalAgreement) {
    let result = []
    if (rentalAgreement.deposit.length === 0) {
        result.push("noDeposit")
    }
    if (!rentalAgreement.transportTo || rentalAgreement.transportTo.length === 0) {
        result.push("noTransportTo")
    }
    if (!rentalAgreement.validUntil || rentalAgreement.validUntil.length === 0) {
        result.push("noValidUntil")
    } else if(!dayjs(rentalAgreement.validUntil).isValid()) {
        result.push("invalidValidUntil")
    }
    if (!rentalAgreement.dateAdded || rentalAgreement.dateAdded.length === 0) {
        result.push("noDateAdded")
    } else if(!dayjs(rentalAgreement.dateAdded).isValid()) {
        result.push("invalidDateAdded")
    }
    if(!rentalAgreement.employee) {
        result.push("noEmployee")
    }
    if(!rentalAgreement.rentalEquipment || rentalAgreement.rentalEquipment.length === 0) {
        result.push("noRentalEquipment")
    }
    return (result)
}
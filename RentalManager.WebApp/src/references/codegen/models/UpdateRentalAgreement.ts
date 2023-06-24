/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

export type UpdateRentalAgreement = {
    employeeId?: number;
    isActive?: boolean;
    clientId?: number;
    rentalEquipmentIds?: Array<number> | null;
    comment?: string | null;
    deposit?: number;
    transportFrom?: number | null;
    transportTo?: number;
    dateAdded?: string;
};

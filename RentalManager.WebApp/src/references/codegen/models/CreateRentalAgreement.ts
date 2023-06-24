/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { CreatePayment } from './CreatePayment';

export type CreateRentalAgreement = {
    employeeId?: number;
    isActive?: boolean;
    clientId?: number;
    rentalEquipmentIds?: Array<number> | null;
    comment?: string | null;
    deposit?: number;
    transportFrom?: number | null;
    transportTo?: number;
    dateAdded?: string;
    payments?: Array<CreatePayment> | null;
};

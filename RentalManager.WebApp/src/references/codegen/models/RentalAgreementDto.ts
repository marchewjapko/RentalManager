/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { ClientDto } from './ClientDto';
import type { EmployeeDto } from './EmployeeDto';
import type { PaymentDto } from './PaymentDto';
import type { RentalEquipmentDto } from './RentalEquipmentDto';

export type RentalAgreementDto = {
    id?: number;
    employee?: EmployeeDto;
    isActive?: boolean;
    client?: ClientDto;
    rentalEquipment?: Array<RentalEquipmentDto> | null;
    payments?: Array<PaymentDto> | null;
    comment?: string | null;
    deposit?: number;
    transportFrom?: number | null;
    transportTo?: number;
    dateAdded?: string;
};

/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { Client } from './Client';
import type { Equipment } from './Equipment';
import type { Payment } from './Payment';

export type Agreement = {
    client: Client;
    equipments: Array<Equipment>;
    payments: Array<Payment>;
    transport_from?: number;
    transport_to: number;
    deposit: number;
};

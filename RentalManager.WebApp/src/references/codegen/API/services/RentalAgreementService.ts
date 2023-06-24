/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CreateRentalAgreement } from '../models/CreateRentalAgreement';
import type { RentalAgreementDto } from '../models/RentalAgreementDto';
import type { UpdateRentalAgreement } from '../models/UpdateRentalAgreement';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class RentalAgreementService {

    /**
     * @returns RentalAgreementDto Success
     * @throws ApiError
     */
    public static postRentalAgreement({
requestBody,
}: {
requestBody?: CreateRentalAgreement,
}): CancelablePromise<RentalAgreementDto> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/RentalAgreement',
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * @returns RentalAgreementDto Success
     * @throws ApiError
     */
    public static getRentalAgreement({
clientId,
surname,
phoneNumber,
city,
street,
rentalEquipmentId,
rentalEquipmentName,
employeeId,
onlyUnpaid = false,
from,
to,
}: {
clientId?: number,
surname?: string,
phoneNumber?: string,
city?: string,
street?: string,
rentalEquipmentId?: number,
rentalEquipmentName?: string,
employeeId?: number,
onlyUnpaid?: boolean,
from?: string,
to?: string,
}): CancelablePromise<Array<RentalAgreementDto>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/RentalAgreement',
            query: {
                'clientId': clientId,
                'surname': surname,
                'phoneNumber': phoneNumber,
                'city': city,
                'street': street,
                'rentalEquipmentId': rentalEquipmentId,
                'rentalEquipmentName': rentalEquipmentName,
                'employeeId': employeeId,
                'onlyUnpaid': onlyUnpaid,
                'from': from,
                'to': to,
            },
        });
    }

    /**
     * @returns any Success
     * @throws ApiError
     */
    public static deleteRentalAgreement({
id,
}: {
id: number,
}): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/RentalAgreement/{id}',
            path: {
                'id': id,
            },
        });
    }

    /**
     * @returns RentalAgreementDto Success
     * @throws ApiError
     */
    public static getRentalAgreement1({
id,
}: {
id: number,
}): CancelablePromise<RentalAgreementDto> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/RentalAgreement/{id}',
            path: {
                'id': id,
            },
        });
    }

    /**
     * @returns RentalAgreementDto Success
     * @throws ApiError
     */
    public static putRentalAgreement({
id,
requestBody,
}: {
id: number,
requestBody?: UpdateRentalAgreement,
}): CancelablePromise<RentalAgreementDto> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/RentalAgreement/{id}',
            path: {
                'id': id,
            },
            body: requestBody,
            mediaType: 'application/json',
        });
    }

}

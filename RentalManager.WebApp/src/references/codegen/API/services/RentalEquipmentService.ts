/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CreateRentalEquipment } from '../models/CreateRentalEquipment';
import type { RentalEquipmentDto } from '../models/RentalEquipmentDto';
import type { UpdateRentalEquipment } from '../models/UpdateRentalEquipment';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class RentalEquipmentService {

    /**
     * @returns RentalEquipmentDto Success
     * @throws ApiError
     */
    public static postRentalEquipment({
requestBody,
}: {
requestBody?: CreateRentalEquipment,
}): CancelablePromise<RentalEquipmentDto> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/RentalEquipment',
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * @returns RentalEquipmentDto Success
     * @throws ApiError
     */
    public static getRentalEquipment({
name,
from,
to,
}: {
name?: string,
from?: string,
to?: string,
}): CancelablePromise<Array<RentalEquipmentDto>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/RentalEquipment',
            query: {
                'name': name,
                'from': from,
                'to': to,
            },
        });
    }

    /**
     * @returns any Success
     * @throws ApiError
     */
    public static deleteRentalEquipment({
id,
}: {
id: number,
}): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/RentalEquipment/{id}',
            path: {
                'id': id,
            },
        });
    }

    /**
     * @returns RentalEquipmentDto Success
     * @throws ApiError
     */
    public static getRentalEquipment1({
id,
}: {
id: number,
}): CancelablePromise<RentalEquipmentDto> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/RentalEquipment/{id}',
            path: {
                'id': id,
            },
        });
    }

    /**
     * @returns RentalEquipmentDto Success
     * @throws ApiError
     */
    public static putRentalEquipment({
id,
requestBody,
}: {
id: number,
requestBody?: UpdateRentalEquipment,
}): CancelablePromise<RentalEquipmentDto> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/RentalEquipment/{id}',
            path: {
                'id': id,
            },
            body: requestBody,
            mediaType: 'application/json',
        });
    }

}

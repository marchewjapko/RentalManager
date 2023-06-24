/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ClientDto } from '../models/ClientDto';
import type { CreateClient } from '../models/CreateClient';
import type { UpdateClient } from '../models/UpdateClient';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class ClientService {

    /**
     * @returns ClientDto Success
     * @throws ApiError
     */
    public static postClient({
requestBody,
}: {
requestBody?: CreateClient,
}): CancelablePromise<ClientDto> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/Client',
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * @returns ClientDto Success
     * @throws ApiError
     */
    public static getClient({
name,
surname,
phoneNumber,
email,
idCard,
city,
street,
from,
to,
}: {
name?: string,
surname?: string,
phoneNumber?: string,
email?: string,
idCard?: string,
city?: string,
street?: string,
from?: string,
to?: string,
}): CancelablePromise<Array<ClientDto>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/Client',
            query: {
                'name': name,
                'surname': surname,
                'phoneNumber': phoneNumber,
                'email': email,
                'idCard': idCard,
                'city': city,
                'street': street,
                'from': from,
                'to': to,
            },
        });
    }

    /**
     * @returns any Success
     * @throws ApiError
     */
    public static deleteClient({
id,
}: {
id: number,
}): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/Client/{id}',
            path: {
                'id': id,
            },
        });
    }

    /**
     * @returns ClientDto Success
     * @throws ApiError
     */
    public static getClient1({
id,
}: {
id: number,
}): CancelablePromise<ClientDto> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/Client/{id}',
            path: {
                'id': id,
            },
        });
    }

    /**
     * @returns ClientDto Success
     * @throws ApiError
     */
    public static putClient({
id,
requestBody,
}: {
id: number,
requestBody?: UpdateClient,
}): CancelablePromise<ClientDto> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/Client/{id}',
            path: {
                'id': id,
            },
            body: requestBody,
            mediaType: 'application/json',
        });
    }

}

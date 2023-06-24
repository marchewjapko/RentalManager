/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CreateEmployee } from '../models/CreateEmployee';
import type { EmployeeDto } from '../models/EmployeeDto';
import type { UpdateEmployee } from '../models/UpdateEmployee';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class EmployeeService {

    /**
     * @returns EmployeeDto Success
     * @throws ApiError
     */
    public static postEmployee({
requestBody,
}: {
requestBody?: CreateEmployee,
}): CancelablePromise<EmployeeDto> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/Employee',
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * @returns EmployeeDto Success
     * @throws ApiError
     */
    public static getEmployee({
name,
from,
to,
}: {
name?: string,
from?: string,
to?: string,
}): CancelablePromise<Array<EmployeeDto>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/Employee',
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
    public static deleteEmployee({
id,
}: {
id: number,
}): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/Employee/{id}',
            path: {
                'id': id,
            },
        });
    }

    /**
     * @returns EmployeeDto Success
     * @throws ApiError
     */
    public static getEmployee1({
id,
}: {
id: number,
}): CancelablePromise<EmployeeDto> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/Employee/{id}',
            path: {
                'id': id,
            },
        });
    }

    /**
     * @returns EmployeeDto Success
     * @throws ApiError
     */
    public static putEmployee({
id,
requestBody,
}: {
id: number,
requestBody?: UpdateEmployee,
}): CancelablePromise<EmployeeDto> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/Employee/{id}',
            path: {
                'id': id,
            },
            body: requestBody,
            mediaType: 'application/json',
        });
    }

}

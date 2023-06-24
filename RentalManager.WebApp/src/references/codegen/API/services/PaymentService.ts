/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CreatePayment } from '../models/CreatePayment';
import type { PaymentDto } from '../models/PaymentDto';
import type { UpdatePayment } from '../models/UpdatePayment';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class PaymentService {

    /**
     * @returns PaymentDto Success
     * @throws ApiError
     */
    public static postPayment({
rentalAgreementId,
requestBody,
}: {
rentalAgreementId?: number,
requestBody?: CreatePayment,
}): CancelablePromise<PaymentDto> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/Payment',
            query: {
                'rentalAgreementId': rentalAgreementId,
            },
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * @returns PaymentDto Success
     * @throws ApiError
     */
    public static getPayment({
rentalAgreementId,
method,
from,
to,
}: {
rentalAgreementId?: number,
method?: string,
from?: string,
to?: string,
}): CancelablePromise<Array<PaymentDto>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/Payment',
            query: {
                'rentalAgreementId': rentalAgreementId,
                'method': method,
                'from': from,
                'to': to,
            },
        });
    }

    /**
     * @returns any Success
     * @throws ApiError
     */
    public static deletePayment({
id,
}: {
id: number,
}): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/Payment/{id}',
            path: {
                'id': id,
            },
        });
    }

    /**
     * @returns PaymentDto Success
     * @throws ApiError
     */
    public static getPayment1({
id,
}: {
id: number,
}): CancelablePromise<PaymentDto> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/Payment/{id}',
            path: {
                'id': id,
            },
        });
    }

    /**
     * @returns PaymentDto Success
     * @throws ApiError
     */
    public static putPayment({
id,
requestBody,
}: {
id: number,
requestBody?: UpdatePayment,
}): CancelablePromise<PaymentDto> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/Payment/{id}',
            path: {
                'id': id,
            },
            body: requestBody,
            mediaType: 'application/json',
        });
    }

}

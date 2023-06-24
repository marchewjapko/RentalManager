/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Agreement } from '../models/Agreement';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class DefaultService {

    /**
     * Generate Document
     * @returns any Successful Response
     * @throws ApiError
     */
    public static generateDocumentDocumentsGenerateDocumentPost({
requestBody,
}: {
requestBody: Agreement,
}): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/documents/generate_document',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                422: `Validation Error`,
            },
        });
    }

}

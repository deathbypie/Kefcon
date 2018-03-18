import { authHeader, config } from '../_helpers';

export const apiService = {
    makeRequest
};

// make an api request. A get request only needs the url specified.
function makeRequest(url, requestMethod = 'GET', requestBody = null, requestHeader = authHeader(), responseHandler = () => {}) {
    const requestOptions = {
        method: requestMethod,
        headers: requestHeader,
        body: requestBody
    };
    
    return fetch(config.apiUrl + url, requestOptions).then(handleResponse, handleError);
}

function handleError(error) {
    return Promise.reject(error && error.message);
}

function handleResponse(response) {
    return new Promise((resolve, reject) => {
        if (response.ok) {
            // return json if it was returned in the response
            var contentType = response.headers.get("content-type");
            if (contentType && contentType.includes("application/json")) {
                response.json().then(json => resolve(json));
            } else {
                resolve();
            }
        } else {
            // return error message from response body
            response.text().then(text => reject(text));
        }
    });
}
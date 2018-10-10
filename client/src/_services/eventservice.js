import { authHeader, config } from '../_helpers';
import { apiService } from './apiservice';

export const eventService = {
    getAll,
    getById,
    create,
    update
};

function getAll() {
    return apiService.makeRequest('/event');
}

function getById(id) {
    return apiService.makeRequest('/event/' + id);
}

function create(event) {
    const headers = { ...authHeader(), 'Content-Type': 'application/json' };
    const body = JSON.stringify(event);

    return apiService.makeRequest('/event/create', 'POST', body, headers );
}

function update(event) {
    const headers = { ...authHeader(), 'Content-Type': 'application/json' };
    const body = JSON.stringify(event);

    return apiService.makeRequest('/event/update', 'PUT', body, headers)
}
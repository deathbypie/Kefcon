import { authHeader, config } from '../_helpers';
import { apiService } from './apiservice';

export const gameService = {
    getAll,
    getById,
    create,
    update,
    delete: _delete
};

function getAll() {
    return apiService.makeRequest('/game');
}

function getById(id) {
    return apiService.makeRequest('/game/' + id);
}

function create(game) {
    const headers = { ...authHeader(), 'Content-Type': 'application/json' };
    const body = JSON.stringify(game);

    return apiService.makeRequest('/game/create', 'POST', body, headers );
}

function update(game) {
    const headers = { ...authHeader(), 'Content-Type': 'application/json' };
    const body = JSON.stringify(game);

    return apiService.makeRequest('/game/update', 'PUT', body, headers)
}

function _delete(id) {
    return apiService.makeRequest('/game/delete' + id, "DELETE");
}
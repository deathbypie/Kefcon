import { authHeader, config } from '../_helpers';
import { apiService } from './apiservice';

export const userService = {
    login,
    logout,
    register,
    getAll,
    getById,
    update,
    delete: _delete
};

const anonymousHeaders = { 'Content-Type': 'application/json' };

function login(email, password) {
    const body = JSON.stringify({ email, password });

    const handleResponse = (user) => {
        // login successful if there's a jwt token in the response
        if (user && user.token) {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('user', JSON.stringify(user));
        }

        return user;
    }

    return apiService.makeRequest('/users/login', 'POST', body, anonymousHeaders).then(handleResponse);

    // return fetch(config.apiUrl + '/users/login', requestOptions)
    //     .then(handleResponse, handleError)
    //     .then(user => {
    //         // login successful if there's a jwt token in the response
    //         if (user && user.token) {
    //             // store user details and jwt token in local storage to keep user logged in between page refreshes
    //             localStorage.setItem('user', JSON.stringify(user));
    //         }

    //         return user;
    //     });
}

function logout() {
    const handleResponse = () => {
        localStorage.removeItem('user');
    }

    apiService.makeRequest('/users/logout').then(handleResponse);;
}

function getAll() {
    return apiService.makeRequest('/users');
}

function getById(id) {
    return apiService.makeRequest('/users/' + id);
}

function register(user) {
    const body = JSON.stringify(user);

    return apiService.makeRequest('/users/register', 'POST', body, anonymousHeaders );
}

function update(user) {
    const headers = { ...authHeader(), 'Content-Type': 'application/json' };
    const body = JSON.stringify(user);

    return apiService.makeRequest('/users', 'PUT', body, headers)
}

// prefixed function name with underscore because delete is a reserved word in javascript
// probably won't need/want this
function _delete(id) {
    return apiService.makeRequest('/users/' + id, "DELETE");
}


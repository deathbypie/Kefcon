import { gameConstants } from '../_constants';
import { gameService } from '../_services';
import { alertActions } from './';

export const gameActions = {
    create,
    update,
    getAll,
    delete: _delete,
};

function create(game) {
    return dispatch => {
        dispatch(request(game));

        return gameService.create(game)
            .then(
                game => { 
                    dispatch(success(game));
                    dispatch(alertActions.success(game.name + ' added.'));
                },
                error => {
                    dispatch(failure(error));
                    dispatch(alertActions.error(error));
                }
            );
    };

    function request(game) { return { type: gameConstants.CREATE_REQUEST, game } }
    function success(game) { return { type: gameConstants.CREATE_SUCCESS, game } }
    function failure(error) { return { type: gameConstants.CREATE_FAILURE, error } }
}

function update(game) {
    return dispatch => {
        dispatch(request(game));

         return gameService.update(game)
            .then(
                game => { 
                    dispatch(success(game));
                    dispatch(alertActions.success(game.name + ' updated.'));
                },
                error => {
                    dispatch(failure(error));
                    dispatch(alertActions.error(error));
                }
            );
    };

    function request(game) { return { type: gameConstants.UPDATE_REQUEST, game } }
    function success(game) { return { type: gameConstants.UPDATE_SUCCESS, game } }
    function failure(error) { return { type: gameConstants.UPDATE_FAILURE, error } }
}

function getAll() {
    return dispatch => {
        dispatch(request());

        return gameService.getAll()
            .then(
                games => dispatch(success(games)),
                error => dispatch(failure(error))
            );
    };

    function request() { return { type: gameConstants.GETALL_REQUEST } }
    function success(games) { return { type: gameConstants.GETALL_SUCCESS, games } }
    function failure(error) { return { type: gameConstants.GETALL_FAILURE, error } }
}

function _delete(id) {
    return dispatch => {
        dispatch(request(id));

        return gameService.delete(id)
            .then(
                () => { 
                    dispatch(success(id));
                },
                error => {
                    dispatch(failure(id, error));
                }
            );
    };

    function request(id) { return { type: gameConstants.DELETE_REQUEST, id } }
    function success(id) { return { type: gameConstants.DELETE_SUCCESS, id } }
    function failure(id, error) { return { type: gameConstants.DELETE_FAILURE, id, error } }
}
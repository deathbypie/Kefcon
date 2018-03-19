import { gameConstants } from '../_constants';

export function games(state = {}, action) {
  switch (action.type) {
    case gameConstants.CREATE_REQUEST:
    case gameConstants.UPDATE_REQUEST:
      return {
        ...state,
        updatingGame: true
      };
    case gameConstants.CREATE_SUCCESS:
      return {
        ...state,
        items: [...state.items, action.game]
      }
    case gameConstants.UPDATE_SUCCESS:
    const updatedItems = state.items.map((game) => {
      if (game.id === action.game.id) {

        const gameCopy = {...action.game};
        return gameCopy
      }
      return game;
    });

    return {
      ...state,
      items: updatedItems
    };
    case gameConstants.CREATE_FAILURE:
    case gameConstants.UPDATE_FAILURE:
      return {
        ...state,
        error: action.error
      };
    case gameConstants.GETALL_REQUEST:
      return {
        loading: true
      };
    case gameConstants.GETALL_SUCCESS:
      return {
        items: action.games
      };
    case gameConstants.GETALL_FAILURE:
      return {
        error: action.error
      };
    case gameConstants.DELETE_REQUEST:
      // add 'deleting:true' property to game being deleted
      return {
        ...state,
        items: state.items.map(game =>
          game.id === action.id
            ? { ...game, deleting: true }
            : game
        )
      };
    case gameConstants.DELETE_SUCCESS:
      // remove deleted game from state
      return {
        items: state.items.filter(game => game.id !== action.id)
      };
    case gameConstants.DELETE_FAILURE:
      // remove 'deleting:true' property and add 'deleteError:[error]' property to game 
      return {
        ...state,
        items: state.items.map(game => {
          if (game.id === action.id) {
            // make copy of game without 'deleting:true' property
            const { deleting, ...gameCopy } = game;
            // return copy of game with 'deleteError:[error]' property
            return { ...gameCopy, deleteError: action.error };
          }

          return game;
        })
      };
    default:
      return state
  }
}
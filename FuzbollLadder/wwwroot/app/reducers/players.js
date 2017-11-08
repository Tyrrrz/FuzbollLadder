import { actions } from '../actions';

export default (state = { players: [] }, action) => {
    switch (action.type) {
        case actions.PLAYERS_LOADED:
            if (!action.players || action.players.lenght === 0) {
                return state;
            }
            return {
                players: state.players.concat(action.players),
            };
        case actions.PLAYER_REGISTERED:
            return {
                players: state.players.concat([action.player]),
            };
        default:
            return state;
    }
    
}
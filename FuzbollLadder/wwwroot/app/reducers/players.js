import { actions } from '../actions';

export default (state = { players: [] }, action) => {
    switch (action.type) {
        case actions.PLAYERS_LOADED:
            if (!action.players || action.players.lenght === 0) {
                return state;
            }
            console.log(action.players)
            return {
                players: state.players.concat(action.players),
            };
        default:
            return state;
    }
    
}
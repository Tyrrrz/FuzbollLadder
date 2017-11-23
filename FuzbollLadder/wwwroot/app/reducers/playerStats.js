import { actions } from '../actions';

export default (state = { playerStats: [] }, action) => {
    switch (action.type) {
        case actions.PLAYER_STATS_LOADED:
            if (!action.stats || action.stats.lenght === 0) {
                return state;
            }
            return {
                playerStats: state.playerStats.concat(action.stats),
            };
        default:
            return state;
    }
    
}
import { actions } from '../actions';

export default (state = { matches: [] }, action) => {
    switch (action.type) {
        case actions.MATCHES_LOADED:
            if (!action.matches || action.matches.lenght === 0) {
                return state;
            }
            return {
                matches: state.matches.concat(action.matches),
            };
        case actions.MATCH_DELETED:
            return {
                matches: state.matches.filter(item => item.id !== action.match)
            }
        case actions.MATCH_ADDED:
            return {
                matches: state.matches.concat([action.match])
            }
        default:
            return state;
    }
    
}
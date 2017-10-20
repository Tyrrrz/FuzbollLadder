import { actions } from '../actions';

export default (state = { matches: [] }, action) => {
    switch (action.type) {
        case actions.MATCHES_LOADED:
            console.log(actions)
            if (!action.matches || action.matches.lenght === 0) {
                return state;
            }
            return {
                matches: state.matches.concat(action.matches),
            };
        default:
            return state;
    }
    
}
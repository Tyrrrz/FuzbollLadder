import { combineReducers } from 'redux'
import players from './players';
import matches from './matches';

const FuzbollApp = combineReducers({
    players,
    matches
})

export default FuzbollApp
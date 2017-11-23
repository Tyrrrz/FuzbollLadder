import { combineReducers } from 'redux'
import players from './players'
import matches from './matches'
import playerStats from './playerStats'

const FuzbollApp = combineReducers({
    players,
    matches,
    playerStats
})

export default FuzbollApp
import { connect } from 'react-redux'
import actions from '../actions'
import PlayerStats from '../components/PlayerStats/PlayerStats'
import PlayersApi from '../services/PlayersApi'
import store from '../store/index'

const mapStateToProps = state => state.playerStats

const PlayerStatsContainer = connect(
    mapStateToProps
)(PlayerStats)

PlayersApi.loadPlayerStats(store.dispatch, 0)

export default PlayerStatsContainer 


import { connect } from 'react-redux'
import actions from '../actions'
import Players from '../components/Players'
import store from '../store/index'
import PlayersApi from '../services/PlayersApi'

const mapStateToProps = state => state.players

const PlayersContainer = connect(
  mapStateToProps
)(Players)

PlayersApi.loadPlayers(store.dispatch, 0)

export default PlayersContainer


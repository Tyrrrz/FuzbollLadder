import { connect } from 'react-redux'
import actions from '../actions'
import Players from '../components/Players'

const mapStateToProps = state => state.players;

const PlayersContainer = connect(
  mapStateToProps
)(Players)

export default PlayersContainer


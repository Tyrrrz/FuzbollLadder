import { connect } from 'react-redux'
import actions from '../actions'
import Matches from '../components/Matches'
import MatchesApi from '../services/MatchesApi'
import store from '../store/index'

const mapStateToProps = state => state.matches

const MatchesContainer = connect(
  mapStateToProps
)(Matches)

MatchesApi.loadMatches(store.dispatch, 0)

export default MatchesContainer


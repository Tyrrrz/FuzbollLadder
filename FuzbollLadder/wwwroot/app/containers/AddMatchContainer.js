import { connect } from 'react-redux'
import actions from '../actions'
import AddMatch from '../components/AddMatch/AddMatch'
import MatchesApi from '../services/MatchesApi'


const mapStateToProps = state => state.players

const mapDispatchToProps = dispatch => (
  {
    onAddMatch: (winner1, winner2, loser1, loser2) => MatchesApi.addMatch(dispatch, winner1, winner2, loser1, loser2)
  }
)

const AddMatchContainer = connect(
  mapStateToProps,
  mapDispatchToProps
)(AddMatch)

export default AddMatchContainer


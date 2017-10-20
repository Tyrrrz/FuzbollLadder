import { connect } from 'react-redux'
import actions from '../actions'
import AddMatch from '../components/AddMatch'


const mapStateToProps = state => state.matches || {}

const mapDispatchToProps = dispatch => (
  {
    onAddMatch: (winners, losers) => MatchesApi.addMatch(dispatch, winners, losers)
  }
)

const AddMatchContainer = connect(
  mapStateToProps,
  mapDispatchToProps
)(AddMatch)

export default AddMatchContainer


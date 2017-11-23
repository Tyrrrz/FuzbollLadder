import { connect } from 'react-redux'
import actions from '../actions'
import PlayersApi from '../services/PlayersApi'
import RegisterPlayer from '../components/RegisterPlayer/RegisterPlayer'

const mapStateToProps = state => state.players

const mapDispatchToProps = dispatch => (
  {
    onRegisterPlayer: name => PlayersApi.registerPlayer(dispatch, name)
  }
); 

const RegisterPlayerContainer = connect(
  mapStateToProps,
  mapDispatchToProps
)(RegisterPlayer)

export default RegisterPlayerContainer


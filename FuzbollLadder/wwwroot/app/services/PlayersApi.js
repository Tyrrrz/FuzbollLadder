import actions from '../actions';

export default {
    loadPlayers: (dispatch, index = 0) => fetch(`api/players/all`)
        .then(x => x.json())
        .then(x => dispatch(actions.PLAYERS_LOADED(x)))
};

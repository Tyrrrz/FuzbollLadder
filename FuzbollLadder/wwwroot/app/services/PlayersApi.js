import actions from '../actions';

export default {
    loadPlayers: (dispatch, index = 0) => fetch(`/api/players/all`)
        .then(x => x.json())
        .then(x => dispatch(actions.PLAYERS_LOADED(x))),
    registerPlayer: (dispatch, name = null) => {
        fetch(`/api/players/add`, {
            method: "POST",
            headers: {
                'Accept': 'application/json, text/plain, */*',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({Name: name})
        })
        .then(x => x.json())
        .then(x => dispatch(actions.PLAYER_REGISTERED(x)));
    },
    loadPlayerStats: (dispatch, index = 0) => {
        fetch(`/api/playerstats/index`, {
            method: "GET",
            headers: {
                'Accept': 'application/json, text/plain, */*',
                'Content-Type': 'application/json'
            }
        })
        .then(x => x.json())
        .then(x => dispatch(actions.PLAYER_STATS_LOADED(x)));
    },
};

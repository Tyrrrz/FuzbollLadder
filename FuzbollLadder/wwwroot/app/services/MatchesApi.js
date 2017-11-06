import actions from '../actions';

export default {
    loadMatches: (dispatch, index = 0) => fetch(`/api/matches/all`)
        .then(x => x.json())
        .then(x => dispatch(actions.MATCHES_LOADED(x))),
    addMatch: (dispatch, winners = null, losers = null) => {
        fetch(`/api/matches/add`, {
            method: "POST",
            headers: {
                'Accept': 'application/json, text/plain, */*',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                WinnerNamesCsv: winners,
                LoserNamesCsv: losers
            })
        })
        .then(x => x.json())
        .then(x => dispatch(actions.MATCH_ADDED(x)));
    },
    deleteMatch: (dispatch, matchId) => {
        fetch(`/api/matches/delete`, {
            method: "DELETE",
            headers: {
                'Accept': 'application/json, text/plain, */*',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                id: matchId
            })
        })
        .then(x => x.json())
        .then(x => dispatch(actions.MATCH_DELETED(x)));
    },
};

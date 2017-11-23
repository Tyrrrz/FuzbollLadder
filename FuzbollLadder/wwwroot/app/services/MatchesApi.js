import actions from '../actions';

export default {
    loadMatches: (dispatch, index = 0) => fetch(`/api/matches/all`)
        .then(x => x.json())
        .then(x => dispatch(actions.MATCHES_LOADED(x))),
    addMatch: (dispatch, winner1 = null, winner2 = null, loser1 = null, loser2 = null) => {
        fetch(`/api/matches/add`, {
            method: "POST",
            headers: {
                'Accept': 'application/json, text/plain, */*',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                WinnerName1: winner1,
                WinnerName2: winner2,
                LoserName1: loser1,
                LoserName2: loser2
            })
        })
        .then(x => x.json())
        .then(x => dispatch(actions.MATCH_ADDED(x)));
    },
    deleteMatch: (dispatch, matchId) => {
        fetch(`/api/matches/${matchId}`, {
            method: "DELETE",
            headers: {
                'Accept': 'application/json, text/plain, */*',
                'Content-Type': 'application/json'
            }
        })
        .then(x => x)
        .then(x => dispatch(actions.MATCH_DELETED(matchId)));
    },
};

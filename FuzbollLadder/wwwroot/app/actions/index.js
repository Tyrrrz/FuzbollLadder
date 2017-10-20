export const actions = {
        'PLAYERS_LOADED': Symbol('PLAYERS_LOADED'),
        'PLAYER_REGISTERED': Symbol('PLAYER_REGISTERED'),
        'MATCHES_LOADED': Symbol('MATCHES_LOADED'),
        'MATCH_ADDED': Symbol('MATCH_ADDED')
    };
  
    export default {
        PLAYERS_LOADED: (players) => ({
            type: actions.PLAYERS_LOADED,
            players
        }),
        PLAYERS_REGISTERED: (player) => ({
            type: actions.PLAYER_REGISTERED,
            player
        }),
        MATCHES_LOADED: (matches) => ({
            type: actions.MATCHES_LOADED,
            matches
        }),
        MATCH_ADDED: (match) => ({
            type: actions.MATCH_ADDED,
            match
        }),
    };
  
  
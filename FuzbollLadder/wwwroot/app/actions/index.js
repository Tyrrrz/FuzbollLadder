export const actions = {
        'PLAYERS_LOADED': Symbol('PLAYERS_LOADED'),
        'PLAYER_REGISTERED': Symbol('PLAYER_REGISTERED'),
        'PLAYER_STATS_LOADED': Symbol('PLAYER_STATS_LOADED'),
        'MATCHES_LOADED': Symbol('MATCHES_LOADED'),
        'MATCH_ADDED': Symbol('MATCH_ADDED'),
        'MATCH_DELETED': Symbol('MATCH_DELETED')
    };
  
    export default {
        PLAYERS_LOADED: (players) => ({
            type: actions.PLAYERS_LOADED,
            players
        }),
        PLAYER_REGISTERED: (player) => ({
            type: actions.PLAYER_REGISTERED,
            player
        }),
        PLAYER_STATS_LOADED: (stats) => ({
            type: actions.PLAYER_STATS_LOADED,
            stats
        }),
        MATCHES_LOADED: (matches) => ({
            type: actions.MATCHES_LOADED,
            matches
        }),
        MATCH_ADDED: (match) => ({
            type: actions.MATCH_ADDED,
            match
        }),
        MATCH_DELETED: (match) => ({
            type: actions.MATCH_DELETED,
            match
        }),
    };
  
  
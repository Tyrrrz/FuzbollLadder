export const actions = {
        'PLAYERS_LOADED': Symbol('PLAYERS_LOADED')
    };
  
    export default {
        PLAYERS_LOADED: (players) => ({
            type: actions.PLAYERS_LOADED,
            players
        })
    };
  
  
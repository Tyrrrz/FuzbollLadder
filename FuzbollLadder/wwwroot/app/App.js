import React from 'react';
import ReactDOM from 'react-dom';
import PlayerContainer from './containers/PlayerContainer';

export default class App extends React.Component {
    render() {
        return (
                <div className="players-container">
                    <h1 className="mb-5 text-center">Fuzboll Ladder</h1>
                    <PlayerContainer />
                </div>
            );
    }
}

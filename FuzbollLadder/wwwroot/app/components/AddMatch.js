import React from 'react';

export default class AddMatch extends React.Component {
    render() {     
        let winners = null;
        let losers = null;
        return (
            <div>
                <div>Submit match</div>
                <h3>Winners</h3>
                <input type="text" ref={(input) => { winners = input; }} />
                <h3>Losers</h3>
                <input type="text" ref={(input) => { losers = input; }} />
                <button onClick={(e) => { e.preventDefault(); this.props.onAddMatch(winners.value, losers.value); }}>Submit</button>
            </div>
        )
    }
}
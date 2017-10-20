import React from 'react';
import Moment from 'react-moment';

class Match extends React.Component {
    render() {
        let match = Object.assign({}, this.props);
        match.ratingDelta = (match.ratingDelta).toFixed(0)
        return (
            <tr className="player-item">
                <th className="rank" scope="row">
                    {match.id}
                </th>
                <td className="date">
                    <Moment format="YYYY.MM.DD">
                        { match.date }
                    </Moment>
                </td>
                <td className="winners">
                    <div>{match.winners[0].name},</div>
                    <div>{match.winners[1].name}</div>
                </td>
                <td className="losers">
                    <div>{match.losers[0].name},</div>
                    <div>{match.losers[1].name}</div>
                </td>
                <td className="delta">
                    ±{match.ratingDelta}
                </td>
                <td className="delete-button-container">
                    <button className="btn">DELETE</button>
                </td>
            </tr>
        );
    }
}

export default Match;

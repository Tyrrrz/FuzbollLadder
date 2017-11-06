import React from 'react';
import Moment from 'react-moment';

export default class Match extends React.Component {
    render() {
        const { onDeleteMatch, ratingDelta, id, date, winners = [], losers = [] } = this.props
        console.log(onDeleteMatch)
        return (
            <tr className="player-item">
                <th className="rank" scope="row">
                    {id}
                </th>

                <td className="date">
                    <Moment format="YYYY.MM.DD">
                        {date}
                    </Moment>
                </td>

                <td className="winners">
                    <div>{winners[0].name},</div>
                    <div>{winners[1].name}</div>
                </td>

                <td className="losers">
                    <div>{losers[0].name},</div>
                    <div>{losers[1].name}</div>
                </td>

                <td className="delta">
                    ±{ratingDelta.toFixed(0)}
                </td>

                <td className="delete-button-container">
                    <button 
                        type="submit" 
                        className="btn btn-warning btn-sm"
                        onClick={this._onSubmit}>DELETE</button>
                </td>
            </tr>
        )
    }
}
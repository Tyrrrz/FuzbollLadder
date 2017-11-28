import React from 'react'
import Moment from 'react-moment'
import NotificationManager from 'react-notifications'

export default class Match extends React.Component {
    render() {
        const { ratingDelta, id, date, winners = [], losers = [] } = this.props
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
                    <div>{winners[0].name}<span>{winners.length > 1 ? ',' : ''}</span></div>
                    <div>{winners.length > 1 ? winners[1].name : ''}</div>
                </td>

                <td className="losers">
                    <div>{losers[0].name}<span>{losers.length > 1 ? ',' : ''}</span></div>
                    <div>{losers.length > 1 ? losers[1].name : ''}</div>
                </td>

                <td className="delta">
                    ±{ratingDelta.toFixed(0)}
                </td>

                <td className="delete-button-container">
                    <button 
                        type="submit" 
                        className="btn btn-warning btn-sm"
                        onClick={() => this._onSubmit(id)}>Delete</button>
                </td>
            </tr>
        )
    }
    _onSubmit = id => {
        const { onDeleteMatch } = this.props
        NotificationManager.success(`The match was successfully deleted!`, 'Success')
        onDeleteMatch(id)
    }
}
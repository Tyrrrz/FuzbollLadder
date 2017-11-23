import React from 'react';
import Moment from 'react-moment';

export default class PlayerStat extends React.Component {
    render() {
        const { player, dailyRatingDelta, monthlyRatingDelta, weeklyRatingDelta } = this.props

        return (
            <tr className="player-item">
                <td className="player-name" scope="row">
                    {player.name}
                </td>

                <td className="daily-rate">
                    {dailyRatingDelta > 0 ? '+' : ''}{dailyRatingDelta.toFixed(0)}
                </td>

                <td className="month-rate">
                    {monthlyRatingDelta > 0 ? '+' : ''}{monthlyRatingDelta.toFixed(0)}
                </td>

                <td className="week-rate">
                    {weeklyRatingDelta > 0 ? '+' : ''}{weeklyRatingDelta.toFixed(0)}
                </td>
            </tr>
        )
    }

}
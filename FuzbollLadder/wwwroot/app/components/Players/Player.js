import React from 'react';


class Player extends React.Component {
    render() {
        let player = Object.assign({}, this.props);
        player.winRate = (player.winRate*100).toFixed(0)
        player.rating = (player.rating).toFixed(0)
        return (
            <tr className="player-item">
                <td className="rank">
                    {player.id}
                </td>
                <td className="name">
                    {player.name}
                </td>
                <td className="wins">
                    {player.wins}
                </td>
                <td className="losses">
                    {player.losses}
                </td>
                <td className="total">
                    {player.totalGames}
                </td>
                <td className="winrate">
                    {player.winRate}%
                </td>
                <td className="rating">
                    {player.rating}
                </td>
            </tr>
        );
    }
}

export default Player;
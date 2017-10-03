import React from 'react';

export default (player) => {
    return (
        <tr className="player-item">
            <td className="rank" scope="row">
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
                {player.winRate}
            </td>
            <td className="rating">
                {player.rating}
            </td>
        </tr>
    );
};
import React from 'react';
import { Link } from 'react-router-dom'
import PlayerStat from './PlayerStat'
const PlayerStats = ({ playerStats = [] }) => (
    <div className="mt-2">
        <table className="table">
            <thead>
                <tr>
                    <th>Player</th>
                    <th>Today</th>
                    <th>Last 7 days</th>
                    <th>Last 30 days</th>
                </tr>
            </thead>
            <tbody>
                {playerStats.map(
                    playerStat => (
                        <PlayerStat key={playerStat.player.id} {...playerStat} />
                ))}
            </tbody>
        </table>
    </div>
);

export default PlayerStats
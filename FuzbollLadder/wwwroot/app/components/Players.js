import React from 'react';
import { Link } from 'react-router-dom'
import Player from './Player'

const Players = ({ players = [], onBookClick }) => (
    <table className="players-list table">
        <thead>
            <tr>
                <th>Rank</th>
                <th>Player</th>
                <th>Wins</th>
                <th>Losses</th>
                <th>Total</th>
                <th>Winrate</th>
                <th>Rating</th>
            </tr>
        </thead>
        <tbody>
            {players.map(
                player => (
                    <Player key={player.id} {...player} />
            ))}
        </tbody>

    </table>);

export default Players;
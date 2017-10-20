import React from 'react';
import { Link } from 'react-router-dom'
import store from '../store/index'
import MatchesApi from '../services/MatchesApi'
import Match from '../components/Match'

class Matches extends React.Component {
    render() {
        return (
            <table className="matches-list table">
                <thead>
                    <tr>
                        <th scope="row">ID</th>
                        <th>Date</th>
                        <th>Winners</th>
                        <th>Losers</th>
                        <th>Delta</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {this.props.matches.map(
                        match => (
                            <Match key={match.id} {...match} />
                    ))}
                </tbody>

            </table>);
    }
}

export default Matches
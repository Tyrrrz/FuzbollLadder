import React from 'react';
import Match from './Match'

class Matches extends React.Component {
    render() {
        const { onDeleteMatch } = this.props
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
                            <Match onDeleteMatch={onDeleteMatch} key={match.id} {...match} />
                    ))}
                </tbody>
            </table>);
    }
}

export default Matches
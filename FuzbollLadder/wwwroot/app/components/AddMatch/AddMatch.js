import React from 'react'
import { isFunction } from 'lodash'
import './add-match.scss'
import Select from 'react-select'
import 'react-select/dist/react-select.css'
import {NotificationManager} from 'react-notifications'

export default class AddMatch extends React.Component {
    state = {
        winners: [],
        losers: [],
        players: [],
        allPlayers: []
    }
    componentWillMount = () => {
        const { players } = this.props 
        this.setState({players: players, allPlayers: players})
    }
    render() {    
        return (
            <form className="action-content-container">
                <div className="mt-5 form-group">
                    <label className="h5" htmlFor="winners">Winners</label>
                    <div>
                        <Select
                            name="winners"
                            valueKey="id"
                            labelKey="name"
                            value={this.state.winners}
                            onChange={this._setWinners}
                            options={this.state.players}
                            multi={true}
                        />
                    </div>
                </div>
                <div className="mt-1 form-group">
                    <label className="h5" htmlFor="losers">Losers</label>
                    <div>
                        <Select
                            name="losers"
                            valueKey="id"
                            labelKey="name"
                            value={this.state.losers}
                            onChange={this._setLosers}
                            options={this.state.players}
                            multi={true}    
                        />
                    </div>
                </div>
                <a 
                    className="btn btn-primary" 
                    onClick={this._onSubmitMatch}>
                    Submit
                </a>
            </form>
        )
    }

    _setWinners = selectedPlayers => {
        if (selectedPlayers.length > 2) {
            this._teamMaxPlayersError()
            return
        }
        this.setState({ winners: selectedPlayers }, () => {
            this._removeSelectedPlayersFromState()
        })
    }

    _setLosers = selectedPlayers => {
        if (selectedPlayers.length > 2) {
            this._teamMaxPlayersError()
            return
        }
        this.setState({ losers: selectedPlayers }, () => {
            this._removeSelectedPlayersFromState()
        })
        
    }

    _onSubmitMatch = () => {
        const { onAddMatch } = this.props
        const { winners, losers } = this.state
        if (!this._validate(winners, losers)) return
        if (isFunction(onAddMatch)) {
            onAddMatch(this._generateIdsArray(winners), this._generateIdsArray(losers))
            NotificationManager.success(`The match was successfully submitted!`, 'Success')
            this.props.history.push('/matches')
        }
    }
    /**
     * w - winners
     * l - losers
     */
    _validate = (w, l) => {
        if (w.length === 0) {
            NotificationManager.error(`At least one player must be in the "Winners" team!`, 'Error')
            return false
        }
        if (l.length === 0) {
            NotificationManager.error(`At least one player must be in the "Losers" team!`, 'Error')
            return false
        }
        if (l.length != w.length) {
            NotificationManager.error(`Players count in both team must be equal!`, 'Error')
            return false
        }
        return true
    }

    _generateIdsArray = array => {
        let generatedArray = []
        array.map(item => {
            generatedArray.push(item.id)
        })
        return generatedArray
    }

    _removeSelectedPlayersFromState = selectedPlayers => {
        const { winners, losers, allPlayers } = this.state
        let allSelectedPlayers = [...winners, ...losers]
        let players = []
        if (allSelectedPlayers.length === 0) players = allPlayers
        let filteredPlayers = allPlayers.filter(val => !allSelectedPlayers.includes(val));
        this.setState({ players: filteredPlayers })
    }

    _teamMaxPlayersError = () => {
        NotificationManager.error(`You can't add more than 2 players in the team`, 'Error')
    }
}
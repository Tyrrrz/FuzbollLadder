import React from 'react'
import TextInput from 'react-autocomplete-input';
import 'react-autocomplete-input/dist/bundle.css';
import { isFunction } from 'lodash'
import './add-match.scss'

export default class AddMatch extends React.Component {
    state = {
        winner1: '',
        winner2: '',
        loser1: '',
        loser2: ''
    }
    render() {    
        const { onAddMatch, players } = this.props 
        let playerNames = []
        players.map(player => {
            playerNames.push(player.name)
        })
        return (
            <form className="action-content-container">
                <div className="mt-5 form-group">
                    <label className="h5" htmlFor="winners">Winners</label>
                    <div>
                        <TextInput  
                            className="form-control match-participators winners"
                            placeholder="Type @ to start entering the name." 
                            trigger="@" 
                            options={playerNames} 
                            onChange={this._onValidateWinners}/>
                    </div>
                </div>
                <div className="mt-1 form-group">
                    <label className="h5" htmlFor="losers">Losers</label>
                    <div>
                        <TextInput  
                            className="form-control match-participators winners"
                            placeholder="Type @ to start entering the name." 
                            trigger="@" 
                            options={playerNames} 
                            onChange={this._onValidateLosers}/>
                    </div>
                </div>
                <a 
                    className="btn btn-primary" 
                    onClick={this._onSubmita}>
                    Submit
                </a>
            </form>
        )
    }

    _onValidateWinners = (value) => {
        if (!value) return
        this._selectPlayers(value, 'winner')
    } 
    _onValidateLosers = (value) => {
        if (!value) return
        this._selectPlayers(value, 'loser')
    } 

    _selectPlayers = (value, type) => {
        const { players } = this.props 
        let playersArray = value.split('@')
        if (playersArray && playersArray.length) {
            playersArray.map((playerName, index) => {
                if (playerName.length < 3 || index > 2) return
                players.map(player => {
                    if (playerName.indexOf(player.name) > -1) {
                        this._addPlayerToState(index, type, player.name)
                    }
                })
            })
        }
    }

    _addPlayerToState = (index, type, name) => {
        if (this.state[type + index] === name) return
        if (index === 1) {
            type === 'winner' ? this.setState({winner1: name}) : this.setState({loser1: name})
        } else {
            type === 'winner' ? this.setState({winner2: name}) : this.setState({loser2: name})
        }
    }

    _onSubmita= () => {
        const { onAddMatch } = this.props
        const { winner1, winner2, loser1, loser2 } = this.state
        
        if (isFunction(onAddMatch)) {
            onAddMatch(winner1, winner2, loser1, loser2)
            this.props.history.push('/matches')
        }
    }
}
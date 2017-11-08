import React from 'react'
import { isFunction } from 'lodash'

export default class RegisterPlayer extends React.Component {
    state = {
        player: '',
    }

    render() {    
        return (
            <div className="action-content-container mt-5"> 
                <div className="input-group">
                    <input 
                        type="text" 
                        className="form-control" 
                        placeholder="Player name"
                        onChange={this._setValue.bind(this)} />
                    <span className="input-group-btn ">
                        <button 
                            className="btn btn-primary" 
                            type="submit"
                            onClick={this._onSubmit}>
                            Register
                        </button>
                    </span>

                </div>
            </div>
        )
    }

    _setValue = event => {
        this.setState({player: event.target.value})
    }

    _onSubmit = () => {
        const { onRegisterPlayer } = this.props 
        const { player } = this.state

        if (isFunction(onRegisterPlayer)) {
            onRegisterPlayer(player)
            this.props.history.push('/')
        }
    }
}
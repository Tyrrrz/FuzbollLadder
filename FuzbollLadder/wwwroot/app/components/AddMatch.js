import React from 'react'

import { isFunction } from 'lodash'

export default class AddMatch extends React.Component {
    state = {
        winners: '',
        losers: '',
    }

    render() {    
        const { onAddMatch } = this.props 

        return (
            <form className="action-content-container">
                <div className="mt-5 form-group">
                    <label for="winners">Winners</label>
                    <input className="form-control" type="text" id="winners" onChange={this._setValue.bind(this, 'winners')} />
                </div>
                <div className="mt-1 form-group">
                    <label for="losers">Losers</label>
                    <input className="form-control" type="text" id="losers" onChange={this._setValue.bind(this, 'losers')} />
                </div>
                <button 
                    className="btn btn-primary" 
                    type="submit"
                    onClick={this._onSubmit}>
                    Submit
                </button>
            </form>
        )
    }

    _setValue = (event, value) => {
        this.setState({ value })
    }

    _onSubmit = () => {
        const { onAddMatch } = this.props
        const { winners, losers } = this.state
        
        if (isFunction(onAddMatch)) {
            onAddMatch(winners, losers)
        }
    }
}
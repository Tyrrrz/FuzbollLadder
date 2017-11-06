import React from 'react';

export default class RegisterPlayer extends React.Component {
    render() {     
        let text = null;
        return (
            <div className="action-content-container mt-5"> 
                <div className="input-group">
                    <input 
                        type="text" 
                        className="form-control" 
                        placeholder="Player name"
                        ref={(input) => { text = input; }} />
                    <span className="input-group-btn ">
                        <button 
                            className="btn btn-primary" 
                            type="submit"
                            onClick={(e) => { e.preventDefault(); this.props.onRegisterPlayer(text.value); }}>
                            Register
                        </button>
                    </span>

                </div>
            </div>
        )
    }
}
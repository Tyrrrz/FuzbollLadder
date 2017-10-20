import React from 'react';

export default class RegisterPlayer extends React.Component {
    render() {     
        let text = null;
        return (
            <div>
                <div>Register Player</div>
                <input type="text" ref={(input) => { text = input; }} />
                <button onClick={(e) => { e.preventDefault(); this.props.onRegisterPlayer(text.value); }}>register</button>
            </div>
        )
    }
}
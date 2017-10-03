import React from 'react';
import ReactDOM from 'react-dom';

export default class App extends React.Component {
    render() {
        fetch('/all').then(function(response) {
          return response.json();
        })
        .then(function(users) {
            console.log(users)
        })
        return (
                <div>
                    Hello
                </div>
            );
    }
}

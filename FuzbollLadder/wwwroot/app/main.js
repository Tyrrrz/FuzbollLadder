import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import store from './store'
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import { PlayersApi } from './services';

ReactDOM.render(
    <Provider store={store}>
      <Router basename="/">
        <div>
          <Route exact path="/" component={App} />
        </div>
      </Router>
    </Provider>,
    document.getElementById('root'));
  registerServiceWorker();
  
  PlayersApi.loadPlayers(store.dispatch, 0);
import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import store from './store'
import App from './App';
import registerServiceWorker from './registerServiceWorker';
global.jQuery = require('jquery');
global.Tether = require('tether');
require('bootstrap');
import './../sass/index.scss';
import RegisterPlayerContainer from './containers/RegisterPlayerContainer';
import Navigation from './components/Navigation';
import MatchesContainer from './containers/MatchesContainer';
import AddMatchContainer from './containers/AddMatchContainer';

ReactDOM.render(
    <Provider store={store}>
      <Router basename="/">
        <div className="layout-container">
            <Navigation />
            <Route exact path="/" component={App} />
            <Route exact path="/players/add" component={RegisterPlayerContainer} />
            <Route exact path="/matches" component={MatchesContainer} />
            <Route exact path="/matches/add" component={AddMatchContainer} />
        </div>
      </Router>
    </Provider>,
    document.getElementById('root'));
  registerServiceWorker();

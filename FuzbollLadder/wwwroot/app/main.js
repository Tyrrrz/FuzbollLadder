import React from 'react'
import ReactDOM from 'react-dom'
import { Provider } from 'react-redux'
import { BrowserRouter as Router, Route } from 'react-router-dom'
import store from './store'
import App from './App';
import registerServiceWorker from './registerServiceWorker'
global.jQuery = require('jquery')
global.Tether = require('tether')
require('bootstrap')
import './../sass/layout.scss'
import RegisterPlayerContainer from './containers/RegisterPlayerContainer'
import MatchesContainer from './containers/MatchesContainer'
import AddMatchContainer from './containers/AddMatchContainer'
import PlayerStatsContainer from './containers/PlayerStatsContainer'
import Navigation from './components/Navigation/Navigation'
import 'antd/dist/antd.css'

ReactDOM.render(
    <Provider store={store}>
      <Router basename="/">
        <div className="layout-container">
          <header>
            <h1 className="h1 mt-3">Fuzboll Ladder</h1>
            <Navigation />
          </header>
          <section>
            <Route exact path="/" component={App} />
            <Route exact path="/players/add" component={RegisterPlayerContainer} />
            <Route exact path="/matches" component={MatchesContainer} />
            <Route exact path="/matches/add" component={AddMatchContainer} />
            <Route exact path="/playerstats" component={PlayerStatsContainer} />
          </section>  
        </div>
      </Router>
    </Provider>,
    document.getElementById('root'))
  registerServiceWorker()

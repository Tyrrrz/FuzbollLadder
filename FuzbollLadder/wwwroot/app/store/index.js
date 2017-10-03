import reducers from '../reducers';
import { createStore } from 'redux';

const store = createStore(reducers);

export default store;
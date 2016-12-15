import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { Router, Route, browserHistory, IndexRoute } from "react-router";
import { syncHistoryWithStore } from "react-router-redux";

import App from "./pages/App";
import StartPage from "./pages/StartPage";
import RegisterPage from "./pages/RegisterPage";
import LoginPage from "./pages/LoginPage";
import store from './store';

const history = syncHistoryWithStore(browserHistory, store);

const app = document.getElementById('app');
ReactDOM.render(
    <Provider store={store}>
        <Router history={history}>
            <Route path="/" component={App}>
                <IndexRoute component={StartPage}></IndexRoute>
                <Route path="register" component={RegisterPage}></Route>
                <Route path="login" component={LoginPage}></Route>            
            </Route>
        </Router>
    </Provider>
, app);

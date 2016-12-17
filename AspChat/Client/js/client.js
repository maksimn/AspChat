import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { Router, Route, hashHistory, IndexRoute } from "react-router";
import { syncHistoryWithStore } from "react-router-redux";

import App from "./pages/App";
import StartPage from "./pages/StartPage";
import RegisterPage from "./pages/RegisterPage";
import LoginPage from "./pages/LoginPage";
import ChatRoom from "./pages/ChatRoom";
import store from './store';

const history = syncHistoryWithStore(hashHistory, store);

const app = document.getElementById('app');

let routes = [];

if (isAuth()) {
    routes.push(<IndexRoute key={0} component={ChatRoom}></IndexRoute>);
} else {
    routes.push(<IndexRoute key={1} component={StartPage}></IndexRoute>);
    routes.push(<Route key={2} path="register" component={RegisterPage}></Route>);
    routes.push(<Route key={3} path="login" component={LoginPage}></Route>);
}

ReactDOM.render(
    <Provider store={store}>
        <Router history={history}>
            <Route path="/" component={App}>
                {routes}
            </Route>
        </Router>
    </Provider>
, app);

/*!

=========================================================
* Black Dashboard React v1.1.0
=========================================================

* Product Page: https://www.creative-tim.com/product/black-dashboard-react
* Copyright 2020 Creative Tim (https://www.creative-tim.com)
* Licensed under MIT (https://github.com/creativetimofficial/black-dashboard-react/blob/master/LICENSE.md)

* Coded by Creative Tim

=========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

*/
import React from "react";
import ReactDOM from "react-dom";
import { createBrowserHistory } from "history";
import { Router, Route, Switch, Redirect } from "react-router-dom";
import { createStore, applyMiddleware, combineReducers } from "redux";
import { Provider } from "react-redux";
import { BrowserRouter } from "react-router-dom";
import { composeWithDevTools } from "redux-devtools-extension";

import thunk from "redux-thunk";
import AdminLayout from "layouts/Admin/Admin";
import AuthLayout from "layouts/Auth/Auth";
import setApiSettings from "api/SetApi.js";
import Logout from "layouts/Auth/Logout";

import "assets/scss/black-dashboard-react.scss";
import "assets/demo/demo.css";
import "assets/css/nucleo-icons.css";

import authReducer from "store/reducers/authReducer";
import movieReducer from "store/reducers/movieReducer";
import movieReviewReducer from "store/reducers/movieReviewReducer";
import commentReducer from "store/reducers/commentReducer";

setApiSettings();
const hist = createBrowserHistory();

const rootReducer = combineReducers({
  authReducer: authReducer,
  movieReducer: movieReducer,
  movieReviewReducer: movieReviewReducer,
  commentReducer: commentReducer,
});

const store = createStore(
  rootReducer,
  composeWithDevTools(applyMiddleware(thunk))
);

const app = (
  <Provider store={store}>
    <BrowserRouter>
      <Router history={hist}>
        <Switch>
          <Route path="/main" render={(props) => <AdminLayout {...props} />} />
          <Route path="/auth" render={(props) => <AuthLayout {...props} />} />
          <Route path="/logout" component={Logout} />
          <Redirect from="/" to="/main/dashboard" />
        </Switch>
      </Router>
    </BrowserRouter>
  </Provider>
);

ReactDOM.render(app, document.getElementById("root"));

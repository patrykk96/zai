import React from "react";
import { Route, Switch, withRouter} from "react-router-dom";
import { connect } from "react-redux";

import PerfectScrollbar from "perfect-scrollbar";

import Login from "components/Auth/Login";
import Register from "components/Auth/Register"
import AdminNavbar from "components/Navbars/AdminNavbar.jsx";
import Sidebar from "components/Sidebar/Sidebar.jsx";

import * as actions from "../../store/actions/authActions";
import routes from "routes.js";

var ps;

class Auth extends React.Component {
    constructor(props) {
      super(props);
      this.state = {
        backgroundColor: "blue",
        sidebarOpened:
          document.documentElement.className.indexOf("nav-open") !== -1
      };
    }
   
    componentDidMount() {
      if (navigator.platform.indexOf("Win") > -1) {
        document.documentElement.className += " perfect-scrollbar-on";
        document.documentElement.classList.remove("perfect-scrollbar-off");
        ps = new PerfectScrollbar(this.refs.mainPanel, { suppressScrollX: true });
        let tables = document.querySelectorAll(".table-responsive");
        for (let i = 0; i < tables.length; i++) {
          ps = new PerfectScrollbar(tables[i]);
        }
      }
    }
    componentWillUnmount() {
      if (navigator.platform.indexOf("Win") > -1) {
        ps.destroy();
        document.documentElement.className += " perfect-scrollbar-off";
        document.documentElement.classList.remove("perfect-scrollbar-on");
      }
    }
    componentDidUpdate(e) {
      if (e.history.action === "PUSH") {
        if (navigator.platform.indexOf("Win") > -1) {
          let tables = document.querySelectorAll(".table-responsive");
          for (let i = 0; i < tables.length; i++) {
            ps = new PerfectScrollbar(tables[i]);
          }
        }
        document.documentElement.scrollTop = 0;
        document.scrollingElement.scrollTop = 0;
        this.refs.mainPanel.scrollTop = 0;
      }
    }
    
    // this function opens and closes the sidebar on small devices
    toggleSidebar = () => {
      document.documentElement.classList.toggle("nav-open");
      this.setState({ sidebarOpened: !this.state.sidebarOpened });
    };
  
    handleBgClick = color => {
      this.setState({ backgroundColor: color });
    };
  
    getBrandText = () => {
      return "Autoryzacja";
    };
    
    render() {
      return (
        <>
          <div className="wrapper">
            <Sidebar
              {...this.props}
              routes={routes}
              bgColor={this.state.backgroundColor}
              toggleSidebar={this.toggleSidebar}
            />
            <div
              className="main-panel"
              ref="mainPanel"
              data={this.state.backgroundColor}
            >
              <AdminNavbar
                {...this.props}
                brandText={this.getBrandText(this.props.location.pathname)}
                toggleSidebar={this.toggleSidebar}
                sidebarOpened={this.state.sidebarOpened}
              />
              <Switch>
                  <Route path="/auth/login" render={(props) => <Login {...props} login={this.props.login}
                                                                resetErrors={this.props.resetErrors}
                                                                loading={this.props.loading}
                                                                error={this.props.error}
                                                                isAuthenticated={this.props.isAuthenticated}
                                                                response={this.props.response}/>}/>
                  <Route path="/auth/register"  render={(props) => <Register {...props} register={this.props.register}
                                                                resetErrors={this.props.resetErrors}
                                                                loading={this.props.loading}
                                                                error={this.props.error}
                                                                response={this.props.response}/>}  />
                 
              </Switch>
            </div>
          </div>
       
        </>
      );
    }
  }
  
  const mapDispatchToProps = dispatch => {
    return {
      resetErrors: () => dispatch(actions.resetErrors()),
      register: user => dispatch(actions.register(user)),
      login: user => dispatch(actions.login(user))
    };
  };
  
  const mapStateToProps = state => {
    return {
      isAuthenticated: localStorage.getItem('token') !== null,
      loading: state.authReducer.loading,
      error: state.authReducer.error,
      response: state.authReducer.response,
      resetPasswordResult: state.authReducer.resetPasswordResult
    };
  };
  
  export default withRouter(connect(
    mapStateToProps,
    mapDispatchToProps
  )(Auth));
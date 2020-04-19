import React, { Component } from "react";
import { Redirect } from "react-router-dom";
import { connect } from "react-redux";
import * as actions from "../../store/actions/authActions";

class Logout extends Component {
  componentDidMount() {
    this.props.logout();
    localStorage.clear();
  }

  render() {
    return <Redirect to="/" />;
  }
}

const mapDispatchToProps = dispatch => {
  return {
    logout: () => dispatch(actions.logout())
  };
};

export default connect(
  null,
  mapDispatchToProps
)(Logout);

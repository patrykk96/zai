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
import { connect } from 'react-redux'
import * as movieActions from '../store/actions/movieActions';

// reactstrap components
import {
  Row
} from "reactstrap";
import MovieCardPanel from "components/Movies/MovieCardPanel";



class Dashboard extends React.Component {
  constructor(props) {
    super(props);
  }

  componentDidMount() {
    this.props.moviesGet();
  }

  render() {
    return (
      <>
        <div className="content">
             <Row>
               <MovieCardPanel
                  movies={this.props.movies}
                  isAuthenticated={this.props.isAuthenticated}
                />
             </Row>
        </div>
      </>
    );
  }
}

const mapDispatchToProps = dispatch => {
  return {
    moviesGet: () => dispatch(movieActions.moviesGet()),
  }
}

const mapStateToProps = state => {
  return {
    movies: state.movieReducer.movies,
    loading: state.movieReducer.loading,
    error: state.movieReducer.error
  }
}

export default connect(
  mapStateToProps, 
  mapDispatchToProps
)(Dashboard);

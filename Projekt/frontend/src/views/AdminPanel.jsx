import React from "react";
import { connect } from "react-redux";

import MovieAdd from "components/AdminPanel/MovieAdd";
import MoviePanel from "components/AdminPanel/MoviePanel";
import Spinner from "components/Spinner";
import * as movieActions from "store/actions/movieActions";

import { Row, Col } from "reactstrap";

class AdminPanel extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      toggleMovieAdd: false,
    };
  }

  componentDidMount() {
    this.getMovies();
  }

  getMovies() {
    this.props.moviesGet();
    console.log(this.props);
  }

  toggleMovieAdd = () => {
    console.log(this.props);
    this.setState((prevState) => ({
      toggleMovieAdd: !prevState.toggleMovieAdd,
    }));
    if (this.state.toggleMovieAdd) {
      this.getMovies();
    }
  };

  render() {
    return (
      <>
        <div className="content">
          <Row>
            {this.props.loading ? (
              <Spinner />
            ) : (
              <Col className="ml-auto mr-auto" lg="12" md="12">
                {this.state.toggleMovieAdd ? (
                  <MovieAdd
                    movieAdd={this.props.movieAdd}
                    toggleMovieAdd={this.toggleMovieAdd}
                    error={this.props.error}
                  />
                ) : (
                  <MoviePanel
                    toggleMovieAdd={this.toggleMovieAdd}
                    movieEdit={this.props.movieEdit}
                    movieDelete={this.props.movieDelete}
                    movies={this.props.movies}
                    moviesGet={this.getMovies}
                  />
                )}
              </Col>
            )}
          </Row>
        </div>
      </>
    );
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    movieAdd: (movie) => dispatch(movieActions.movieAdd(movie)),
    moviesGet: () => dispatch(movieActions.moviesGet()),
    movieEdit: (movie) => dispatch(movieActions.movieEdit(movie)),
    movieDelete: (id) => dispatch(movieActions.movieDelete(id)),
  };
};

const mapStateToProps = (state) => {
  return {
    loading: state.movieReducer.loading,
    error: state.movieReducer.error,
    movies: state.movieReducer.movies,
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(AdminPanel);

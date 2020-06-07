import React from "react";
import { connect } from "react-redux";

import {
  Button,
  Card,
  CardHeader,
  CardBody,
  CardTitle,
  Table,
} from "reactstrap";

import * as movieActions from "store/actions/movieActions";
import FavouriteMovie from "components/Movies/FavouriteMovie";
import Spinner from "components/Spinner";
import ErrorText from "components/ErrorText/ErrorText";

class FavouriteMovies extends React.Component {
  constructor(props) {
    super(props);
  }

  componentDidMount() {
    this.props.favouriteMoviesGet();
  }

  submitFavouriteMovieDelete = (id) => {
    this.props.favouriteMovieDelete(id);
  };

  render() {
    let movies = null;
    if (this.props.movies && !this.props.loading) {
      movies = this.props.movies.map((movie) => {
        return (
          <FavouriteMovie
            key={movie.id}
            id={movie.id}
            movieName={movie.name}
            movieDescription={movie.description}
            movieImage={movie.logo}
            userRating={movie.userRating}
            submitFavouriteMovieDelete={this.submitFavouriteMovieDelete}
          />
        );
      });
    }
    return (
      <div className="content">
        {this.props.loading ? (
          <Spinner />
        ) : (
          <Card className="card-tasks">
            <CardHeader>
              <CardTitle tag="h3">
                <i className="tim-icons icon-controller text-info" /> Twoje
                ulubione filmy
              </CardTitle>
            </CardHeader>
            <CardBody>
              <Table>
                <thead className="text-primary">
                  <tr>
                    <th>Obraz</th>
                    <th>Tytuł</th>
                    <th>Opis</th>
                    <th className="text-center">Twoja ocena</th>
                    <th className="text-center">Przejdź do filmu</th>
                    <th className="text-center">Usuń z ulubionych</th>
                  </tr>
                </thead>
                <tbody>{movies}</tbody>
              </Table>
              <ErrorText error={this.props.error}></ErrorText>
            </CardBody>
          </Card>
        )}
      </div>
    );
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    favouriteMoviesGet: () => dispatch(movieActions.favouriteMoviesGet()),
    favouriteMovieDelete: (movieId) =>
      dispatch(movieActions.favouriteMovieDelete(movieId, true)),
  };
};

const mapStateToProps = (state) => {
  return {
    loading: state.movieReducer.loading,
    error: state.movieReducer.error,
    movies: state.movieReducer.movies,
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(FavouriteMovies);

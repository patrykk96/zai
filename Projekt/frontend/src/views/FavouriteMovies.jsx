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
    if (this.props.movies) {
      movies = this.props.movies.map((movie) => {
        return (
          <FavouriteMovie
            key={movie.id}
            id={movie.id}
            movieName={movie.name}
            movieDescription={movie.description}
            movieImage={movie.logo}
            userRating={movie.rating}
            submitFavouriteMovieDelete={this.submitFavouriteMovieDelete}
          />
        );
      });
    }
    return (
      <div className="content">
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
                  <th className="text-center">Usuń z ulubionych</th>
                </tr>
              </thead>
              <tbody>{movies}</tbody>
            </Table>
          </CardBody>
        </Card>
      </div>
    );
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    favouriteMoviesGet: () => dispatch(movieActions.favouriteMoviesGet()),
    favouriteMovieDelete: (movieId) =>
      dispatch(movieActions.favouriteMovieDelete(movieId)),
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

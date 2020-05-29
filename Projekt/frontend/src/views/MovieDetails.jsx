import React from "react";
import Rating from "react-rating";
import { connect } from "react-redux";

import Spinner from "components/Spinner";
import * as movieActions from "../store/actions/movieActions";
import "./css/MovieDetails.css";

import {
  Card,
  CardHeader,
  Label,
  CardTitle,
  CardBody,
  CardImg,
  Button,
} from "reactstrap";

class MovieDetails extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      id: null,
    };
  }

  componentDidMount() {
    const {
      match: { params },
    } = this.props;
    this.setState({ id: params.movieId });
    this.props.movieGet(params.movieId);
  }

  favouriteMovieAdd = () => {
    this.props.favouriteMovieAdd(this.state.id);
  };

  favouriteMovieDelete = () => {
    this.props.favouriteMovieDelete(this.state.id);
  };

  render() {
    return (
      <div className="content">
        {this.props.loading || !this.props.movie ? (
          <Spinner />
        ) : (
          <Card>
            <CardHeader>
              <CardTitle tag="h3">
                {this.props.movie.name}
                <Button className="float-right" color="success">
                  Wyświetl recenzje
                </Button>
                {this.props.isAuthenticated ? (
                  <>
                    {!this.props.movie.rating ? (
                      <Button className="float-right" color="success">
                        Utwórz recenzję
                      </Button>
                    ) : (
                      <Button className="float-right" color="success">
                        Wyświetl swoją recenzję
                      </Button>
                    )}
                  </>
                ) : (
                  <></>
                )}
                {this.props.isAuthenticated ? (
                  <>
                    {!this.props.movie.isFavourite ? (
                      <Button
                        className="float-right"
                        color="info"
                        title="Oznacz jako ulubiony"
                        onClick={this.favouriteMovieAdd}
                      >
                        <i className="tim-icons icon-heart-2" />
                      </Button>
                    ) : (
                      <Button
                        className="float-right"
                        color="primary"
                        title="Odznacz ulubiony"
                        onClick={this.favouriteMovieDelete}
                      >
                        <i className="tim-icons icon-heart-2" />
                      </Button>
                    )}
                  </>
                ) : (
                  <></>
                )}
              </CardTitle>

              <CardImg
                src={this.props.movie.logo}
                style={{ width: "50%", height: "auto" }}
              />

              <hr />
              <div className="rating">
                Średnia ocen:
                <br />
                <Rating
                  placeholderRating={this.props.movie.rating}
                  emptySymbol="tim-icons icon-shape-star rating"
                  fullSymbol="tim-icons icon-shape-star text-success rating"
                  placeholderSymbol="tim-icons icon-shape-star text-success rating"
                  readonly
                />
                <br />
                <p className="ratingText">{"  7,8/10"}</p>
              </div>

              {this.props.isAuthenticated ? (
                <div className="userRating">
                  Twoja ocena:
                  <br />
                  <Rating
                    placeholderRating={this.props.movie.rating}
                    emptySymbol="tim-icons icon-shape-star rating"
                    fullSymbol="tim-icons icon-shape-star text-success rating"
                    placeholderSymbol="tim-icons icon-shape-star text-success rating"
                    readonly
                  />
                  <br />
                  <p className="ratingText">{"  7,8/10"}</p>
                </div>
              ) : (
                <></>
              )}
            </CardHeader>

            <CardBody>
              <br />
              <p className="movieDescription">{this.props.movie.description}</p>
            </CardBody>
          </Card>
        )}
      </div>
    );
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    movieGet: (movieId) => dispatch(movieActions.movieGet(movieId)),
    favouriteMovieAdd: (movieId) =>
      dispatch(movieActions.favouriteMovieAdd(movieId)),
    favouriteMovieDelete: (movieId) =>
      dispatch(movieActions.favouriteMovieDelete(movieId, false)),
  };
};

const mapStateToProps = (state) => {
  return {
    movie: state.movieReducer.movie,
    loading: state.movieReducer.loading,
    error: state.movieReducer.error,
    isAuthenticated: localStorage.getItem("token") !== null,
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(MovieDetails);

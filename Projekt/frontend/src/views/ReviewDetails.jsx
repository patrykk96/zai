import React from "react";
import Rating from "react-rating";
import { connect } from "react-redux";
import { Link } from "react-router-dom";

import Spinner from "components/Spinner";
import * as movieReviewActions from "../store/actions/movieReviewActions";
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

class ReviewDetails extends React.Component {
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
    this.setState({ id: params.reviewId });
    this.props.movieReviewGet(params.reviewId);
  }

  render() {
    return (
      <div className="content">
        {this.props.loading || !this.props.review ? (
          <Spinner />
        ) : (
          <Card>
            <CardHeader>
              <CardTitle tag="h3">
                {this.props.review.movieName}

                {this.props.isAuthenticated ? (
                  <>
                    <Link to={`../reviewUpdate/${this.props.movie.id}`}>
                      <Button className="float-right" color="success">
                        Edytuj recenzję
                      </Button>
                    </Link>

                    <Button className="float-right" color="warning">
                      Usuń recenzję
                    </Button>
                  </>
                ) : (
                  <></>
                )}
              </CardTitle>

              <hr />
              <div className="rating">
                Twoja ocena
                <br />
                <Rating
                  placeholderRating={this.props.review.rating}
                  emptySymbol="tim-icons icon-shape-star rating"
                  fullSymbol="tim-icons icon-shape-star text-success rating"
                  placeholderSymbol="tim-icons icon-shape-star text-success rating"
                  readonly
                />
                <br />
                <p className="ratingText">{"  7,8/10"}</p>
              </div>
            </CardHeader>

            <CardBody>
              <br />
              <p className="movieDescription">{this.props.review.content}</p>
            </CardBody>
          </Card>
        )}
      </div>
    );
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    movieReviewGet: (movieId) =>
      dispatch.movieReviewActions.movieReviewGet(movieId),
  };
};

const mapStateToProps = (state) => {
  return {
    review: state.movieReviewReducer.review,
    loading: state.movieReviewReducer.loading,
    error: state.movieReviewReducer.error,
    isAuthenticated: localStorage.getItem("token") !== null,
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(ReviewDetails);

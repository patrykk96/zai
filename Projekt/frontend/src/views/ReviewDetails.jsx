import React from "react";
import Rating from "react-rating";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import Comments from "components/Comments/Comments";
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
      reviewId: null,
    };
  }

  componentDidMount() {
    const {
      match: { params },
    } = this.props;
    this.setState({ reviewId: params.reviewId });
    this.props.movieReviewGet(params.reviewId);
  }

  submitMovieReviewDelete = () => {
    this.props.movieReviewDelete(this.props.review.reviewId);
  };

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

                {this.props.isAuthenticated && this.props.review.isAuthor ? (
                  <>
                    <Link to={`../reviewUpdate/${this.props.review.reviewId}`}>
                      <Button className="float-right" color="success">
                        Edytuj recenzję
                      </Button>
                    </Link>

                    <Button
                      className="float-right"
                      color="warning"
                      onClick={this.submitMovieReviewDelete}
                    >
                      Usuń recenzję
                    </Button>
                  </>
                ) : (
                  <></>
                )}
                <br />
              </CardTitle>
              <h5>Autor: {this.props.review.author}</h5>

              <hr />
              <div className="rating">
                {"      "}Ocena
                <br />
                <Rating
                  placeholderRating={this.props.review.rating}
                  emptySymbol="tim-icons icon-shape-star rating"
                  fullSymbol="tim-icons icon-shape-star text-success rating"
                  placeholderSymbol="tim-icons icon-shape-star text-success rating"
                  readonly
                />
                <br />
                <p className="ratingText">{this.props.review.rating + "/5"}</p>
              </div>
            </CardHeader>

            <CardBody>
              <br />
              <p className="movieDescription">{this.props.review.content}</p>
            </CardBody>
            <Comments reviewId={this.state.reviewId}></Comments>
          </Card>
        )}
      </div>
    );
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    movieReviewGet: (reviewId) =>
      dispatch(movieReviewActions.movieReviewGet(reviewId)),
    movieReviewDelete: (reviewId) =>
      dispatch(movieReviewActions.movieReviewDelete(reviewId)),
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

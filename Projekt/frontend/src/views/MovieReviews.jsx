import React from "react";
import { connect } from "react-redux";

import { Card, CardHeader, CardBody, CardTitle, Table } from "reactstrap";

import * as movieReviewActions from "store/actions/movieReviewActions";
import MovieReview from "components/Reviews/MovieReview";
import Spinner from "components/Spinner";

class MovieReviews extends React.Component {
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

    let id = params.movieId ? params.movieId : 0;
    this.setState({ id: id });
    this.props.movieReviewsGet(id);
  }

  render() {
    let reviews = null;
    if (this.props.reviews.length > 0 && !this.props.loading) {
      reviews = this.props.reviews.map((review) => {
        return (
          <MovieReview
            key={review.reviewId}
            id={review.reviewId}
            reviewAuthor={review.author}
            movieName={review.movieName}
            reviewContent={review.content}
            rating={review.rating}
            isUserReviews={this.state.id ? true : false}
          />
        );
      });
    }
    return (
      <div className="content">
        {this.props.loading || !this.props.reviews ? (
          <Spinner />
        ) : (
          <Card className="card-tasks">
            <CardHeader>
              <CardTitle tag="h3">
                <i className="tim-icons icon-tv-2 text-info" />
                {"   "}Recenzje{"       "}
                {this.state.id && this.props.reviews.length > 0
                  ? this.props.reviews[0].movieName
                  : ""}
              </CardTitle>
            </CardHeader>
            <CardBody>
              <Table>
                <thead className="text-primary">
                  <tr>
                    <th>Autor</th>
                    <th>Tytuł filmu</th>
                    <th>Treść</th>
                    <th className="text-center">Ocena</th>
                    <th className="text-center">Przejdź do recenzji</th>
                  </tr>
                </thead>
                <tbody>{reviews}</tbody>
              </Table>
            </CardBody>
          </Card>
        )}
      </div>
    );
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    movieReviewsGet: (movieId) =>
      dispatch(movieReviewActions.movieReviewsGet(movieId)),
  };
};

const mapStateToProps = (state) => {
  return {
    loading: state.movieReviewReducer.loading,
    error: state.movieReviewReducer.error,
    reviews: state.movieReviewReducer.reviews,
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(MovieReviews);

import React from "react";
import { connect } from "react-redux";

import { Card, CardHeader, CardBody, CardTitle, Table } from "reactstrap";

import * as movieReviewActions from "store/actions/movieReviewActions";
import Spinner from "components/Spinner";

class MovieReviews extends React.Component {
  constructor(props) {
    super(props);
  }

  componentDidMount() {
    this.props.favouriteMoviesGet();
  }

  render() {
    let reviews = null;
    if (this.props.reviews && !this.props.loading) {
      reviews = this.props.reviews.map((review) => {
        return (
          <MovieReview
            key={review.id}
            id={review.id}
            reviewAuthor={review.authorName}
            movieName={review.movieName}
            reviewContent={review.content}
            rating={review.score}
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
                <i className="tim-icons icon-controller text-info" />
                Recenzje {this.props.reviews[0].movieName}
              </CardTitle>
            </CardHeader>
            <CardBody>
              <Table>
                <thead className="text-primary">
                  <tr>
                    <th>Autor</th>
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
    loading: state.movieReducer.loading,
    error: state.movieReducer.error,
    reviews: state.movieReducer.reviews,
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(MovieReviews);

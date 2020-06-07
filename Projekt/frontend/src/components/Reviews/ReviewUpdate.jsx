import React from "react";
import Rating from "react-rating";
import { connect } from "react-redux";
import { Link, Redirect } from "react-router-dom";

import {
  Button,
  Card,
  CardHeader,
  CardBody,
  CardTitle,
  Row,
  Col,
  FormGroup,
  Input,
  UncontrolledAlert,
} from "reactstrap";

import ErrorText from "components/ErrorText/ErrorText";
import * as movieActions from "../../store/actions/movieActions";
import * as movieReviewActions from "../../store/actions/movieReviewActions";
import Spinner from "../Spinner";

class ReviewUpdate extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      reviewId: "",
      reviewContent: "",
      rating: 0,
      invalidRating: false,
    };
  }

  componentDidMount() {
    const {
      match: { params },
    } = this.props;
    this.setState({ reviewId: params.reviewId });
    this.props.movieReviewGet(params.reviewId);
  }
  submitReview = (event) => {
    const review = {
      movieId: parseInt(this.props.review.movieId),
      content:
        this.state.reviewContent.length > 0
          ? this.state.reviewContent
          : this.props.review.content,
      score:
        this.state.rating !== 0 ? this.state.rating : this.props.review.rating,
    };

    const reviewId = this.props.review.reviewId;
    event.preventDefault();
    this.props.movieReviewEdit(reviewId, review);
  };

  renderRedirect = () => {
    if (this.props.redirect) {
      let route = `../review/${this.props.review.reviewId}`;
      return <Redirect to={route} />;
    }
  };

  setRating = (value) => {
    this.setState({ invalidRating: false });
    this.setState({ rating: value });
    this.props.review.rating = value;
  };

  handleInputChange = (event) => {
    event.preventDefault();
    this.setState({ [event.target.name]: event.target.value });
  };

  render() {
    return (
      <>
        {this.renderRedirect()}
        <div className="content">
          {this.props.loading || !this.props.review ? (
            <Spinner />
          ) : (
            <Row>
              <Col>
                <Card>
                  <CardHeader>
                    <CardTitle tag="h3">
                      <i className="tim-icons icon-controller text-success" />{" "}
                      Zaktualizuj recenzję
                      <Link to={`../review/${this.props.review.reviewId}`}>
                        <Button
                          className="float-right"
                          color="link text-warning"
                        >
                          <i className="tim-icons icon-simple-remove"></i>
                        </Button>
                      </Link>
                    </CardTitle>
                  </CardHeader>
                  <CardBody>
                    <form onSubmit={this.submitReview}>
                      <FormGroup>
                        <label>Tytuł filmu</label>
                        <br />
                        {this.props.review.movieName}
                      </FormGroup>

                      <FormGroup>
                        <label>Treść recenzji</label>
                        <Input
                          placeholder="Treść recenzji"
                          name="reviewContent"
                          type="textarea"
                          defaultValue={this.props.review.content}
                          maxLength="1000"
                          required
                          col="100"
                          onChange={(event) => this.handleInputChange(event)}
                        />
                      </FormGroup>

                      <FormGroup>
                        <Rating
                          placeholderRating={this.props.review.rating}
                          emptySymbol="tim-icons icon-shape-star rating"
                          fullSymbol="tim-icons icon-shape-star text-success rating"
                          placeholderSymbol="tim-icons icon-shape-star text-success rating"
                          readonly={false}
                          onClick={this.setRating}
                        />
                      </FormGroup>

                      <Button color="primary">Zaktualizuj recenzję</Button>
                      {this.state.invalidRating ? (
                        <UncontrolledAlert color="danger">
                          <span>Nie podano oceny</span>
                        </UncontrolledAlert>
                      ) : (
                        <></>
                      )}
                      <ErrorText error={this.props.error}></ErrorText>
                    </form>
                  </CardBody>
                </Card>
              </Col>
            </Row>
          )}
        </div>
      </>
    );
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    movieGet: (movieId) => dispatch(movieActions.movieGet(movieId)),
    movieReviewEdit: (reviewId, review) =>
      dispatch(movieReviewActions.movieReviewEdit(reviewId, review)),
    movieReviewGet: (reviewId) =>
      dispatch(movieReviewActions.movieReviewGet(reviewId)),
  };
};

const mapStateToProps = (state) => {
  return {
    loading: state.movieReviewReducer.loading,
    error: state.movieReducer.error,
    movie: state.movieReducer.movie,
    review: state.movieReviewReducer.review,
    redirect: state.movieReviewReducer.redirect,
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(ReviewUpdate);

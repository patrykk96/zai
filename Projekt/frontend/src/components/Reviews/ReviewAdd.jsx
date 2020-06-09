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

class ReviewAdd extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      movieId: "",
      reviewContent: "",
      rating: 0,
      invalidRating: false,
    };
  }

  componentDidMount() {
    const {
      match: { params },
    } = this.props;
    this.setState({ movieId: params.movieId });
    this.props.movieGet(params.movieId);
  }
  submitReview = (event) => {
    if (this.state.rating !== 0) {
      this.setState({ invalidRating: false });
    } else {
      this.setState({ invalidRating: true });
      event.preventDefault();
      return;
    }
    const review = {
      movieId: parseInt(this.state.movieId),
      content: this.state.reviewContent,
      score: this.state.rating,
    };

    event.preventDefault();
    this.props.movieReviewAdd(review);
  };

  setRating = (value) => {
    this.setState({ invalidRating: false });
    this.setState({ rating: value });
  };

  renderRedirect = () => {
    if (this.props.redirect) {
      let route = `../movie/${this.props.movie.id}`;
      return <Redirect to={route} />;
    }
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
          {this.props.loading || !this.props.movie ? (
            <Spinner />
          ) : (
            <Row>
              <Col>
                <Card>
                  <CardHeader>
                    <CardTitle tag="h3">
                      <i className="tim-icons icon-controller text-success" />{" "}
                      Dodaj recenzję
                      <Link to={`../movie/${this.props.movie.id}`}>
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
                        {this.props.movie.name}
                      </FormGroup>

                      <FormGroup>
                        <label>Treść recenzji</label>
                        <Input
                          defaultValue=""
                          placeholder="Treść recenzji"
                          name="reviewContent"
                          type="textarea"
                          maxLength="1000"
                          required
                          onChange={(event) => this.handleInputChange(event)}
                        />
                      </FormGroup>

                      <FormGroup>
                        <Rating
                          placeholderRating={this.state.rating}
                          emptySymbol="tim-icons icon-shape-star rating"
                          fullSymbol="tim-icons icon-shape-star text-success rating"
                          placeholderSymbol="tim-icons icon-shape-star text-success rating"
                          readonly={false}
                          onClick={this.setRating}
                        />
                      </FormGroup>

                      <Button color="primary">Dodaj recenzję</Button>
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
    movieReviewAdd: (review) =>
      dispatch(movieReviewActions.movieReviewAdd(review)),
  };
};

const mapStateToProps = (state) => {
  return {
    loading: state.movieReviewReducer.loading,
    redirect: state.movieReviewReducer.redirect,
    error: state.movieReducer.error,
    movie: state.movieReducer.movie,
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(ReviewAdd);

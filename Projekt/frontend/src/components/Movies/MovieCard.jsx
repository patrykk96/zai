import React from "react";
import Rating from "react-rating";
import "./MovieCard.css";
import { Link } from "react-router-dom";
import { Textfit } from "react-textfit";

import {
  Card,
  Col,
  CardHeader,
  CardBody,
  CardTitle,
  Label,
  CardImg,
} from "reactstrap";

class MovieCard extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      rating: null,
    };
  }

  render() {
    return (
      <Col lg="4">
        <Link to={`movie/${this.props.id}`}>
          <Card>
            <CardHeader>
              <CardTitle tag="h3">
                {/* <i className="tim-icons icon-tv-2 text-info" /> */}
                <Textfit
                  mode="single"
                  forceSingleModeWidth={false}
                  className="movieTitle"
                >
                  {" "}
                  {this.props.movieName}{" "}
                </Textfit>
              </CardTitle>

              <CardImg
                src={this.props.movieImage}
                className="customCardImg"
              ></CardImg>
            </CardHeader>
            <CardBody>
              <Label>{this.props.movieDescription}</Label>
              <br />
              <Label>Średnia ocen:</Label>
              <br />
              <Rating
                placeholderRating={this.props.movieRating}
                emptySymbol="tim-icons icon-shape-star rating"
                fullSymbol="tim-icons icon-shape-star text-success rating"
                placeholderSymbol="tim-icons icon-shape-star text-success rating"
                readonly
              />
              <br />
            </CardBody>
          </Card>
        </Link>
      </Col>
    );
  }
}

export default MovieCard;

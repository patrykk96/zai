import React from "react";
import Rating from "react-rating";
import { Link } from "react-router-dom";

import { Button } from "reactstrap";

class FavouriteMovie extends React.Component {
  render() {
    return (
      <tr>
        <td width="5%">
          <img
            src={this.props.movieImage}
            className="movieListElementImg"
            alt="Movie"
          ></img>
        </td>
        <td width="25%" className="tablesorter">
          {" "}
          {this.props.movieName}
        </td>
        <td width="25%">
          <small>{this.props.movieDescription}</small>
        </td>
        <td width="15%" className="text-center">
          <Rating
            placeholderRating={this.props.userRating}
            emptySymbol="tim-icons icon-shape-star rating"
            fullSymbol="tim-icons icon-shape-star text-success rating"
            placeholderSymbol="tim-icons icon-shape-star text-success rating"
            readonly
          />
        </td>
        <td width="10%" className="text-center">
          <Link to={`movie/${this.props.id}`}>
            <i className="tim-icons icon-user-run" />
          </Link>
        </td>
        <td width="10%" className="text-center">
          <Button
            onClick={() => this.props.submitFavouriteMovieDelete(this.props.id)}
            color="link"
          >
            <i className="tim-icons icon-trash-simple" />
          </Button>
        </td>
      </tr>
    );
  }
}

export default FavouriteMovie;

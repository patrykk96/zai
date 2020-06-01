import React from "react";
import Rating from "react-rating";
import { Link } from "react-router-dom";

class MovieReview extends React.Component {
  render() {
    return (
      <tr>
        <td width="15%">{this.props.reviewAuthor}</td>
        <td width="25%" className="tablesorter">
          {" "}
          {this.props.movieName}
        </td>
        <td width="30%">
          <small>{this.props.reviewContent}</small>
        </td>
        <td width="10%" className="text-center">
          <Rating
            placeholderRating={this.props.rating}
            emptySymbol="tim-icons icon-shape-star rating"
            fullSymbol="tim-icons icon-shape-star text-success rating"
            placeholderSymbol="tim-icons icon-shape-star text-success rating"
            readonly
          />
        </td>
        <td width="10%" className="text-center">
          <Link to={`review/${this.props.id}`}>
            <i className="tim-icons icon-user-run" />
          </Link>
        </td>
      </tr>
    );
  }
}

export default MovieReview;

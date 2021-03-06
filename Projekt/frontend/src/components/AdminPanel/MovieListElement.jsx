import React from "react";
import Rating from "react-rating";
import "./MovieListElement.css";

import { Button, Input, FormGroup } from "reactstrap";

class MovieListElement extends React.Component {
  render() {
    return (
      <tr>
        {this.props.id === this.props.selectedId && this.props.enabledEdit ? (
          <>
            <td width="20%">
              <img
                src={this.props.movieImage}
                className="movieListElementImg"
                alt="Movie"
              ></img>
              <br />
              <br />
              <label>Zmień obraz</label>
              <Input
                type="file"
                name="file"
                onChange={this.props.fileSelected}
              />
            </td>
            <td width="20%" className="tablesorter">
              {" "}
              <FormGroup>
                <label>Edytuj tytuł</label>
                <Input
                  required
                  cols="5"
                  defaultValue={this.props.movieName}
                  placeholder="Tytuł filmu"
                  type="textarea"
                  name="name"
                  onChange={(event) => this.props.handleInputChange(event)}
                />
              </FormGroup>
            </td>
            <td width="20%">
              <FormGroup>
                <label>Edytuj opis</label>
                <Input
                  required
                  cols="100"
                  defaultValue={this.props.movieDescription}
                  placeholder="Opis filmu"
                  name="description"
                  onChange={(event) => this.props.handleInputChange(event)}
                  rows="8"
                  type="textarea"
                />
              </FormGroup>
            </td>
          </>
        ) : (
          <>
            <td width="5%">
              <img
                src={this.props.movieImage}
                className="movieListElementImg"
                alt="Movie"
              ></img>
            </td>
            <td width="15%" className="tablesorter">
              {" "}
              {this.props.movieName}
            </td>
            <td width="25%">
              <small>{this.props.movieDescription}</small>
            </td>
          </>
        )}
        <td width="10%" className="text-center">
          <Rating
            placeholderRating={this.props.movieRating}
            emptySymbol="tim-icons icon-shape-star rating"
            fullSymbol="tim-icons icon-shape-star text-success rating"
            placeholderSymbol="tim-icons icon-shape-star text-success rating"
            readonly
          />
        </td>
        <td width="5%" className="text-right">
          <Button
            onClick={() => this.props.enableEdit(this.props.id)}
            color="link"
          >
            <i className="tim-icons icon-pencil" />
          </Button>
        </td>
        <td width="5%" className="text-right">
          <Button
            onClick={() => this.props.submitMovieDelete(this.props.id)}
            color="link"
          >
            <i className="tim-icons icon-trash-simple" />
          </Button>
        </td>
      </tr>
    );
  }
}

export default MovieListElement;

import React from "react";

import {
  Button,
  Card,
  CardHeader,
  CardBody,
  CardTitle,
  Table,
} from "reactstrap";
import MovieListElement from "./MovieListElement";

class MoviePanel extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      enabledEdit: false,
      selectedId: null,
      image: null,
      name: null,
      description: null,
    };
  }

  handleInputChange = (event) => {
    event.preventDefault();
    if (this.props.id === this.props.selectedId) {
      this.setState({ [event.target.name]: event.target.value });
    }
  };

  fileSelected = (event) => {
    this.setState({ fileSelected: event.target.files[0] });
  };

  enableEdit = (id) => {
    if (this.state.selectedId !== id && this.state.selectedId !== null) {
      this.setState({ enabledEdit: true });
    } else {
      this.setState((prevState) => ({
        enabledEdit: !prevState.enabledEdit,
      }));
    }
    this.setState({ selectedId: id });
  };

  disableEdit = (id) => {
    this.setState({ enabledEdit: false });
  };

  submitMovieEdit = () => {
    const movie = {
      id: this.state.selectedId,
      name: this.state.name,
      description: this.state.description,
      image: this.state.image,
    };

    this.props.movieEdit(movie);
  };

  submitMovieDelete = (id) => {
    this.props.movieDelete(id);
  };

  calculateLength = (count) => {
    let elementLength = 66;
    let headerLength = 115;
    return count * elementLength + headerLength;
  };

  render() {
    let movies = null;
    let moviesLength = this.calculateLength(1);
    if (this.props.movies) {
      movies = this.props.movies.map((movie) => {
        return (
          <MovieListElement
            key={movie.id}
            id={movie.id}
            movieName={movie.name}
            movieDescription={movie.description}
            movieImage={movie.logo}
            movieRating={movie.rating}
            enableEdit={this.enableEdit}
            enabledEdit={this.state.enabledEdit}
            submitMovieDelete={this.submitMovieDelete}
            selectedId={this.state.selectedId}
            handleInputChange={this.handleInputChange}
            fileSelected={this.fileSelected}
          />
        );
      });
      moviesLength = this.calculateLength(movies.length);
    }
    return (
      <>
        <Card className="card-tasks">
          <CardHeader>
            <CardTitle tag="h3">
              <i className="tim-icons icon-controller text-info" /> Baza filmów
              {this.state.enabledEdit ? (
                <>
                  <Button
                    className="float-right"
                    color="link text-warning"
                    onClick={this.disableEdit}
                  >
                    <i className="tim-icons icon-simple-remove"></i>
                  </Button>
                  <Button
                    className="float-right"
                    color="success"
                    onClick={this.submitMovieEdit}
                  >
                    Edytuj
                  </Button>
                </>
              ) : (
                <Button
                  onClick={this.props.toggleMovieAdd}
                  className="float-right"
                  color="primary"
                >
                  Dodaj film
                </Button>
              )}
            </CardTitle>
          </CardHeader>
          <CardBody>
            <Table>
              <thead className="text-primary">
                <tr>
                  <th>Obraz</th>
                  <th>Tytuł</th>
                  <th>Opis</th>
                  <th className="text-center">Średnia ocen</th>
                  <th></th>
                </tr>
              </thead>
              <tbody>{movies}</tbody>
            </Table>
          </CardBody>
        </Card>
      </>
    );
  }
}

export default MoviePanel;

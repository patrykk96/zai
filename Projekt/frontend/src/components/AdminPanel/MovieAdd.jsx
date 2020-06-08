import React from "react";

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

class MovieAdd extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      movie: {
        movieName: "",
        movieDescription: "",
        image: null,
      },
    };
  }

  imageSelected = (event) => {
    this.setState({ image: event.target.files[0] });
  };

  submitMovie = (event) => {
    const formData = new FormData();
    formData.append("image", this.state.image);

    const movie = {
      name: this.state.movieName,
      description: this.state.movieDescription,
      image: formData,
    };

    event.preventDefault();
    this.props.movieAdd(movie);
  };

  handleInputChange = (event) => {
    event.preventDefault();
    this.setState({ [event.target.name]: event.target.value });
  };

  render() {
    return (
      <>
        <Row>
          <Col>
            <Card>
              <CardHeader>
                <CardTitle tag="h3">
                  <i className="tim-icons icon-tv text-success" /> Dodaj film
                  <Button
                    className="float-right"
                    color="link text-warning"
                    onClick={this.props.toggleMovieAdd}
                  >
                    <i className="tim-icons icon-simple-remove"></i>
                  </Button>
                </CardTitle>
              </CardHeader>
              <CardBody>
                <form onSubmit={this.submitMovie}>
                  <FormGroup>
                    <label>Tytuł filmu</label>
                    <Input
                      defaultValue=""
                      placeholder="Tytuł filmu"
                      name="movieName"
                      type="text"
                      maxLength="120"
                      required
                      onChange={(event) => this.handleInputChange(event)}
                    />
                  </FormGroup>

                  <FormGroup>
                    <label>Opis filmu</label>
                    <Input
                      defaultValue=""
                      placeholder="Opis filmu"
                      name="movieDescription"
                      type="text"
                      maxLength="200"
                      required
                      onChange={(event) => this.handleInputChange(event)}
                    />
                  </FormGroup>

                  <Input
                    type="file"
                    name="file"
                    onChange={this.imageSelected}
                  />
                  <br />

                  <Button onClick={this.submitMovie} color="primary">
                    Dodaj film
                  </Button>

                  <ErrorText error={this.props.error}></ErrorText>
                </form>
              </CardBody>
            </Card>
          </Col>
        </Row>
      </>
    );
  }
}

export default MovieAdd;

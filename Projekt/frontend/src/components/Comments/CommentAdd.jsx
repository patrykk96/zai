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
} from "reactstrap";
import ErrorText from "components/ErrorText/ErrorText";

class CommentAdd extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      comment: {
        content: "",
      },
    };
  }

  submitComment = (event) => {
    const comment = {
      content: this.state.content,
      reviewId: parseInt(this.props.reviewId),
    };

    event.preventDefault();
    this.props.commentAdd(comment);
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
                  <i className="tim-icons icon-volume-98 text-success" /> Dodaj
                  komentarz
                </CardTitle>
              </CardHeader>
              <CardBody>
                <form onSubmit={this.submitComment}>
                  <FormGroup>
                    <label>Komentarz</label>
                    <Input
                      defaultValue=""
                      placeholder="Treść"
                      name="content"
                      type="text"
                      maxLength="255"
                      required
                      onChange={(event) => this.handleInputChange(event)}
                    />
                  </FormGroup>
                  <br />

                  <Button onClick={this.submitComment} color="primary">
                    Dodaj komentarz
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

export default CommentAdd;

import React from "react";
import "./Comment.css";

import { Button, Input, FormGroup } from "reactstrap";

class Comment extends React.Component {
  render() {
    return (
      <tr>
        <>
          <td width="15%" className="tablesorter">
            {" "}
            {this.props.author}
          </td>
          <td width="15%" className="tablesorter">
            {this.props.created}
            <br />
            <div className="dateText">
              {this.props.isUpdated ? (
                <div>
                  Edytowano:
                  <br />
                  {this.props.updated}{" "}
                </div>
              ) : (
                <></>
              )}
            </div>
          </td>
          {this.props.id === this.props.selectedId && this.props.enabledEdit ? (
            <>
              <td width="20%">
                <FormGroup>
                  <label>Edytuj komentarz</label>
                  <Input
                    required
                    cols="100"
                    defaultValue={this.props.content}
                    placeholder="Treść"
                    name="content"
                    onChange={(event) => this.props.handleInputChange(event)}
                    rows="8"
                    type="textarea"
                  />
                </FormGroup>
              </td>
              <td width="20%">
                <Button
                  className="float-right"
                  color="link text-warning"
                  onClick={this.props.disableEdit}
                >
                  <i className="tim-icons icon-simple-remove"></i>
                </Button>
                <Button
                  className="float-right"
                  color="success"
                  onClick={this.props.submitCommentEdit}
                >
                  Edytuj
                </Button>
              </td>
            </>
          ) : (
            <>
              <td width="50%">{this.props.content}</td>

              {this.props.loggedInUser == this.props.author ? (
                <>
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
                      onClick={() =>
                        this.props.submitCommentDelete(this.props.id)
                      }
                      color="link"
                    >
                      <i className="tim-icons icon-trash-simple" />
                    </Button>
                  </td>{" "}
                </>
              ) : (
                <>
                  <td width="5%" className="text-right"></td>
                  <td width="5%" className="text-right"></td>
                </>
              )}
            </>
          )}
        </>
      </tr>
    );
  }
}

export default Comment;

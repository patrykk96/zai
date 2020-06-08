import React from "react";
import { connect } from "react-redux";

import Comment from "./Comment";
import * as commentActions from "../../store/actions/commentActions";
import CommentAdd from "./CommentAdd";
import Spinner from "../Spinner";

import {
  Button,
  Card,
  CardHeader,
  CardBody,
  CardTitle,
  Table,
} from "reactstrap";

class Comments extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      enabledEdit: false,
      selectedId: null,
      content: "",
    };
  }

  componentDidMount() {
    this.props.commentsGet(this.props.reviewId);
  }

  componentWillUpdate() {
    if (
      this.props.turnOffEdit &&
      this.state.enabledEdit &&
      !this.props.loading
    ) {
      this.disableEdit();
    }
  }

  handleInputChange = (event) => {
    event.preventDefault();
    if (this.props.id === this.props.selectedId) {
      this.setState({ [event.target.name]: event.target.value });
    }
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

  submitCommentEdit = () => {
    if (!this.state.content) {
      this.disableEdit();
      return;
    }
    const comment = {
      reviewId: parseInt(this.props.reviewId),
      content: this.state.content,
    };

    this.props.commentEdit(this.state.selectedId, comment);
  };

  submitCommentDelete = (id) => {
    this.props.commentDelete(id, this.props.reviewId);
  };

  turnOffEdit = () => {
    console.log(this.props.turnOffEdit);
  };

  render() {
    let comments = null;
    if (this.props.comments) {
      comments = this.props.comments.map((comment) => {
        return (
          <Comment
            key={comment.commentId}
            id={comment.commentId}
            author={comment.author}
            loggedInUser={this.props.loggedInUser}
            content={comment.content}
            created={comment.created}
            updated={comment.updated}
            isUpdated={comment.isUpdated}
            enableEdit={this.enableEdit}
            disableEdit={this.disableEdit}
            enabledEdit={this.state.enabledEdit}
            submitCommentEdit={this.submitCommentEdit}
            submitCommentDelete={this.submitCommentDelete}
            selectedId={this.state.selectedId}
            handleInputChange={this.handleInputChange}
          />
        );
      });
    }
    return (
      <div className="content">
        {this.props.loading || !this.props.comments ? (
          <Spinner />
        ) : (
          <>
            <Card className="card-tasks">
              <CardHeader>
                <CardTitle tag="h3">
                  <i className="tim-icons icon-paper text-info" /> Komentarze
                </CardTitle>
              </CardHeader>
              <CardBody>
                <Table>
                  <thead className="text-primary">
                    <tr>
                      <th>Autor</th>
                      <th>Data dodania</th>
                      <th>Treść</th>
                      <th></th>
                    </tr>
                  </thead>
                  <tbody>{comments}</tbody>
                </Table>
              </CardBody>
            </Card>
            {this.props.isAuthenticated ? (
              <CommentAdd
                error={this.props.error}
                reviewId={this.props.reviewId}
                commentAdd={this.props.commentAdd}
              ></CommentAdd>
            ) : (
              <></>
            )}
          </>
        )}
      </div>
    );
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    commentAdd: (comment) => dispatch(commentActions.commentAdd(comment)),
    commentsGet: (reviewId) => dispatch(commentActions.commentsGet(reviewId)),
    commentEdit: (commentId, comment) =>
      dispatch(commentActions.commentEdit(commentId, comment)),
    commentDelete: (commentId, reviewId) =>
      dispatch(commentActions.commentDelete(commentId, reviewId)),
  };
};

const mapStateToProps = (state) => {
  return {
    loading: state.commentReducer.loading,
    error: state.commentReducer.error,
    comments: state.commentReducer.comments,
    turnOffEdit: state.commentReducer.turnOffEdit,
    isAuthenticated: localStorage.getItem("token") !== null,
    loggedInUser: localStorage.getItem("username"),
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(Comments);

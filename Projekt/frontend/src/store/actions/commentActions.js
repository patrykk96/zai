import * as types from "./actionTypes";
import axios from "axios";

export const commentAddStart = () => {
  return {
    type: types.COMMENT_ADD_START,
  };
};

export const commentAddSuccess = (response) => {
  return {
    type: types.COMMENT_ADD_SUCCESS,
    response: response,
  };
};

export const commentAddFailed = (error) => {
  return {
    type: types.COMMENT_ADD_FAILED,
    error: error,
  };
};

export const commentAdd = (comment) => {
  return (dispatch) => {
    dispatch(commentAddStart());

    axios
      .post("/comment/addComment/", comment)
      .then((response) => {
        dispatch(commentAddSuccess(response.status));
        dispatch(commentsGet(comment.reviewId));
      })
      .catch((error) => {
        dispatch(commentAddFailed(error.response.data));
      });
  };
};

export const commentEditStart = () => {
  return {
    type: types.COMMENT_EDIT_START,
  };
};

export const commentEditSuccess = (response) => {
  return {
    type: types.COMMENT_EDIT_SUCCESS,
    response: response,
  };
};

export const commentEditFailed = (error) => {
  return {
    type: types.COMMENT_EDIT_FAILED,
    error: error,
  };
};

export const commentEdit = (commentId, comment) => {
  return (dispatch) => {
    dispatch(commentEditStart());

    axios
      .patch("/comment/updateComment/" + commentId, comment)
      .then((response) => {
        dispatch(commentEditSuccess(response.data));
        dispatch(commentsGet(comment.reviewId));
      })
      .catch((error) => {
        dispatch(commentEditFailed(error.response.data));
      });
  };
};

export const commentsGetStart = () => {
  return {
    type: types.COMMENTS_GET_START,
  };
};

export const commentsGetSuccess = (response) => {
  return {
    type: types.COMMENTS_GET_SUCCESS,
    response: response,
  };
};

export const commentsGetFailed = (error) => {
  return {
    type: types.COMMENTS_GET_FAILED,
    error: error,
  };
};

export const commentsGet = (reviewId) => {
  return (dispatch) => {
    dispatch(commentsGetStart());

    axios
      .get("/comment/getComments/" + reviewId)
      .then((response) => {
        dispatch(commentsGetSuccess(response.data));
      })
      .catch((error) => {
        dispatch(commentsGetFailed(error.response.data));
      });
  };
};

export const commentDeleteStart = () => {
  return {
    type: types.COMMENT_DELETE_START,
  };
};

export const commentDeleteSuccess = (response) => {
  return {
    type: types.COMMENT_DELETE_SUCCESS,
    response: response,
  };
};

export const commentDeleteFailed = (error) => {
  return {
    type: types.COMMENT_DELETE_FAILED,
    error: error,
  };
};

export const commentDelete = (commentId, reviewId) => {
  return (dispatch) => {
    dispatch(commentDeleteStart());

    axios
      .delete("/comment/deleteComment/" + commentId)
      .then((response) => {
        dispatch(commentDeleteSuccess(response.data));
        dispatch(commentsGet(reviewId));
      })
      .catch((error) => {
        dispatch(commentDeleteFailed(error.response.data));
      });
  };
};

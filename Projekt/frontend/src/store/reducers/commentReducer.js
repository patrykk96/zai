import * as actionTypes from "../actions/actionTypes";
import { updateObject } from "../utility";

const initialState = {
  error: null,
  response: null,
  loading: false,
  comments: [],
  turnOffEdit: false,
};

const commentAddStart = (state) => {
  return updateObject(state, { loading: true });
};

const commentAddSuccess = (state, action) => {
  return updateObject(state, {
    error: null,
    loading: false,
    response: action.response,
  });
};

const commentAddFailed = (state, action) => {
  return updateObject(state, {
    error: action.error,
    loading: false,
  });
};

const commentEditStart = (state) => {
  return updateObject(state, { loading: true, turnOffEdit: false });
};

const commentEditSuccess = (state, action) => {
  return updateObject(state, {
    error: null,
    loading: false,
    response: action.response,
    turnOffEdit: true,
  });
};

const commentEditFailed = (state, action) => {
  return updateObject(state, {
    error: action.error,
    loading: false,
    turnOffEdit: false,
  });
};

const commentsGetStart = (state) => {
  return updateObject(state, { loading: true });
};

const commentsGetSuccess = (state, action) => {
  return updateObject(state, {
    error: null,
    loading: false,
    comments: action.response.successResult.list,
  });
};

const commentsGetFailed = (state, action) => {
  return updateObject(state, {
    error: action.error,
    loading: false,
  });
};

const commentDeleteStart = (state) => {
  return updateObject(state, { loading: true });
};

const commentDeleteSuccess = (state, action) => {
  return updateObject(state, {
    error: null,
    loading: false,
    response: action.response,
  });
};

const commentDeleteFailed = (state, action) => {
  return updateObject(state, {
    error: action.error,
    loading: false,
  });
};

const commentReducer = (state = initialState, action) => {
  switch (action.type) {
    case actionTypes.COMMENT_ADD_START:
      return commentAddStart(state);
    case actionTypes.COMMENT_ADD_SUCCESS:
      return commentAddSuccess(state, action);
    case actionTypes.COMMENT_ADD_FAILED:
      return commentAddFailed(state, action);

    case actionTypes.COMMENT_EDIT_START:
      return commentEditStart(state);
    case actionTypes.COMMENT_EDIT_SUCCESS:
      return commentEditSuccess(state, action);
    case actionTypes.COMMENT_EDIT_FAILED:
      return commentEditFailed(state, action);

    case actionTypes.COMMENTS_GET_START:
      return commentsGetStart(state);
    case actionTypes.COMMENTS_GET_SUCCESS:
      return commentsGetSuccess(state, action);
    case actionTypes.COMMENTS_GET_FAILED:
      return commentsGetFailed(state, action);

    case actionTypes.COMMENT_DELETE_START:
      return commentDeleteStart(state);
    case actionTypes.COMMENT_DELETE_SUCCESS:
      return commentDeleteSuccess(state, action);
    case actionTypes.COMMENT_DELETE_FAILED:
      return commentDeleteFailed(state, action);

    default:
      return state;
  }
};

export default commentReducer;

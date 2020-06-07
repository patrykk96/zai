import * as actionTypes from "../actions/actionTypes";
import { updateObject } from "../utility";

const initialState = {
  error: null,
  response: null,
  loading: false,
  reviews: [],
  review: null,
  redirect: false,
};

const movieReviewAddStart = (state) => {
  return updateObject(state, { loading: true, redirect: false });
};

const movieReviewAddSuccess = (state, action) => {
  return updateObject(state, {
    error: null,
    loading: false,
    response: action.response,
    redirect: true,
  });
};

const movieReviewAddFailed = (state, action) => {
  return updateObject(state, {
    error: action.error,
    loading: false,
  });
};

const movieReviewEditStart = (state) => {
  return updateObject(state, { loading: true, redirect: false });
};

const movieReviewEditSuccess = (state, action) => {
  return updateObject(state, {
    error: null,
    loading: false,
    response: action.response,
    redirect: true,
  });
};

const movieReviewEditFailed = (state, action) => {
  return updateObject(state, {
    error: action.error,
    loading: false,
  });
};

const movieReviewGetStart = (state) => {
  return updateObject(state, { loading: true });
};

const movieReviewGetSuccess = (state, action) => {
  return updateObject(state, {
    error: null,
    loading: false,
    review: action.response.successResult,
  });
};

const movieReviewGetFailed = (state, action) => {
  return updateObject(state, {
    error: action.error,
    loading: false,
  });
};

const movieReviewsGetStart = (state) => {
  return updateObject(state, { loading: true });
};

const movieReviewsGetSuccess = (state, action) => {
  return updateObject(state, {
    error: null,
    loading: false,
    reviews: action.response.successResult.list,
  });
};

const movieReviewsGetFailed = (state, action) => {
  return updateObject(state, {
    error: action.error,
    loading: false,
  });
};

const movieReviewDeleteStart = (state) => {
  return updateObject(state, { loading: true });
};

const movieReviewDeleteSuccess = (state, action) => {
  return updateObject(state, {
    error: null,
    loading: false,
    response: action.response,
  });
};

const movieReviewDeleteFailed = (state, action) => {
  return updateObject(state, {
    error: action.error,
    loading: false,
  });
};

const movieReviewReducer = (state = initialState, action) => {
  switch (action.type) {
    case actionTypes.MOVIE_REVIEW_ADD_START:
      return movieReviewAddStart(state);
    case actionTypes.MOVIE_REVIEW_ADD_SUCCESS:
      return movieReviewAddSuccess(state, action);
    case actionTypes.MOVIE_REVIEW_ADD_FAILED:
      return movieReviewAddFailed(state, action);

    case actionTypes.MOVIE_REVIEW_EDIT_START:
      return movieReviewEditStart(state);
    case actionTypes.MOVIE_REVIEW_EDIT_SUCCESS:
      return movieReviewEditSuccess(state, action);
    case actionTypes.MOVIE_REVIEW_EDIT_FAILED:
      return movieReviewEditFailed(state, action);

    case actionTypes.MOVIE_REVIEW_GET_START:
      return movieReviewGetStart(state);
    case actionTypes.MOVIE_REVIEW_GET_SUCCESS:
      return movieReviewGetSuccess(state, action);
    case actionTypes.MOVIE_REVIEW_GET_FAILED:
      return movieReviewGetFailed(state, action);

    case actionTypes.MOVIE_REVIEWS_GET_START:
      return movieReviewsGetStart(state);
    case actionTypes.MOVIE_REVIEWS_GET_SUCCESS:
      return movieReviewsGetSuccess(state, action);
    case actionTypes.MOVIE_REVIEWS_GET_FAILED:
      return movieReviewsGetFailed(state, action);

    case actionTypes.MOVIE_REVIEW_DELETE_START:
      return movieReviewDeleteStart(state);
    case actionTypes.MOVIE_REVIEW_DELETE_SUCCESS:
      return movieReviewDeleteSuccess(state, action);
    case actionTypes.MOVIE_REVIEW_DELETE_FAILED:
      return movieReviewDeleteFailed(state, action);

    default:
      return state;
  }
};

export default movieReviewReducer;

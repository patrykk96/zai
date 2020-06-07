import * as actionTypes from "../actions/actionTypes";
import { updateObject } from "../utility";

const initialState = {
  error: null,
  response: null,
  loading: false,
  movies: [],
  movie: null,
};

const movieAddStart = (state) => {
  return updateObject(state, { loading: true });
};

const movieAddSuccess = (state, action) => {
  return updateObject(state, {
    error: null,
    loading: false,
    response: action.response,
  });
};

const movieAddFailed = (state, action) => {
  return updateObject(state, {
    error: action.error,
    loading: false,
  });
};

const movieEditStart = (state) => {
  return updateObject(state, { loading: true });
};

const movieEditSuccess = (state, action) => {
  return updateObject(state, {
    error: null,
    loading: false,
    response: action.response,
  });
};

const movieEditFailed = (state, action) => {
  return updateObject(state, {
    error: action.error,
    loading: false,
  });
};

const movieGetStart = (state) => {
  return updateObject(state, { loading: true });
};

const movieGetSuccess = (state, action) => {
  return updateObject(state, {
    error: null,
    loading: false,
    movie: action.response.successResult,
  });
};

const movieGetFailed = (state, action) => {
  return updateObject(state, {
    error: action.error,
    loading: false,
  });
};

const moviesGetStart = (state) => {
  return updateObject(state, { loading: true });
};

const moviesGetSuccess = (state, action) => {
  return updateObject(state, {
    error: null,
    loading: false,
    movies: action.response.successResult.list,
  });
};

const moviesGetFailed = (state, action) => {
  return updateObject(state, {
    error: action.error,
    loading: false,
  });
};

const movieDeleteStart = (state) => {
  return updateObject(state, { loading: true });
};

const movieDeleteSuccess = (state, action) => {
  return updateObject(state, {
    error: null,
    loading: false,
    response: action.response,
  });
};

const movieDeleteFailed = (state, action) => {
  return updateObject(state, {
    error: action.error,
    loading: false,
  });
};

const favouriteMovieAddStart = (state) => {
  return updateObject(state, { loading: true });
};

const favouriteMovieAddSuccess = (state, action) => {
  return updateObject(state, {
    error: null,
    loading: false,
    response: action.response,
  });
};

const favouriteMovieAddFailed = (state, action) => {
  return updateObject(state, {
    error: action.error,
    loading: false,
  });
};

const favouriteMovieDeleteStart = (state) => {
  return updateObject(state, { loading: true });
};

const favouriteMovieDeleteSuccess = (state, action) => {
  return updateObject(state, {
    error: null,
    loading: false,
    response: action.response,
  });
};

const favouriteMovieDeleteFailed = (state, action) => {
  return updateObject(state, {
    error: action.error,
    loading: false,
  });
};

const favouriteMoviesGetStart = (state) => {
  return updateObject(state, { loading: true });
};

const favouriteMoviesGetSuccess = (state, action) => {
  return updateObject(state, {
    error: null,
    loading: false,
    movies: action.response.successResult.list,
  });
};

const favouriteMoviesGetFailed = (state, action) => {
  return updateObject(state, {
    error: action.error,
    loading: false,
  });
};

const movieReducer = (state = initialState, action) => {
  switch (action.type) {
    case actionTypes.MOVIE_ADD_START:
      return movieAddStart(state);
    case actionTypes.MOVIE_ADD_SUCCESS:
      return movieAddSuccess(state, action);
    case actionTypes.MOVIE_ADD_FAILED:
      return movieAddFailed(state, action);

    case actionTypes.MOVIE_EDIT_START:
      return movieEditStart(state);
    case actionTypes.MOVIE_EDIT_SUCCESS:
      return movieEditSuccess(state, action);
    case actionTypes.MOVIE_EDIT_FAILED:
      return movieEditFailed(state, action);

    case actionTypes.MOVIE_GET_START:
      return movieGetStart(state);
    case actionTypes.MOVIE_GET_SUCCESS:
      return movieGetSuccess(state, action);
    case actionTypes.MOVIE_GET_FAILED:
      return movieGetFailed(state, action);

    case actionTypes.MOVIES_GET_START:
      return moviesGetStart(state);
    case actionTypes.MOVIES_GET_SUCCESS:
      return moviesGetSuccess(state, action);
    case actionTypes.MOVIES_GET_FAILED:
      return moviesGetFailed(state, action);

    case actionTypes.MOVIE_DELETE_START:
      return movieDeleteStart(state);
    case actionTypes.MOVIE_DELETE_SUCCESS:
      return movieDeleteSuccess(state, action);
    case actionTypes.MOVIE_DELETE_FAILED:
      return movieDeleteFailed(state, action);

    case actionTypes.FAVOURITE_MOVIE_ADD_START:
      return favouriteMovieAddStart(state);
    case actionTypes.FAVOURITE_MOVIE_ADD_SUCCESS:
      return favouriteMovieAddSuccess(state, action);
    case actionTypes.FAVOURITE_MOVIE_ADD_FAILED:
      return favouriteMovieAddFailed(state, action);

    case actionTypes.FAVOURITE_MOVIE_DELETE_START:
      return favouriteMovieDeleteStart(state);
    case actionTypes.FAVOURITE_MOVIE_DELETE_SUCCESS:
      return favouriteMovieDeleteSuccess(state, action);
    case actionTypes.FAVOURITE_MOVIE_DELETE_FAILED:
      return favouriteMovieDeleteFailed(state, action);

    case actionTypes.FAVOURITE_MOVIES_GET_START:
      return favouriteMoviesGetStart(state);
    case actionTypes.FAVOURITE_MOVIES_GET_SUCCESS:
      return favouriteMoviesGetSuccess(state, action);
    case actionTypes.FAVOURITE_MOVIES_GET_FAILED:
      return favouriteMoviesGetFailed(state, action);

    default:
      return state;
  }
};

export default movieReducer;

import * as types from "./actionTypes";
import axios from "axios";

export const movieReviewAddStart = () => {
  return {
    type: types.MOVIE_REVIEW_ADD_START,
  };
};

export const movieReviewAddSuccess = (response) => {
  return {
    type: types.MOVIE_REVIEW_ADD_SUCCESS,
    response: response,
  };
};

export const movieReviewAddFailed = (error) => {
  return {
    type: types.MOVIE_REVIEW_ADD_FAILED,
    error: error,
  };
};

export const movieReviewAdd = (review) => {
  return (dispatch) => {
    dispatch(movieReviewAddStart());

    axios
      .post("/review/addReview/", review)
      .then((response) => {
        dispatch(movieReviewAddSuccess(response.status));
      })
      .catch((error) => {
        dispatch(movieReviewAddFailed(error.response.data));
      });
  };
};

export const movieReviewEditStart = () => {
  return {
    type: types.MOVIE_REVIEW_EDIT_START,
  };
};

export const movieReviewEditSuccess = (response) => {
  return {
    type: types.MOVIE_REVIEW_EDIT_SUCCESS,
    response: response,
  };
};

export const movieReviewEditFailed = (error) => {
  return {
    type: types.MOVIE_EDIT_FAILED,
    error: error,
  };
};

export const movieReviewEdit = (reviewId, review) => {
  return (dispatch) => {
    dispatch(movieReviewEditStart());
    axios
      .patch("/review/updateReview/" + reviewId, review)
      .then((response) => {
        dispatch(movieReviewEditSuccess(response.data));
      })
      .catch((error) => {
        dispatch(movieReviewEditFailed(error.response.data));
      });
  };
};

export const movieReviewGetStart = () => {
  return {
    type: types.MOVIE_REVIEW_GET_START,
  };
};

export const movieReviewGetSuccess = (response) => {
  return {
    type: types.MOVIE_REVIEW_GET_SUCCESS,
    response: response,
  };
};

export const movieReviewGetFailed = (error) => {
  return {
    type: types.MOVIE_REVIEW_GET_FAILED,
    error: error,
  };
};

export const movieReviewGet = (reviewId) => {
  return (dispatch) => {
    dispatch(movieReviewGetStart());

    axios
      .get("/review/getReview/" + reviewId)
      .then((response) => {
        dispatch(movieReviewGetSuccess(response.data));
      })
      .catch((error) => {
        dispatch(movieReviewGetFailed(error.response.data));
      });
  };
};

export const movieReviewsGetStart = () => {
  return {
    type: types.MOVIE_REVIEWS_GET_START,
  };
};

export const movieReviewsGetSuccess = (response) => {
  return {
    type: types.MOVIE_REVIEWS_GET_SUCCESS,
    response: response,
  };
};

export const movieReviewsGetFailed = (error) => {
  return {
    type: types.MOVIE_REVIEWS_GET_FAILED,
    error: error,
  };
};

export const movieReviewsGet = (movieId) => {
  return (dispatch) => {
    dispatch(movieReviewsGetStart());

    axios
      .get("/review/getReviews/" + movieId)
      .then((response) => {
        dispatch(movieReviewsGetSuccess(response.data));
      })
      .catch((error) => {
        dispatch(movieReviewsGetFailed(error.response.data));
      });
  };
};

export const movieReviewDeleteStart = () => {
  return {
    type: types.MOVIE_REVIEW_DELETE_START,
  };
};

export const movieReviewDeleteSuccess = (response) => {
  return {
    type: types.MOVIE_REVIEW_DELETE_SUCCESS,
    response: response,
  };
};

export const movieReviewDeleteFailed = (error) => {
  return {
    type: types.MOVIE_REVIEW_DELETE_FAILED,
    error: error,
  };
};

export const movieReviewDelete = (reviewId) => {
  return (dispatch) => {
    dispatch(movieReviewDeleteStart());

    axios
      .delete("/review/deleteReview/" + reviewId)
      .then((response) => {
        dispatch(movieReviewDeleteSuccess(response.data));
      })
      .catch((error) => {
        dispatch(movieReviewDeleteFailed(error.response.data));
      });
  };
};

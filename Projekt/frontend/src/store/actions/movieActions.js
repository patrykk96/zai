import * as types from "./actionTypes";
import axios from "axios";

export const movieAddStart = () => {
    return {
        type: types.MOVIE_ADD_START
    }
}

export const movieAddSuccess = response => {
    return {
        type: types.MOVIE_ADD_SUCCESS,
        response: response
    }
}

export const movieAddFailed = error => {
    return {
        type: types.MOVIE_ADD_FAILED,
        error: error
    }
}

export const movieAdd = movie => {
    return dispatch => {
        dispatch(movieAddStart());

        axios.post("url do uzupełnienia")
             .then(response =>{
                 dispatch(movieAddSuccess(response.status));
             })
             .catch(error => {
                 dispatch(movieAddFailed(error.response.status))
             });
    }
}
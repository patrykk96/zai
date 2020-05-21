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

        axios.post("/movie/addMovie/" + movie.name + "/" + movie.description, movie.image)
             .then(response =>{
                 dispatch(movieAddSuccess(response.status));
             })
             .catch(error => {
                 dispatch(movieAddFailed(error.response.data))
             });
    }
}

export const movieEditStart = () => {
    return {
        type: types.MOVIE_EDIT_START
    }
}

export const movieEditSuccess = response => {
    return {
        type: types.MOVIE_EDIT_SUCCESS,
        response: response
    }
}

export const movieEditFailed = error => {
    return {
        type: types.MOVIE_EDIT_FAILED,
        error: error
    }
}

export const movieEdit = movie => {
    return dispatch => {
        dispatch(movieEditStart());

        const formData = new FormData();
        formData.append("Image", movie.image);

        axios.patch("/movie/updateMovie/" + movie.id + "/" + movie.name + "/" + movie.description, formData)
             .then(response => {
                 dispatch(movieEditSuccess(response.data));
                 dispatch(moviesGet());
             })
             .catch(error => {
                 dispatch(movieEditFailed(error.response.data));
             });
    }
}

export const movieGetStart = () => {
    return {
        type: types.MOVIE_GET_START
    }
}

export const movieGetSuccess = response => {
    return {
        type: types.MOVIE_GET_SUCCESS,
        response: response
    }
}

export const movieGetFailed = error => {
    return {
        type: types.MOVIE_GET_FAILED,
        error: error
    }
}

export const movieGet = movie => {
    return dispatch => {
        dispatch(movieGetStart());

        axios.get("/movie/getMovie/" + movie)
             .then(response => {
                 dispatch(movieGetSuccess(response.data));
             })
             .catch(error => {
                 dispatch(movieGetFailed(error.response.data));
             });
    }
}

export const moviesGetStart = () => {
    return {
        type: types.MOVIES_GET_START
    }
}

export const moviesGetSuccess = response => {
    return {
        type: types.MOVIES_GET_SUCCESS,
        response: response
    }
}

export const moviesGetFailed = error => {
    return {
        type: types.MOVIES_GET_FAILED,
        error: error
    }
}

export const moviesGet = () => {
    return dispatch => {
        dispatch(moviesGetStart());

        axios.get("/movie/getMovies")
             .then(response => {
                 dispatch(moviesGetSuccess(response.data));
             })
             .catch(error => {
                 dispatch(moviesGetFailed(error.response.data));
             });
    }
}

export const movieDeleteStart = () => {
    return {
        type: types.MOVIE_DELETE_START
    }
}

export const movieDeleteSuccess = response => {
    return {
        type: types.MOVIE_DELETE_SUCCESS,
        response: response
    }
}

export const movieDeleteFailed = error => {
    return {
        type: types.MOVIE_DELETE_FAILED,
        error: error
    }
}

export const movieDelete = movie => {
    return dispatch => {
        dispatch(movieDeleteStart());

        axios.delete("/movie/deleteMovie/" + movie)
             .then(response => {
                 dispatch(movieDeleteSuccess(response.data));
                 dispatch(moviesGet());
             })
             .catch(error => {
                 dispatch(movieDeleteFailed(error.response.data));
             });
    }
}



import * as actionTypes from '../actions/actionTypes';
import { updateObject } from '../utility';

const initialState = {
    error: null,
    response: null,
    loading: false
}

const movieAddStart = state => {
    return updateObject(state, {loading: true});
}

const movieAddSuccess = (state, action) => {
    return updateObject(state , {
        error: null,
        loading: false,
        response: action.response
    });
}

const movieAddFailed = (state, action) => {
    return updateObject(state, {
        error: action.error,
        loading: false
    });
}

const movieReducer = (state = initialState, action) => {
    switch(action.type){
        case actionTypes.MOVIE_ADD_START: return movieAddStart(state);
        case actionTypes.MOVIE_ADD_SUCCESS: return movieAddSuccess(state, action);
        case actionTypes.MOVIE_ADD_FAILED: return movieAddFailed(state, action);

        default: return state
    };
};

export default movieReducer;
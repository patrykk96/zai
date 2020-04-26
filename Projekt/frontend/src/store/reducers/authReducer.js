import * as actionTypes from '../actions/actionTypes';
import { updateObject } from '../utility';

const initialState = {
    email: null,
    token: null,
    error: null,
    response: null,
    loading: false
}

const registerStart = state => {
    return updateObject(state, {loading: true});
}

const registerSuccess = (state, action) => {
    return updateObject(state, {error: null, loading: false, response: action.response});
};

const registerFailed = (state, action) => {
    return updateObject(state, {error: action.error, loading: false});
}


const loginStart = ( state ) => {
    return updateObject( state, { 
        error: null, 
        loading: true});
}

const loginSuccess = ( state, action ) => {
    return updateObject( state, {
        token: action.token,
        error: null, loading: false});
}

const loginFailed = ( state, action ) => {
    return updateObject( state, {
        error: action.error,
        loading: false });
}

const logout = ( state ) => {
    return updateObject( state, {
        token: null});
}

const resetErrors = ( state ) => {
    return updateObject( state, { 
        error: null,
        response: null,
        confirmAccount: null,
        resetPasswordResult: null,
        changePasswordResult: null,
    });
}


const authReducer = (state = initialState, action) => {
    switch(action.type){
        case actionTypes.REGISTER_START: return registerStart(state);
        case actionTypes.REGISTER_SUCCESS: return registerSuccess(state, action);
        case actionTypes.REGISTER_FAILED: return registerFailed(state, action);

        case actionTypes.LOGIN_START: return loginStart(state, action);
        case actionTypes.LOGIN_SUCCESS: return loginSuccess(state, action);
        case actionTypes.LOGIN_FAILED: return loginFailed(state, action);

        case actionTypes.LOGOUT: return logout(state);

        case actionTypes.RESET_ERRORS: return resetErrors(state);

        default: return state
    };
};

export default authReducer;
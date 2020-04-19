import * as types from './actionTypes';
import axios from 'axios';

export const registerStart = () => {
    return {
        type: types.REGISTER_START
    };
};

export const registerSuccess = response => {
    return{
        type: types.REGISTER_SUCCESS,
        response: response
    };
};

export const registerFailed = error => {
    return{
        type: types.REGISTER_FAILED,
        error: error
    };
};

export const register = user => {
    return dispatch => {
        dispatch(registerStart());

        axios.post("/user/register", user)
             .then(response => {
                 dispatch(registerSuccess(response.status));
             })
             .catch(error => {
                 dispatch(registerFailed(error.response.data.error));
             });
    };
};

export const loginStart = () => {
    return {
      type: types.LOGIN_START
    };
  };
  
  export const loginSuccess = token => {
    return {
      type: types.LOGIN_SUCCESS,
      token: token
    };
  };
  
  export const loginFailed = error => {
    return {
      type: types.LOGIN_FAILED,
      error: error
    };
  };
  
  export const login = user => {
    return dispatch => {
      dispatch(loginStart());
  
      axios
        .post("/user/login", user)
        .then(response => {
          localStorage.setItem("token", response.data.successResult.token);
          dispatch(loginSuccess(response.data.successResult.token));
          //window.location.reload();
        })
        .catch(error => {
          dispatch(loginFailed(error.response.data));
        });
    };
  };

  export const logout = () => {
    localStorage.clear();
    return {
      type: types.LOGOUT
    };
  };
  

  export const resetErrors = () => {
    return {
      type: types.RESET_ERRORS
    };
  };

  
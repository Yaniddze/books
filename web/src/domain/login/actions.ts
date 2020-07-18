import {
  LOGIN_ASYNC,
  LOGIN_CLEAN,
  LOGIN_ERROR,
  LOGIN_FINISH,
  LOGIN_START,
  LOGIN_SUCCESS,
  LOGOUT,
  LoginActions,
  LoginAsyncAction,
  LoginErrorAction,
  LoginInfo,
  LoginSuccessAction,
} from './types';

export function start(): LoginActions {
  return {
    type: LOGIN_START,
  };
}

export function finish(): LoginActions {
  return {
    type: LOGIN_FINISH,
  };
}

export function error(payload: string[]): LoginErrorAction {
  return {
    type: LOGIN_ERROR,
    payload,
  };
}

export function success(): LoginSuccessAction {
  return {
    type: LOGIN_SUCCESS,
  };
}

export function logout(): LoginActions {
  return {
    type: LOGOUT,
  };
}

export function clean(): LoginActions {
  return {
    type: LOGIN_CLEAN,
  };
}

export function loginAsync(payload: LoginInfo): LoginAsyncAction {
  return {
    type: LOGIN_ASYNC,
    payload,
  };
}

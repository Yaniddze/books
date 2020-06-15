import {
  LOGIN_START,
  LOGIN_ASYNC,
  LOGIN_ERROR,
  LOGIN_SUCCESS,
  LOGIN_FINISH,
  LOGOUT,
  LoginActions,
  LoginErrorAction,
  LoginSuccessAction,
  LoginAsyncAction,
  LoginInfo,
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

export function success(payload: string): LoginSuccessAction {
  return {
    type: LOGIN_SUCCESS,
    payload,
  };
}

export function logout(): LoginActions {
  return {
    type: LOGOUT,
  };
}

export function loginAsync(payload: LoginInfo): LoginAsyncAction {
  return {
    type: LOGIN_ASYNC,
    payload,
  };
}

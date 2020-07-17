export type LoginState = {
  isFetching: boolean;
  data: LoginAnswer;
}

export type LoginAnswer = {
  success: boolean;
  data: string;
  errors: string[];
}

export type LoginInfo = {
  login: string;
  password: string;
}

export const LOGIN_START = 'LOGIN_START';
type LoginStartAction = {
  type: typeof LOGIN_START;
};

export const LOGIN_FINISH = 'LOGIN_FINISH';
type LoginFinishAction = {
  type: typeof LOGIN_FINISH;
}

export const LOGIN_SUCCESS = 'LOGIN_SUCCESS';
export type LoginSuccessAction = {
  type: typeof LOGIN_SUCCESS;
  payload: string; // Token
}

export const LOGIN_ERROR = 'LOGIN_ERROR';
export type LoginErrorAction = {
  type: typeof LOGIN_ERROR;
  payload: string[]; // Errors
}

export const LOGIN_CLEAN = 'LOGIN_CLEAN';
export type LoginCleanAction = {
  type: typeof LOGIN_CLEAN;
}

export const LOGOUT = 'LOGOUT';
export type LogoutAction = {
  type: typeof LOGOUT;
}

export const LOGIN_ASYNC = 'LOGIN_ASYNC';
export type LoginAsyncAction = {
  type: typeof LOGIN_ASYNC;
  payload: LoginInfo;
}

export type LoginActions =
  | LoginStartAction
  | LoginFinishAction
  | LoginSuccessAction
  | LoginErrorAction
  | LoginAsyncAction
  | LogoutAction
  | LoginCleanAction;

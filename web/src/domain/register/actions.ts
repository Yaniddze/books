import {
  REGISTER_ASYNC,
  REGISTER_CLEAN,
  REGISTER_ERROR,
  REGISTER_FINISH,
  REGISTER_START,
  REGISTER_SUCCESS,
  RegisterActions,
  RegisterAsyncAction,
  RegisterErrorAction,
  RegisterInfo,
} from './types';

export function start(): RegisterActions {
  return {
    type: REGISTER_START,
  };
}

export function finish(): RegisterActions {
  return {
    type: REGISTER_FINISH,
  };
}

export function success(): RegisterActions {
  return {
    type: REGISTER_SUCCESS,
  };
}

export function error(payload: string[]): RegisterErrorAction {
  return {
    type: REGISTER_ERROR,
    payload,
  };
}

export function clean(): RegisterActions {
  return {
    type: REGISTER_CLEAN,
  };
}

export function registerAsync(payload: RegisterInfo): RegisterAsyncAction {
  return {
    type: REGISTER_ASYNC,
    payload,
  };
}

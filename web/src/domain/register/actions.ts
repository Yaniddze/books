import {
  REGISTER_ASYNC,
  REGISTER_SUCCESS,
  REGISTER_FINISH,
  REGISTER_START,
  REGISTER_ERROR,
  RegisterActions,
  RegisterErrorAction,
  RegisterAsyncAction,
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

export function registerAsync(payload: RegisterInfo): RegisterAsyncAction {
  return {
    type: REGISTER_ASYNC,
    payload,
  };
}

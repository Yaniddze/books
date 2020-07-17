export type RegisterState = {
  isFetching: boolean;
  data: {
    success: boolean;
    errors: string[];
  };
}

export type RegisterAnswer = {
  success: boolean;
  errors: string[];
  data: string;
}

export type RegisterInfo = {
  login: string;
  password: string;
}

export const REGISTER_START = 'REGISTER_START';
type RegisterStartAction = {
  type: typeof REGISTER_START;
};

export const REGISTER_FINISH = 'REGISTER_FINISH';
type RegisterFinishAction = {
  type: typeof REGISTER_FINISH;
};

export const REGISTER_SUCCESS = 'REGISTER_SUCCESS';
export type RegisterSuccessAction = {
  type: typeof REGISTER_SUCCESS;
}

export const REGISTER_ERROR = 'REGISTER_ERROR';
export type RegisterErrorAction = {
  type: typeof REGISTER_ERROR;
  payload: string[]; // Errors
}

export const REGISTER_CLEAN = 'REGISTER_CLEAN';
export type RegisterCleanAction = {
  type: typeof REGISTER_CLEAN;
}

export const REGISTER_ASYNC = 'REGISTER_ASYNC';
export type RegisterAsyncAction = {
  type: typeof REGISTER_ASYNC;
  payload: RegisterInfo;
}

export type RegisterActions =
  | RegisterStartAction
  | RegisterAsyncAction
  | RegisterErrorAction
  | RegisterSuccessAction
  | RegisterFinishAction
  | RegisterCleanAction;

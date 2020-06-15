import {
  REGISTER_SUCCESS,
  REGISTER_ERROR,
  REGISTER_START,
  REGISTER_FINISH,
  REGISTER_ASYNC,
  REGISTER_CLEAN,
  RegisterState,
  RegisterActions,
} from './types';

const initialState: RegisterState = {
  isFetching: false,
  data: {
    success: false,
    errors: [],
  },
};

export function registerReducer(state = initialState, action: RegisterActions): RegisterState {
  switch (action.type) {
    case REGISTER_START:
      return {
        ...state,
        isFetching: true,
        data: {
          ...state.data,
          errors: [],
        },
      };
    case REGISTER_FINISH:
      return {
        ...state,
        isFetching: false,
      };
    case REGISTER_ERROR:
      return {
        ...state,
        isFetching: false,
        data: {
          success: false,
          errors: action.payload,
        },
      };
    case REGISTER_CLEAN:
      return initialState;
    case REGISTER_ASYNC:
      return state;
    case REGISTER_SUCCESS:
      return state;
    default:
      // eslint-disable-next-line @typescript-eslint/no-unused-vars,no-case-declarations
      const x: never = action;
  }

  return state;
}

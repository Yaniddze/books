// Types
import {
  LOGIN_ASYNC,
  LOGIN_CLEAN,
  LOGIN_ERROR,
  LOGIN_FINISH,
  LOGIN_START,
  LOGOUT,
  LOGIN_SUCCESS,
  LoginActions,
  LoginState,
} from './types';

const initialState: LoginState = {
  isFetching: false,
  data: {
    errors: [],
    success: false,
    data: true,
  },
};

export function loginReducer(state = initialState, action: LoginActions): LoginState {
  switch (action.type) {
    case LOGIN_START:
      return {
        ...state,
        isFetching: true,
        data: {
          errors: [],
          success: true,
          data: true,
        },
      };
    case LOGIN_ERROR:
      return {
        ...state,
        isFetching: false,
        data: {
          errors: action.payload,
          success: false,
          data: false,
        },
      };
    case LOGIN_FINISH:
      return {
        ...state,
        isFetching: false,
      };
    case LOGIN_SUCCESS:
      return {
        ...state,
        isFetching: false,
        data: {
          success: true,
          errors: [],
          data: true,
        },
      };
    case LOGOUT:
      return {
        ...state,
        data: {
          ...state.data,
          data: false,
        },
      };
    case LOGIN_CLEAN:
      return {
        ...initialState,
        data: {
          ...initialState.data,
          data: state.data.data,
        },
      };
    case LOGIN_ASYNC:
      return state;
    default:
      // eslint-disable-next-line @typescript-eslint/no-unused-vars,no-case-declarations
      const x: never = action;
  }

  return state;
}

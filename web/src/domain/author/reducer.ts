import {
  AUTHOR_ERROR,
  AUTHOR_FETCH_ASYNC,
  AUTHOR_FILL,
  AUTHOR_FINISH,
  AUTHOR_START,
  AuthorsActionTypes,
  AuthorsFetch,
} from './types';

const initialState: AuthorsFetch = {
  data: {
    data: [],
    error: [],
    success: false,
  },
  isFetching: false,
};

export function authorReducer(state = initialState, action: AuthorsActionTypes): AuthorsFetch {
  switch (action.type) {
    case AUTHOR_START:
      return {
        ...state,
        isFetching: true,
      };
    case AUTHOR_FINISH:
      return {
        ...state,
        isFetching: false,
      };
    case AUTHOR_FILL:
      return {
        ...state,
        data: action.payload,
      };
    case AUTHOR_ERROR:
      return {
        ...state,
        isFetching: false,
        data: {
          data: [],
          error: action.payload,
          success: false,
        },
      };
    case AUTHOR_FETCH_ASYNC:
      return state;
    default:
      // eslint-disable-next-line no-case-declarations,@typescript-eslint/no-unused-vars
      const x: never = action;
  }
  return state;
}

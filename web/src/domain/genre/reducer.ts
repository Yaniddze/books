import {
  GENRE_START,
  GENRE_FINISH,
  GENRE_FETCH_ASYNC,
  GENRE_ERROR,
  GENRE_FILL,
  GenreFetch,
  FetchGenreActions,
} from './types';

const initialState: GenreFetch = {
  isFetching: false,
  data: {
    error: [],
    success: true,
    genres: [],
  },
};

export function genreReducer(state = initialState, action: FetchGenreActions): GenreFetch {
  switch (action.type) {
    case GENRE_START:
      return {
        ...state,
        isFetching: true,
      };
    case GENRE_FINISH:
      return {
        ...state,
        isFetching: false,
      };
    case GENRE_ERROR:
      return {
        isFetching: false,
        data: {
          success: false,
          error: action.payload,
          genres: [],
        },
      };
    case GENRE_FILL:
      return {
        isFetching: false,
        data: action.payload,
      };
    case GENRE_FETCH_ASYNC:
      return state;
    default:
      // eslint-disable-next-line no-case-declarations,@typescript-eslint/no-unused-vars
      const x: never = action;
  }

  return state;
}

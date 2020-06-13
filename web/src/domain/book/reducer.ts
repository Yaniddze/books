// Types
import {
  BOOK_START,
  BOOK_FETCH_ASYNC,
  BOOK_FINISH,
  BOOK_FILL,
  BOOK_ERROR,
  BooksActionTypes,
  BookState,
} from './types';

const initialState: BookState = {
  data: {
    success: false,
    errors: [],
    books: [],
  },
  isFetching: false,
};

export const booksReducer = (
  state = initialState,
  action: BooksActionTypes,
): BookState => {
  switch (action.type) {
    case BOOK_START:
      return {
        ...state,
        isFetching: true,
      };
    case BOOK_FINISH:
      return {
        ...state,
        isFetching: false,
      };
    case BOOK_ERROR:
      return {
        ...state,
        data: {
          success: false,
          errors: action.payload,
          books: [],
        },
        isFetching: false,
      };
    case BOOK_FILL:
      return {
        ...state,
        isFetching: false,
        data: {
          ...action.payload,
        },
      };
    case BOOK_FETCH_ASYNC:
      return state;
    default:
      // eslint-disable-next-line no-case-declarations,@typescript-eslint/no-unused-vars
      const x: never = action;
  }
  return state;
};

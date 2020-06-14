// Types
import {
  BOOK_ADD_ASYNC,
  BOOK_ADDED_SUCCESS,
  BOOK_ERROR,
  BOOK_FETCH_ASYNC,
  BOOK_FILL,
  BOOK_FINISH,
  BOOK_START,
  BOOK_UPDATE_ASYNC,
  BOOK_UPDATE_SUCCESS,
  BOOKS_DELETE_ASYNC,
  BOOKS_DELETED_SUCCESS,
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
    case BOOK_UPDATE_SUCCESS:
      return {
        ...state,
        isFetching: false,
        data: {
          ...state.data,
          books: state.data.books.map((book) => (
            book.id === action.payload.id ? action.payload : book
          )),
        },
      };
    case BOOK_ADDED_SUCCESS:
      return {
        ...state,
        data: {
          ...state.data,
          books: [...state.data.books, action.payload],
        },
      };
    case BOOKS_DELETED_SUCCESS:
      return {
        ...state,
        data: {
          ...state.data,
          books: state.data.books.filter((x) => !action.payload.includes(x.id)),
        },
      };
    case BOOK_FETCH_ASYNC:
      return state;
    case BOOK_UPDATE_ASYNC:
      return state;
    case BOOK_ADD_ASYNC:
      return state;
    case BOOKS_DELETE_ASYNC:
      return state;
    default:
      // eslint-disable-next-line no-case-declarations,@typescript-eslint/no-unused-vars
      const x: never = action;
  }
  return state;
};

// Types
import {
  BOOK_ERROR,
  BOOK_FETCH_ASYNC,
  BOOK_FILL,
  BOOK_FINISH,
  BOOK_START,
  BookErrorAction,
  BookFillAction,
  Books,
  BooksActionTypes,
  ErrorHttpAction,
} from './types';

// Sync
export function start(): BooksActionTypes {
  return {
    type: BOOK_START,
  };
}

export function finish(): BooksActionTypes {
  return {
    type: BOOK_FINISH,
  };
}

export function fill(payload: Books): BookFillAction {
  return {
    type: BOOK_FILL,
    payload,
  };
}

export function error(payload: ErrorHttpAction): BookErrorAction {
  return {
    type: BOOK_ERROR,
    payload,
  };
}

export function fetchAsync(): BooksActionTypes {
  return {
    type: BOOK_FETCH_ASYNC,
  };
}

// Types
import {
  Book,
  BOOK_ERROR,
  BOOK_FETCH_ASYNC,
  BOOK_FILL,
  BOOK_FINISH,
  BOOK_START, BOOK_UPDATE_ASYNC,
  BOOK_UPDATE_SUCCESS,
  BookErrorAction,
  BookFillAction,
  Books,
  BooksActionTypes,
  BookUpdateSuccessAction,
  BookUpdateAsyncAction,
  BookToUpdate,
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

export function error(payload: string[]): BookErrorAction {
  return {
    type: BOOK_ERROR,
    payload,
  };
}

export function bookUpdateSuccess(payload: Book): BookUpdateSuccessAction {
  return {
    type: BOOK_UPDATE_SUCCESS,
    payload,
  };
}

// Async
export function fetchAsync(): BooksActionTypes {
  return {
    type: BOOK_FETCH_ASYNC,
  };
}

export function updateAsync(payload: BookToUpdate): BookUpdateAsyncAction {
  return {
    type: BOOK_UPDATE_ASYNC,
    payload,
  };
}

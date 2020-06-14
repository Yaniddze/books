// Types
import {
  Book,
  BOOK_ADD_ASYNC,
  BOOK_ADDED_SUCCESS,
  BOOK_ERROR,
  BOOK_FETCH_ASYNC,
  BOOK_FILL,
  BOOK_FINISH,
  BOOK_START,
  BOOK_UPDATE_ASYNC,
  BOOK_UPDATE_SUCCESS,
  BookAddAsyncAction,
  BookAddedSuccessAction,
  BookErrorAction,
  BookFillAction,
  Books,
  BOOKS_DELETE_ASYNC,
  BOOKS_DELETED_SUCCESS,
  BooksActionTypes,
  BooksDeleteAsyncAction,
  BooksDeletedSuccessAction,
  BookToAdd,
  BookToUpdate,
  BookUpdateAsyncAction,
  BookUpdateSuccessAction,
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

export function bookAddSuccess(payload: Book): BookAddedSuccessAction {
  return {
    type: BOOK_ADDED_SUCCESS,
    payload,
  };
}

export function booksDeleteSuccess(payload: string[]): BooksDeletedSuccessAction {
  return {
    type: BOOKS_DELETED_SUCCESS,
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

export function addAsync(payload: BookToAdd): BookAddAsyncAction {
  return {
    type: BOOK_ADD_ASYNC,
    payload,
  };
}

export function deleteAsync(payload: string[]): BooksDeleteAsyncAction {
  return {
    type: BOOKS_DELETE_ASYNC,
    payload,
  };
}

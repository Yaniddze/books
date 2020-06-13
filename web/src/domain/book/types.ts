// Instruments
import { Author } from '../author/types';
import { Genre } from '../genre/types';

export type Book = {
  bookInfo: {
    title: string;
    year: number;
  };
  author: Author;
  genre: Genre;
  id: string;
};

export type BookState = {
  data: Books;
  isFetching: boolean;
};

export type Books = {
  success: boolean;
  errors: string[];
  books: Book[];
};

export type ErrorHttpAction = string[];

// Sync
export const BOOK_START = 'BOOK_START';
type BookStartAction = {
  type: typeof BOOK_START;
};

export const BOOK_FINISH = 'BOOK_FINISH';
type BookFinishAction = {
  type: typeof BOOK_FINISH;
};

export const BOOK_FILL = 'BOOK_FILL';
export type BookFillAction = {
  type: typeof BOOK_FILL;
  payload: Books;
};

export const BOOK_ERROR = 'BOOK_ERROR';
export type BookErrorAction = {
  type: typeof BOOK_ERROR;
  payload: ErrorHttpAction;
};

// Async
export const BOOK_FETCH_ASYNC = 'BOOK_FETCH_ASYNC';
type BookFetchAsyncAction = {
  type: typeof BOOK_FETCH_ASYNC;
};

export type BooksActionTypes =
  | BookStartAction
  | BookFinishAction
  | BookFillAction
  | BookErrorAction
  | BookFetchAsyncAction;

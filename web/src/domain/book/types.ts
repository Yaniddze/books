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

export type BookToUpdate = {
  bookId: string;
  newYear: number;
  newTitle: string;
  newAuthorId: string;
  newAuthorName: string;
  newGenreTitle: string;
  newGenreId: string;
}

export type BookToAdd = {
  bookTitle: string;
  year: number;
  authorId: string;
  authorName: string;
  genreId: string;
  genreTitle: string;
}

export type BookState = {
  data: Books;
  isFetching: boolean;
};

export type BookUpdateState = {
  success: boolean;
  errors: string[];
}

export type BookAddState = {
  success: boolean;
  errors: string[];
  bookId: string;
}

export type Books = {
  success: boolean;
  errors: string[];
  books: Book[];
};

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
  payload: string[];
};

export const BOOK_UPDATE_SUCCESS = 'BOOK_UPDATE_SUCCESS';
export type BookUpdateSuccessAction = {
  type: typeof BOOK_UPDATE_SUCCESS;
  payload: Book;
}

export const BOOK_ADDED_SUCCESS = 'BOOK_ADDED_SUCCESS';
export type BookAddedSuccessAction = {
  type: typeof BOOK_ADDED_SUCCESS;
  payload: Book;
}

// Async
export const BOOK_FETCH_ASYNC = 'BOOK_FETCH_ASYNC';
type BookFetchAsyncAction = {
  type: typeof BOOK_FETCH_ASYNC;
};

export const BOOK_UPDATE_ASYNC = 'BOOK_UPDATE_ASYNC';
export type BookUpdateAsyncAction = {
  type: typeof BOOK_UPDATE_ASYNC;
  payload: BookToUpdate;
};

export const BOOK_ADD_ASYNC = 'BOOK_ADD_ASYNC';
export type BookAddAsyncAction = {
  type: typeof BOOK_ADD_ASYNC;
  payload: BookToAdd;
}

export type BooksActionTypes =
  | BookStartAction
  | BookFinishAction
  | BookFillAction
  | BookErrorAction
  | BookUpdateSuccessAction
  | BookFetchAsyncAction
  | BookUpdateAsyncAction
  | BookAddedSuccessAction
  | BookAddAsyncAction;

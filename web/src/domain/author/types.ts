export type Author = {
  id: string;
  name: string;
};

export type AuthorsFetch = {
  data: Authors;
  isFetching: boolean;
}

export type Authors = {
  authors: Author[];
  error: string[];
  success: boolean;
}

// Sync
export const AUTHOR_START = 'AUTHOR_START';
type AuthorStartAction = {
  type: typeof AUTHOR_START;
};

export const AUTHOR_FINISH = 'AUTHOR_FINISH';
type AuthorFinishAction = {
  type: typeof AUTHOR_FINISH;
};

export const AUTHOR_FILL = 'AUTHOR_FILL';
export type AuthorFillAction = {
  type: typeof AUTHOR_FILL;
  payload: Authors;
};

export const AUTHOR_ERROR = 'AUTHOR_ERROR';
export type AuthorErrorAction = {
  type: typeof AUTHOR_ERROR;
  payload: string[];
};

// Async
export const AUTHOR_FETCH_ASYNC = 'AUTHOR_FETCH_ASYNC';
type AuthorFetchAsyncAction = {
  type: typeof AUTHOR_FETCH_ASYNC;
};

export type AuthorsActionTypes =
  | AuthorStartAction
  | AuthorFinishAction
  | AuthorFillAction
  | AuthorErrorAction
  | AuthorFetchAsyncAction;

export type Genre = {
  id: string;
  title: string;
};

export type GenreFetch = {
  isFetching: boolean;
  data: Genres;
}

export type Genres = {
  data: Genre[];
  error: string[];
  success: boolean;
}

export const GENRE_START = 'GENRE_START';
type StartGenreAction = {
  type: typeof GENRE_START;
}

export const GENRE_FINISH = 'GENRE_FINISH';
type FinishGenreAction = {
  type: typeof GENRE_FINISH;
}

export const GENRE_FILL = 'GENRE_FILL';
export type FillGenreAction = {
  type: typeof GENRE_FILL;
  payload: Genres;
}

export const GENRE_ERROR = 'GENRE_ERROR';
export type ErrorGenreAction = {
  type: typeof GENRE_ERROR;
  payload: string[];
}

export const GENRE_FETCH_ASYNC = 'GENRE_FETCH_ASYNC';
type FetchGenreAsyncAction = {
  type: typeof GENRE_FETCH_ASYNC;
}

export type FetchGenreActions =
  | StartGenreAction
  | FinishGenreAction
  | FillGenreAction
  | ErrorGenreAction
  | FetchGenreAsyncAction;

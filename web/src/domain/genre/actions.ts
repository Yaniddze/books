import {
  GENRE_FILL,
  GENRE_FETCH_ASYNC,
  GENRE_FINISH,
  GENRE_START,
  GENRE_ERROR,
  FetchGenreActions,
  FillGenreAction,
  ErrorGenreAction,
  Genres,
} from './types';

export function start(): FetchGenreActions {
  return {
    type: GENRE_START,
  };
}

export function finish(): FetchGenreActions {
  return {
    type: GENRE_FINISH,
  };
}

export function fill(payload: Genres): FillGenreAction {
  return {
    type: GENRE_FILL,
    payload,
  };
}

export function error(payload: string[]): ErrorGenreAction {
  return {
    type: GENRE_ERROR,
    payload,
  };
}

export function fetchAsync(): FetchGenreActions {
  return {
    type: GENRE_FETCH_ASYNC,
  };
}

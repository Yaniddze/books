// Types
import {
  AUTHOR_ERROR,
  AUTHOR_FETCH_ASYNC,
  AUTHOR_FILL,
  AUTHOR_FINISH,
  AUTHOR_START,
  AuthorsActionTypes,
  Authors,
  AuthorFillAction,
  AuthorErrorAction,
} from './types';

// Sync
export function start(): AuthorsActionTypes {
  return {
    type: AUTHOR_START,
  };
}

export function finish(): AuthorsActionTypes {
  return {
    type: AUTHOR_FINISH,
  };
}

export function fill(payload: Authors): AuthorFillAction {
  return {
    type: AUTHOR_FILL,
    payload,
  };
}

export function error(payload: string[]): AuthorErrorAction {
  return {
    type: AUTHOR_ERROR,
    payload,
  };
}

// Async
export function fetchAsync(): AuthorsActionTypes {
  return {
    type: AUTHOR_FETCH_ASYNC,
  };
}

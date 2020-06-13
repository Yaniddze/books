// Core
import { all, fork } from 'redux-saga/effects';

// Watchers
import { watchBooks } from '../domain/book/saga';
import { watchAuthors } from '../domain/author/saga';
import { watchGenres } from '../domain/genre/saga';

export function* rootSaga(): Generator {
  yield all([
    fork(watchBooks),
    fork(watchAuthors),
    fork(watchGenres),
  ]);
}

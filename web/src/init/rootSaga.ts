// Core
import { all, fork } from 'redux-saga/effects';

// Watchers
import { watchBooks } from '../domain/book/saga';

export function* rootSaga(): Generator {
  yield all([fork(watchBooks)]);
}

// Core
import { SagaIterator } from '@redux-saga/core';
import { takeEvery, all, call } from 'redux-saga/effects';

// Types
import { BOOK_FETCH_ASYNC } from '../types';

// Workers
import { fetchBooks } from './workers';

function* watchFetchBooks(): SagaIterator {
  yield takeEvery(BOOK_FETCH_ASYNC, fetchBooks);
}

export function* watchBooks(): Generator {
  yield all([call(watchFetchBooks)]);
}

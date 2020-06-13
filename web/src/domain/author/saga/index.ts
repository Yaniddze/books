// Core
import { SagaIterator } from '@redux-saga/core';
import { takeEvery, all, call } from 'redux-saga/effects';

// Types
import { AUTHOR_FETCH_ASYNC } from '../types';

// Workers
import { fetchAuthors } from './workers';

function* watchFetchAuthors(): SagaIterator {
  yield takeEvery(AUTHOR_FETCH_ASYNC, fetchAuthors);
}

export function* watchAuthors(): Generator {
  yield all([call(watchFetchAuthors)]);
}

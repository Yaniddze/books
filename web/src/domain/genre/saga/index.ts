// Core
import { SagaIterator } from '@redux-saga/core';
import { takeEvery, call, all } from 'redux-saga/effects';

// Types
import { GENRE_FETCH_ASYNC } from '../types';

// Workers
import { fetchGenres } from './workers';

function* watchGenresFetch(): SagaIterator {
  yield takeEvery(GENRE_FETCH_ASYNC, fetchGenres);
}

export function* watchGenres(): Generator {
  yield all([call(watchGenresFetch)]);
}

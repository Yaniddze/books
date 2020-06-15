// Core
import { SagaIterator } from '@redux-saga/core';
import { takeEvery, all, call } from 'redux-saga/effects';

// Types
import {
  LOGIN_ASYNC,
} from '../types';

// Workers
import {
  login,
} from './workers';

function* watchFetchLogin(): SagaIterator {
  yield takeEvery(LOGIN_ASYNC, login);
}

export function* watchLogin(): Generator {
  yield all([
    call(watchFetchLogin),
  ]);
}

// Core
import { SagaIterator } from '@redux-saga/core';
import { takeEvery, all, call } from 'redux-saga/effects';

// Types
import {
  LOGIN_ASYNC,
  LOGOUT,
} from '../types';

// Workers
import {
  login,
  logout,
} from './workers';

function* watchFetchLogin(): SagaIterator {
  yield takeEvery(LOGIN_ASYNC, login);
}

function* watchLogout(): SagaIterator {
  yield takeEvery(LOGOUT, logout);
}

export function* watchLogin(): Generator {
  yield all([
    call(watchFetchLogin),
    call(watchLogout),
  ]);
}

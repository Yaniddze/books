import { SagaIterator } from '@redux-saga/core';
import { takeEvery, all, call } from 'redux-saga/effects';

import {
  REGISTER_ASYNC,
} from '../types';

import {
  register,
} from './workers';

function* watchFetchRegister(): SagaIterator {
  yield takeEvery(REGISTER_ASYNC, register);
}

export function* watchRegister(): Generator {
  yield all([
    call(watchFetchRegister),
  ]);
}

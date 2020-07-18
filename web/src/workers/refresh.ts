// Tools
import { call } from '@redux-saga/core/effects';
import { SagaIterator } from '@redux-saga/core';
import { AbstractAnswer, api } from '../api';

export function* refreshToken(): SagaIterator {
  try {
    const result: AbstractAnswer = yield call(api.login.refresh);

    return result.success;
  } catch {
    return false;
  }
}

import { call, put } from 'redux-saga/effects';
import { SagaIterator } from '@redux-saga/core';

// API
import { api } from '../../../../api';

// Types
import {
  LoginAsyncAction,
  LoginInfo,
  LoginAnswer,
} from '../../types';
import {
  start,
  success,
  error,
  finish,
} from '../../actions';

function* makeRequest(loginInfo: LoginInfo): SagaIterator {
  try {
    yield put(start());

    const result: LoginAnswer = yield call(api.login.fetch, loginInfo);

    if (result.success) {
      yield put(success(result.data));
    } else {
      yield put(error(result.errors));
    }
  } catch {
    yield put(error(['Network error']));
  } finally {
    yield put(finish());
  }
}

export function* login(action: LoginAsyncAction): Generator {
  yield makeRequest(action.payload);
}

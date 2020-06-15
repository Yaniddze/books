import { SagaIterator } from '@redux-saga/core';
import { call, put } from 'redux-saga/effects';

import {
  start,
  success as registerSuccess,
  finish,
  error,
} from '../../actions';

import {
  success as loginSuccess,
} from '../../../login/actions';

import {
  RegisterAnswer,
  RegisterInfo,
  RegisterAsyncAction,
} from '../../types';

import {
  api,
} from '../../../../api';

function* makeRequest(info: RegisterInfo): SagaIterator {
  try {
    yield put(start());

    const result: RegisterAnswer = yield call(api.register.fetch, info);

    if (result.success) {
      yield put(registerSuccess()); // call register success
      yield put(loginSuccess(result.token)); // call login success to write token
    } else {
      yield put(error(result.errors));
    }
  } catch {
    yield put(error(['Network error']));
  } finally {
    yield put(finish());
  }
}

export function* register(action: RegisterAsyncAction): Generator {
  yield makeRequest(action.payload);
}

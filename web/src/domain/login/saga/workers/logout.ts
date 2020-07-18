import { call } from 'redux-saga/effects';
import { SagaIterator } from '@redux-saga/core';

// API
import { api } from '../../../../api';

function* makeRequest(): SagaIterator {
  try {
    yield call(api.login.logout);
  } catch {
    console.log('logout error');
  }
}

export function* logout(): Generator {
  yield makeRequest();
}

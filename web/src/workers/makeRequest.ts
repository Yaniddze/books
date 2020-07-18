// Core
import { ActionCreator, AnyAction } from 'redux';
import { SagaIterator } from '@redux-saga/core';
import { put, call } from 'redux-saga/effects';

// Common types
import { FillActionType, ErrorActionType } from '../types';
import { logout } from '../domain/login/actions';

type OptionsType<T> = {
  fetcher: (uri?: string) => Promise<T>;
  fetcherParam?: string;
  start: ActionCreator<AnyAction>;
  finish: ActionCreator<AnyAction>;
  fill: FillActionType<T>;
  error: ErrorActionType;
};

export function* makeRequest<T>(options: OptionsType<T>): SagaIterator {
  const {
    fetcher, start, fetcherParam, fill, finish, error,
  } = options;

  try {
    yield put(start());

    const result = yield call(fetcher, fetcherParam);

    yield put(fill(result));
  } catch (e) {
    yield put(logout());
    yield put(error([e.message]));
  } finally {
    yield put(finish());
  }
}

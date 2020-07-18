// Core
import { Action } from 'redux';
import { SagaIterator } from '@redux-saga/core';
import { put, call } from 'redux-saga/effects';

// Common types
import { logout } from '../domain/login/actions';

type OptionsType<TFetchOut, TParam> = {
  fetcher: (param: TParam) => Promise<TFetchOut>;
  fetcherParam: TParam;
  onSuccess: (result: TFetchOut) => Action;
};

export function* makeRequestSingle<TFetchOut, TParam>(
  { fetcher, fetcherParam, onSuccess }: OptionsType<TFetchOut, TParam>,
): SagaIterator {
  try {
    const result: TFetchOut = yield call(fetcher, fetcherParam);

    yield put(onSuccess(result));
  } catch (e) {
    yield put(logout());
  }
}

// Core
import { Action } from 'redux';
import { put, call } from 'redux-saga/effects';

// Common types
import { logout } from '../domain/login/actions';
import { refreshToken } from './refresh';

type OptionsType<TFetchOut, TParam> = {
  fetcher: (param: TParam) => Promise<TFetchOut>;
  fetcherParam: TParam;
  onSuccess: (result: TFetchOut) => Action;
};

export function* makeRequestSingle<TFetchOut, TParam>(
  { fetcher, fetcherParam, onSuccess }: OptionsType<TFetchOut, TParam>,
): Generator {
  let count = 1;
  while (count < 2) {
    try {
      const result = (yield call(fetcher, fetcherParam)) as TFetchOut;

      yield put(onSuccess(result));
    } catch (e) {
      const result = yield refreshToken();
      if (result) {
        count--;
      } else {
        yield put(logout());
      }
    } finally {
      count++;
    }
  }
}

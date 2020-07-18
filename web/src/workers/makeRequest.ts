// Core
import { ActionCreator, AnyAction } from 'redux';
import { put, call } from 'redux-saga/effects';

// Common types
import { FillActionType, ErrorActionType } from '../types';

import { refreshToken } from './refresh';
import { logout } from '../domain/login/actions';

type OptionsType<T> = {
  fetcher: (uri?: string) => Promise<T>;
  fetcherParam?: string;
  start: ActionCreator<AnyAction>;
  finish: ActionCreator<AnyAction>;
  fill: FillActionType<T>;
  error: ErrorActionType;
};

export function* makeRequest<T>({
  fetcher, start, fetcherParam, fill, finish, error,
}: OptionsType<T>): Generator {
  let count = 1;
  while (count < 2) {
    try {
      yield put(start());

      const result = (yield call(fetcher, fetcherParam)) as T;

      yield put(fill(result));
    } catch (e) {
      const result = yield refreshToken();
      if (result) {
        count--;
      } else {
        yield put(logout());
      }
      yield put(error([e.message]));
    } finally {
      yield put(finish());
      count++;
    }
  }
}

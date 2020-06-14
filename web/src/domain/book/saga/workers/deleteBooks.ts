import { call, put } from 'redux-saga/effects';
import { SagaIterator } from '@redux-saga/core';

// API
import { api } from '../../../../api';

// Types
import { BookDeleteState, BooksDeleteAsyncAction } from '../../types';
import { booksDeleteSuccess } from '../../actions';

function* makeRequest(ids: string[]): SagaIterator {
  try {
    const result: BookDeleteState = yield call(api.books.delete, ids);

    if (result.success) {
      yield put(booksDeleteSuccess(ids));
    }
  } catch {
    // TODO create handle
  }
}

export function* deleteBooks(action: BooksDeleteAsyncAction): Generator {
  yield makeRequest(action.payload);
}

// Tools
import { call, put } from 'redux-saga/effects';
import { SagaIterator } from '@redux-saga/core';
import {
  bookUpdateSuccess,
} from '../../actions';
import {
  BookUpdateState,
  BookUpdateAsyncAction,
  BookToUpdate,
} from '../../types';

// API
import { api } from '../../../../api';

export function* makeRequest(body: BookToUpdate): SagaIterator {
  try {
    const result: BookUpdateState = yield call(api.books.update, body);

    if (result.success) {
      yield put(bookUpdateSuccess({
        id: body.bookId,
        bookInfo: {
          title: body.newTitle,
          year: body.newYear,
        },
        author: {
          id: body.newAuthorId,
          name: body.newAuthorName,
        },
        genre: {
          id: body.newGenreId,
          title: body.newGenreTitle,
        },
      }));
    }
  } catch (e) {
    // TODO create handle
  }
}

export function* updateBook(action: BookUpdateAsyncAction): Generator {
  yield makeRequest(action.payload);
}

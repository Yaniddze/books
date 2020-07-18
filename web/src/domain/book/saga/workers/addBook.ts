// Core
import { call, put } from 'redux-saga/effects';
import { SagaIterator } from '@redux-saga/core';
import {
  bookAddSuccess,
} from '../../actions';

// API
import { api } from '../../../../api';
import { BookAddAsyncAction, BookAddState, BookToAdd } from '../../types';
import { logout } from '../../../login/actions';

export function* makeRequest(body: BookToAdd): SagaIterator {
  try {
    const result: BookAddState = yield call(api.books.add, body);

    if (result.success) {
      yield put(bookAddSuccess({
        id: result.data,
        bookInfo: {
          title: body.bookTitle,
          year: body.year,
        },
        author: {
          id: body.authorId,
          name: body.authorName,
        },
        genre: {
          id: body.genreId,
          title: body.genreTitle,
        },
      }));
    }
  } catch (e) {
    yield put(logout());
  }
}

export function* addBook(action: BookAddAsyncAction): Generator {
  yield makeRequest(action.payload);
}

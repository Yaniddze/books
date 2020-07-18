// Tools
import {
  bookUpdateSuccess,
} from '../../actions';
import {
  BookUpdateAsyncAction,
  BookToUpdate,
} from '../../types';

// API
import { api } from '../../../../api';

import { makeRequestSingle } from '../../../../workers';

export function* makeRequest(body: BookToUpdate): Generator {
  yield makeRequestSingle({
    fetcher: api.books.update,
    fetcherParam: body,
    onSuccess: () => bookUpdateSuccess({
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
    }),
  });
}

export function* updateBook(action: BookUpdateAsyncAction): Generator {
  yield makeRequest(action.payload);
}

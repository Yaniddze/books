// Core
import {
  bookAddSuccess,
} from '../../actions';

import { makeRequestSingle } from '../../../../workers';

// API
import { api } from '../../../../api';
import { BookAddAsyncAction, BookToAdd } from '../../types';

export function* makeRequest(body: BookToAdd): Generator {
  yield makeRequestSingle({
    fetcher: api.books.add,
    fetcherParam: body,
    onSuccess: (result) => bookAddSuccess({
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
    }),
  });
}

export function* addBook(action: BookAddAsyncAction): Generator {
  yield makeRequest(action.payload);
}

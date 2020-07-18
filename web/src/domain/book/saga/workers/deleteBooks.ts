// API
import { api } from '../../../../api';

import { makeRequestSingle } from '../../../../workers';

// Types
import { BooksDeleteAsyncAction } from '../../types';
import { booksDeleteSuccess } from '../../actions';

function* makeRequest(ids: string[]): Generator {
  yield makeRequestSingle({
    fetcher: api.books.delete,
    fetcherParam: ids,
    onSuccess: () => booksDeleteSuccess(ids),
  });
}

export function* deleteBooks(action: BooksDeleteAsyncAction): Generator {
  yield makeRequest(action.payload);
}

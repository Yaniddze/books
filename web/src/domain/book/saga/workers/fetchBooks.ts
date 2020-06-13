// Tools
import {
  start,
  finish,
  fill,
  error,
} from '../../actions';
import { Books } from '../../types';

// Workers
import { makeRequest } from '../../../../workers';

// API
import { api } from '../../../../api';

export function* fetchBooks(): Generator {
  const options = {
    fetcher: api.books.fetch,
    start,
    finish,
    fill,
    error,
  };

  yield makeRequest<Books>(options);
}

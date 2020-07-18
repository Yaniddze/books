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
  yield makeRequest<Books>({
    start,
    finish,
    fill,
    error,
    fetcher: api.books.fetch,
  });
}

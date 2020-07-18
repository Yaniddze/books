// Tools
import {
  start,
  finish,
  fill,
  error,
} from '../../actions';

import { Authors } from '../../types';

// Workers
import { makeRequest } from '../../../../workers';

// API
import { api } from '../../../../api';

export function* fetchAuthors(): Generator {
  yield yield makeRequest<Authors>({
    start,
    finish,
    error,
    fill,
    fetcher: api.authors.fetch,
  });
}

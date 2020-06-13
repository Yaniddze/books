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
  const options = {
    fetcher: api.authors.fetch,
    start,
    finish,
    fill,
    error,
  };

  yield makeRequest<Authors>(options);
}

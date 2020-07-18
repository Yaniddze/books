// Tools
import {
  start,
  finish,
  error,
  fill,
} from '../../actions';

import {
  Genres,
} from '../../types';

// Workers
import { makeRequest } from '../../../../workers';

// API
import { api } from '../../../../api';

export function* fetchGenres(): Generator {
  yield makeRequest<Genres>({
    start,
    fill,
    finish,
    error,
    fetcher: api.genres.fetch,
  });
}

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
  const options = {
    fetcher: api.genres.fetch,
    start,
    finish,
    fill,
    error,
  };

  yield makeRequest<Genres>(options);
}

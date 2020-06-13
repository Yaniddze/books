import axios from 'axios';
import { root } from './config';
import { Books } from '../domain/book/types';
import { Authors } from '../domain/author/types';

export type FetchDataType<T> = () => Promise<T>;

type APIFetchDataType = {
  books: {
    fetch: FetchDataType<Books>;
  };
  authors: {
    fetch: FetchDataType<Authors>;
  };
}

export const api: APIFetchDataType = {
  books: {
    fetch: (): Promise<Books> => axios.get(`${root}/book/all`)
      .then((result) => result.data as Books),
  },
  authors: {
    fetch: (): Promise<Authors> => axios.get(`${root}/author/all`)
      .then((result) => result.data as Authors),
  },
};

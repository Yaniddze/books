import axios from 'axios';
import { root } from './config';
import { Books } from '../domain/book/types';

export type FetchDataType<T> = () => Promise<T>;

type APIFetchDataType = {
  books: {
    fetch: FetchDataType<Books>;
  };
}

export const api: APIFetchDataType = {
  books: {
    fetch: (): Promise<Books> => axios.get(`${root}/book/all`)
      .then((result) => result.data as Books),
  },
};

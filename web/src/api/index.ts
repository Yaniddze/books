import axios from 'axios';
import { root } from './config';
import {
  Books, BookUpdateState, BookToUpdate, BookToAdd, BookAddState,
} from '../domain/book/types';
import { Authors } from '../domain/author/types';
import { Genres } from '../domain/genre/types';

export type FetchDataType<T> = () => Promise<T>;

type APIFetchDataType = {
  books: {
    fetch: FetchDataType<Books>;
    update: (book: BookToUpdate) => Promise<BookUpdateState>;
    add: (book: BookToAdd) => Promise<BookAddState>;
  };
  authors: {
    fetch: FetchDataType<Authors>;
  };
  genres: {
    fetch: FetchDataType<Genres>;
  };
}

export const api: APIFetchDataType = {
  books: {
    fetch: (): Promise<Books> => axios.get(`${root}/book/all`)
      .then((result) => result.data as Books),
    update: (book: BookToUpdate): Promise<BookUpdateState> => axios.patch(`${root}/book/update`, book)
      .then((result) => result.data as BookUpdateState),
    add: (book: BookToAdd): Promise<BookAddState> => axios.put(`${root}/book/add`, book)
      .then((result) => result.data as BookAddState),
  },
  authors: {
    fetch: (): Promise<Authors> => axios.get(`${root}/author/all`)
      .then((result) => result.data as Authors),
  },
  genres: {
    fetch: (): Promise<Genres> => axios.get(`${root}/genre/all`)
      .then((result) => result.data as Genres),
  },
};

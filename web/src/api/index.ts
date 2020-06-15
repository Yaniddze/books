import axios from 'axios';
import { root } from './config';
import {
  Books, BookUpdateState, BookToUpdate, BookToAdd, BookAddState, BookDeleteState,
} from '../domain/book/types';
import { Authors } from '../domain/author/types';
import { Genres } from '../domain/genre/types';
import { LoginAnswer, LoginInfo } from '../domain/login/types';
import { RegisterAnswer, RegisterInfo } from '../domain/register/types';

export type FetchDataType<T> = () => Promise<T>;

type APIFetchDataType = {
  books: {
    fetch: FetchDataType<Books>;
    update: (book: BookToUpdate) => Promise<BookUpdateState>;
    add: (book: BookToAdd) => Promise<BookAddState>;
    delete: (ids: string[]) => Promise<BookDeleteState>;
  };
  authors: {
    fetch: FetchDataType<Authors>;
  };
  genres: {
    fetch: FetchDataType<Genres>;
  };
  login: {
    fetch: (info: LoginInfo) => Promise<LoginAnswer>;
  };
  register: {
    fetch: (info: RegisterInfo) => Promise<RegisterAnswer>;
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
    delete: (ids: string[]): Promise<BookDeleteState> => axios.delete(`${root}/book/delete`,
      { data: { bookIds: ids } })
      .then((result) => result.data as BookDeleteState),
  },
  authors: {
    fetch: (): Promise<Authors> => axios.get(`${root}/author/all`)
      .then((result) => result.data as Authors),
  },
  genres: {
    fetch: (): Promise<Genres> => axios.get(`${root}/genre/all`)
      .then((result) => result.data as Genres),
  },
  login: {
    fetch: (info: LoginInfo): Promise<LoginAnswer> => axios.post(`${root}/identity/login`, info)
      .then((result) => result.data as LoginAnswer),
  },
  register: {
    fetch: (info: RegisterInfo): Promise<RegisterAnswer> => axios.post(`${root}/identity/register`, info)
      .then((result) => result.data as RegisterAnswer),
  },
};

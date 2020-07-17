import axios, { AxiosRequestConfig, AxiosResponse } from 'axios';
import Cookies from 'js-cookie';
import { root } from './config';
import {
  Books, BookUpdateState, BookToUpdate, BookToAdd, BookAddState, BookDeleteState,
} from '../domain/book/types';
import { Authors } from '../domain/author/types';
import { Genres } from '../domain/genre/types';
import { LoginAnswer, LoginInfo } from '../domain/login/types';
import { RegisterAnswer, RegisterInfo } from '../domain/register/types';

export type FetchDataType<T> = () => Promise<T>;

function generateConfig(): AxiosRequestConfig {
  return {
    headers: {
      Authorization: `bearer ${Cookies.get('token')}`,
    },
  };
}

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
    fetch: (): Promise<Books> => axios.get(`${root}/book/all`, generateConfig())
      .then((result: AxiosResponse<Books>) => result.data),

    update: (book: BookToUpdate): Promise<BookUpdateState> => axios.patch(`${root}/book/update`, book, generateConfig())
      .then((result: AxiosResponse<BookUpdateState>) => result.data),

    add: (book: BookToAdd): Promise<BookAddState> => axios.put(`${root}/book/add`, book, generateConfig())
      .then((result: AxiosResponse<BookAddState>) => result.data),

    delete: (ids: string[]): Promise<BookDeleteState> => axios.delete(`${root}/book/delete`,
      { data: { bookIds: ids }, ...generateConfig() })
      .then((result: AxiosResponse<BookDeleteState>) => result.data),
  },
  authors: {
    fetch: (): Promise<Authors> => axios.get(`${root}/author/all`, generateConfig())
      .then((result: AxiosResponse<Authors>) => result.data),
  },
  genres: {
    fetch: (): Promise<Genres> => axios.get(`${root}/genre/all`, generateConfig())
      .then((result: AxiosResponse<Genres>) => result.data),
  },
  login: {
    fetch: (info: LoginInfo): Promise<LoginAnswer> => axios.post(`${root}/identity/login`, info)
      .then((result: AxiosResponse<LoginAnswer>) => result.data),
  },
  register: {
    fetch: (info: RegisterInfo): Promise<RegisterAnswer> => axios.post(`${root}/identity/register`, info)
      .then((result: AxiosResponse<RegisterAnswer>) => result.data),
  },
};

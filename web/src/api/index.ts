import axios, { AxiosRequestConfig, AxiosResponse } from 'axios';
import { root } from './config';
import {
  Books, BookUpdateState, BookToUpdate, BookToAdd, BookAddState, BookDeleteState,
} from '../domain/book/types';
import { Authors } from '../domain/author/types';
import { Genres } from '../domain/genre/types';
import { LoginAnswer, LoginInfo } from '../domain/login/types';
import { RegisterAnswer, RegisterInfo } from '../domain/register/types';

export type FetchDataType<T> = () => Promise<T>;

export type AbstractAnswer = {
  success: boolean;
  errors: string[];
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
    logout: () => void;
    refresh: () => Promise<AbstractAnswer>;
  };
  register: {
    fetch: (info: RegisterInfo) => Promise<RegisterAnswer>;
  };
}

const requestInterceptor = (request: AxiosRequestConfig): AxiosRequestConfig => {
  request.withCredentials = true;
  return request;
};

const client = axios.create({ baseURL: root });
client.interceptors.request.use((request) => requestInterceptor(request));

export const api: APIFetchDataType = {
  books: {
    fetch: (): Promise<Books> => client.get('/book/all')
      .then((result: AxiosResponse<Books>) => result.data),

    update: (book: BookToUpdate): Promise<BookUpdateState> => client.patch('/book/update', book)
      .then((result: AxiosResponse<BookUpdateState>) => result.data),

    add: (book: BookToAdd): Promise<BookAddState> => client.put('/book/add', book)
      .then((result: AxiosResponse<BookAddState>) => result.data),

    delete: (ids: string[]): Promise<BookDeleteState> => client.delete('/book/delete',
      { data: { bookIds: ids } })
      .then((result: AxiosResponse<BookDeleteState>) => result.data),
  },
  authors: {
    fetch: (): Promise<Authors> => client.get('/author/all')
      .then((result: AxiosResponse<Authors>) => result.data),
  },
  genres: {
    fetch: (): Promise<Genres> => client.get('/genre/all')
      .then((result: AxiosResponse<Genres>) => result.data),
  },
  login: {
    fetch: (info: LoginInfo): Promise<LoginAnswer> => client.post('/identity/login', info)
      .then((result: AxiosResponse<LoginAnswer>) => result.data),
    logout: (): void => {
      client.post('/identity/logout');
    },
    refresh: (): Promise<AbstractAnswer> => client.post('/identity/refresh')
      .then((result: AxiosResponse<AbstractAnswer>) => result.data),
  },
  register: {
    fetch: (info: RegisterInfo): Promise<RegisterAnswer> => client.post('/identity/register', info)
      .then((result: AxiosResponse<RegisterAnswer>) => result.data),
  },
};

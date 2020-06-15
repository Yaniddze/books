// Core
import { combineReducers } from 'redux';

// Reducers
import { booksReducer } from '../domain/book/reducer';
import { authorReducer } from '../domain/author/reducer';
import { genreReducer } from '../domain/genre/reducer';
import { loginReducer } from '../domain/login/reducer';

export const rootReducer = combineReducers({
  booksReducer,
  authorReducer,
  genreReducer,
  loginReducer,
});

export type AppState = ReturnType<typeof rootReducer>;

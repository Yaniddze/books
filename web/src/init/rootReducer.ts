// Core
import { combineReducers } from 'redux';

// Reducers
import { booksReducer } from '../domain/book/reducer';
import { authorReducer } from '../domain/author/reducer';
import { genreReducer } from '../domain/genre/reducer';
import { loginReducer } from '../domain/login/reducer';
import { registerReducer } from '../domain/register/reducer';

export const rootReducer = combineReducers({
  booksReducer,
  authorReducer,
  genreReducer,
  loginReducer,
  registerReducer,
});

export type AppState = ReturnType<typeof rootReducer>;

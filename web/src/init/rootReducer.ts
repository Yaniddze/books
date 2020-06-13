// Core
import { combineReducers } from 'redux';

// Reducers
import { booksReducer } from '../domain/book/reducer';
import { authorReducer } from '../domain/author/reducer';
import { genreReducer } from '../domain/genre/reducer';

export const rootReducer = combineReducers({
  booksReducer,
  authorReducer,
  genreReducer,
});

export type AppState = ReturnType<typeof rootReducer>;

// Core
import { combineReducers } from 'redux';

// Reducers
import { booksReducer } from '../domain/book/reducer';
import { authorReducer } from '../domain/author/reducer';

export const rootReducer = combineReducers({
  booksReducer,
  authorReducer,
});

export type AppState = ReturnType<typeof rootReducer>;

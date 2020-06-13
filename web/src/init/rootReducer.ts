// Core
import { combineReducers } from 'redux';

// Reducers
import { booksReducer } from '../domain/book/reducer';

export const rootReducer = combineReducers({
  booksReducer,
});

export type AppState = ReturnType<typeof rootReducer>;

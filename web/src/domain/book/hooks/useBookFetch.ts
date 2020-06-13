// Core
import { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { BookState } from '../types';

import { fetchAsync } from '../actions';
import { AppState } from '../../../init/rootReducer';

export const useBookFetch = (): BookState => {
  const dispatch = useDispatch();
  const { data, isFetching } = useSelector<AppState, BookState>(
    (state) => state.booksReducer,
  );

  useEffect(() => {
    dispatch(fetchAsync());
  }, [dispatch]);

  return {
    data,
    isFetching,
  };
};

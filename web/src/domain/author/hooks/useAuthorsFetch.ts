// Core
import { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { AuthorsFetch } from '../types';

import { fetchAsync } from '../actions';
import { AppState } from '../../../init/rootReducer';

export const useAuthorsFetch = (): AuthorsFetch => {
  const dispatch = useDispatch();
  const { data, isFetching } = useSelector<AppState, AuthorsFetch>(
    (state) => state.authorReducer,
  );

  useEffect(() => {
    dispatch(fetchAsync());
  }, [dispatch]);

  return {
    data,
    isFetching,
  };
};

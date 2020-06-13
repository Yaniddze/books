// Core
import { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { AppState } from '../../../init/rootReducer';

import { GenreFetch } from '../types';
import { fetchAsync } from '../actions';

export function useGenresFetch(): GenreFetch {
  const dispatch = useDispatch();
  const reducerValue = useSelector<AppState, GenreFetch>(
    (state) => state.genreReducer,
  );

  useEffect(() => {
    dispatch(fetchAsync());
  }, [dispatch]);

  return reducerValue;
}

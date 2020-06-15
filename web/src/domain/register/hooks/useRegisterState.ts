import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { RegisterState } from '../types';
import { AppState } from '../../../init/rootReducer';
import { error } from '../actions';

export const useRegisterState = (): RegisterState => {
  const reducerState = useSelector<AppState, RegisterState>(
    (state) => state.registerReducer,
  );
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(error([]));
  }, [dispatch]);

  return reducerState;
};

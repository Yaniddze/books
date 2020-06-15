import { useSelector } from 'react-redux';
import { RegisterState } from '../types';
import { AppState } from '../../../init/rootReducer';

export const useRegisterState = (): RegisterState => {
  const reducerState = useSelector<AppState, RegisterState>(
    (state) => state.registerReducer,
  );

  return reducerState;
};

// Core
import { useSelector } from 'react-redux';
import { LoginState } from '../types';
import { AppState } from '../../../init/rootReducer';

export const useLoginState = (): LoginState => {
  const store = useSelector<AppState, LoginState>(
    (state) => state.loginReducer,
  );

  return store;
};

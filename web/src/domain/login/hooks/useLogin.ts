// Core
import { useDispatch, useSelector } from 'react-redux';
import { LoginInfo, LoginState } from '../types';
import { loginAsync } from '../actions';
import { AppState } from '../../../init/rootReducer';

type ReturnedType = {
  state: LoginState;
  fetch: (info: LoginInfo) => void;
}

export const useLogin = (): ReturnedType => {
  const store = useSelector<AppState, LoginState>(
    (state) => state.loginReducer,
  );

  const dispatch = useDispatch();
  return {
    fetch: (info: LoginInfo): void => {
      dispatch(loginAsync(info));
    },
    state: store,
  };
};

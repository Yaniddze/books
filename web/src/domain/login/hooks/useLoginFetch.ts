// Core
import { useDispatch } from 'react-redux';
import { LoginInfo, LoginState } from '../types';
import { loginAsync } from '../actions';

type ReturnedType = {
  state: LoginState;
  fetch: (info: LoginInfo) => void;
}

export const useLoginFetch = (): (info: LoginInfo) => void => {
  const dispatch = useDispatch();
  return (info: LoginInfo): void => {
    dispatch(loginAsync(info));
  };
};

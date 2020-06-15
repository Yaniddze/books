// Core
import { useDispatch } from 'react-redux';
import { LoginInfo } from '../types';
import { loginAsync } from '../actions';

export const useLoginFetch = (): (info: LoginInfo) => void => {
  const dispatch = useDispatch();
  return (info: LoginInfo): void => {
    dispatch(loginAsync(info));
  };
};

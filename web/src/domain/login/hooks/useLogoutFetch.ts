import { useDispatch } from 'react-redux';

import {
  logout,
} from '../actions';

export const useLogoutFetch = (): () => void => {
  const dispatch = useDispatch();

  return (): void => { dispatch(logout()); };
};

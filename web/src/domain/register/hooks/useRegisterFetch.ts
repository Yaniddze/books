import { useDispatch } from 'react-redux';
import { RegisterInfo } from '../types';
import { registerAsync } from '../actions';

export const useRegisterFetch = (): (info: RegisterInfo) => void => {
  const dispatch = useDispatch();

  return (info: RegisterInfo): void => { dispatch(registerAsync(info)); };
};

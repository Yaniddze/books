// Core
import { useDispatch } from 'react-redux';
import { clean } from '../actions';

export const useLoginClean = (): () => void => {
  const dispatch = useDispatch();
  return (): void => {
    dispatch(clean());
  };
};

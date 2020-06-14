// Core
import { useDispatch } from 'react-redux';
import { deleteAsync } from '../actions';

export const useBooksDelete = (): (payload: string[]) => void => {
  const dispatch = useDispatch();

  return (payload: string[]): void => {
    dispatch(deleteAsync(payload));
  };
};

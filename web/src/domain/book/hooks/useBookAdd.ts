// Core
import { useDispatch } from 'react-redux';

// Actions
import { addAsync } from '../actions';
import { BookToAdd } from '../types';

export const useBookAdd = (): (bookToAdd: BookToAdd) => void => {
  const dispatch = useDispatch();

  return (bookToAdd: BookToAdd): void => {
    dispatch(addAsync(bookToAdd));
  };
};

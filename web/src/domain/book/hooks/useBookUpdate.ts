// Core
import { useDispatch } from 'react-redux';

import { updateAsync } from '../actions';
import { BookToUpdate } from '../types';

type ReturnType = {
  dispatch: (book: BookToUpdate) => void;
}

export const useBookUpdate = (): ReturnType => {
  const dispatch = useDispatch();
  return {
    dispatch: (book): void => {
      dispatch(updateAsync(book));
    },
  };
};

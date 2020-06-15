import { useDispatch } from 'react-redux';
import { clean } from '../actions';

export const useRegisterClean = (): () => void => {
  const dispatch = useDispatch();

  return (): void => { dispatch(clean()); };
};

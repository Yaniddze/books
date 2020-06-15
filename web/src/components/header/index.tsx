// Core
import React, { FC } from 'react';

// Styles
import styles from './styles.module.scss';

import { useLoginState } from '../../domain/login/hooks/useLoginState';
import { useLogoutFetch } from '../../domain/login/hooks/useLogoutFetch';

type PropTypes = {
  children: string;
}

export const Header: FC<PropTypes> = ({ children }: PropTypes) => {
  const loginState = useLoginState();
  const logoutFetch = useLogoutFetch();
  const logoutBtn = loginState.data.token !== '' && (
    <button
      className={styles.logoutBtn}
      type="submit"
      onClick={(): void => {
        logoutFetch();
      }}
    >
      Выйти
    </button>
  );

  return (
    <div className={styles.header}>
      <span>
        {children}
      </span>
      {logoutBtn}
    </div>
  );
};

// Core
import React, { FC } from 'react';

// Styles
import styles from './styles.module.scss';

type PropTypes = {
  children: string;
}

export const Header: FC<PropTypes> = ({ children }: PropTypes) => (
  <div className={styles.header}>
    <span>
      {children}
    </span>
  </div>
);

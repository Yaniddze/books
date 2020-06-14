// Core
import React, { FC } from 'react';

// Styles
import styles from './styles.module.scss';

type PropTypes = {
  children: string;
  onClick: () => void;
}

export const Button: FC<PropTypes> = ({ children, onClick }: PropTypes) => (
  <button className={styles.myBtn} type="submit" onClick={onClick}>
    {children}
  </button>
);

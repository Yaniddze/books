// Core
import React, { FC } from 'react';

// Styles
import styles from './styles.module.scss';

type PropTypes = {
  children?: never;
  onClick: () => void;
}

export const RoundButton: FC<PropTypes> = ({ onClick }: PropTypes) => (
  <button className={styles.myRoundBtn} type="submit" onClick={onClick}>
    &#43;
  </button>
);

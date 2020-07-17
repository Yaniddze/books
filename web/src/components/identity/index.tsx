import React, { FC, ReactElement } from 'react';
import { Redirect } from 'react-router-dom';

import { useLoginState } from '../../domain/login/hooks/useLoginState';

type PropTypes = {
  children: ReactElement;
  redirectOnEmpty: boolean;
  to: string;
}

export const TokenIdentity: FC<PropTypes> = ({ children, to, redirectOnEmpty }: PropTypes) => {
  const login = useLoginState();

  const redirect = (login.data.data !== '') === redirectOnEmpty && <Redirect to={to} />;

  return (
    <div>
      {redirect}
      {children}
    </div>
  );
};

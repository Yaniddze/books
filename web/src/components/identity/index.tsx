import React, { FC, ReactElement } from 'react';
import { Redirect } from 'react-router-dom';

import { useLogin } from '../../domain/login/hooks/useLogin';

type PropTypes = {
  children: ReactElement;
  redirectOnEmpty: boolean;
  to: string;
}

export const TokenIdentity: FC<PropTypes> = ({ children, to, redirectOnEmpty }: PropTypes) => {
  const login = useLogin();

  const redirect = (login.state.data.token !== '') === redirectOnEmpty && <Redirect to={to} />;

  return (
    <div>
      {redirect}
      {children}
    </div>
  );
};

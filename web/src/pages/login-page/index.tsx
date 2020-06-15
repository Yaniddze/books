import React, { FC, useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { useLogin } from '../../domain/login/hooks/useLogin';
import { TokenIdentity } from '../../components/identity';

type PropTypes = {
  children?: never;
}

export const LoginPage: FC<PropTypes> = () => {
  const { handleSubmit, register, errors } = useForm();

  const login = useLogin();

  useEffect(() => {
    document.title = 'Login';
  }, []);

  const loginErrors = !login.state.isFetching && !login.state.data.success
    && login.state.data.errors.map((error) => <div key={error}>{error}</div>);

  const loginBtn = !login.state.isFetching && (
    <div>
      <button type="submit">
        Войти
      </button>
    </div>
  );

  return (
    <TokenIdentity redirectOnEmpty to="books">
      <div style={{
        border: '1px solid black',
        margin: '10px',
        position: 'absolute',
        top: '1',
        bottom: '1',
        right: '1',
        left: '1',
        padding: '10px',
      }}
      >
        <h1>
          Аутентификация
        </h1>
        <form
          onSubmit={handleSubmit((values: Record<string, string>) => {
            login.fetch({ login: values.login, password: values.password });
          })}
        >
          <div>Login</div>
          {loginErrors}
          <div>
            {errors.login && errors.login.message}
          </div>
          <input
            name="login"
            ref={register({
              required: true,
              minLength: 4,
              maxLength: 50,
            })}
          />

          <div>Password</div>
          <div>
            {errors.password && errors.password.message}
          </div>
          <input
            name="password"
            type="password"
            ref={register({
              required: true,
              minLength: 4,
              maxLength: 30,
            })}
          />

          {loginBtn}
        </form>
      </div>
    </TokenIdentity>
  );
};

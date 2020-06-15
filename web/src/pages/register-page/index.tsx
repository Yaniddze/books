import React, { FC, useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { useRegisterState } from '../../domain/register/hooks/useRegisterState';
import { useRegisterFetch } from '../../domain/register/hooks/useRegisterFetch';
import { TokenIdentity } from '../../components/identity';

type PropTypes = {
  children?: never;
}

export const RegisterPage: FC<PropTypes> = () => {
  const { handleSubmit, register, errors } = useForm();

  const registerState = useRegisterState();
  const registerFetch = useRegisterFetch();

  useEffect(() => {
    document.title = 'Register';
  }, []);

  const registerErrors = !registerState.isFetching && !registerState.data.success
    && registerState.data.errors.map((error) => <div key={error}>{error}</div>);

  const loginBtn = !registerState.isFetching && (
    <div>
      <button type="submit">
        Регистрация
      </button>
    </div>
  );

  return (
    <TokenIdentity redirectOnEmpty to="/books">
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
          Регистрация
        </h1>
        <form
          onSubmit={handleSubmit((values: Record<string, string>) => {
            registerFetch({ login: values.login, password: values.password });
          })}
        >
          <div>Login</div>
          {registerErrors}
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

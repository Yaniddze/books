// Core
import React, { FC, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { useForm } from 'react-hook-form';

// Hooks
import { useLoginState } from '../../domain/login/hooks/useLoginState';
import { useLoginFetch } from '../../domain/login/hooks/useLoginFetch';
import { useLoginClean } from '../../domain/login/hooks/useLoginClean';

// Components
import { TokenIdentity } from '../../components/identity';

// Styles
import styles from './styles.module.scss';

type PropTypes = {
  children?: never;
}

export const LoginPage: FC<PropTypes> = () => {
  const { handleSubmit, register, errors } = useForm();

  const loginState = useLoginState();
  const loginFetch = useLoginFetch();
  const loginClean = useLoginClean();

  useEffect(() => {
    document.title = 'Login';
    loginClean();
  }, []);

  const loginErrors = !loginState.isFetching && !loginState.data.success
    && loginState.data.errors.map((error) => <div key={error}>{error}</div>);

  const loginBtn = !loginState.isFetching && (
    <div>
      <button className={styles.authButton} type="submit">
        Войти
      </button>
    </div>
  );

  return (
    <TokenIdentity redirectOnEmpty to="books">
      <div>
        <div className={styles.authForm}>
          <h1>
            Аутентификация
          </h1>
          <form
            onSubmit={handleSubmit((values: Record<string, string>) => {
              loginFetch({ login: values.login, password: values.password });
            })}
          >
            <div className={styles.error}>{loginErrors}</div>
            <div className={styles.authTitle}>Login</div>
            <div>
              {errors.login && errors.login.message}
            </div>
            <input
              name="login"
              className={styles.authInput}
              ref={register({
                required: true,
                minLength: 4,
                maxLength: 50,
              })}
            />

            <div className={styles.authTitle}>Password</div>
            <div>
              {errors.password && errors.password.message}
            </div>
            <input
              name="password"
              className={styles.authInput}
              type="password"
              ref={register({
                required: true,
                minLength: 4,
                maxLength: 30,
              })}
            />

            {loginBtn}
            <Link to="/register">
              Регистрация
            </Link>
          </form>
        </div>
      </div>
    </TokenIdentity>
  );
};

// Core
import React, { FC, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { useForm } from 'react-hook-form';

// Hooks
import { useRegisterState } from '../../domain/register/hooks/useRegisterState';
import { useRegisterFetch } from '../../domain/register/hooks/useRegisterFetch';
import { useRegisterClean } from '../../domain/register/hooks/useRegisterClean';

// Components
import { TokenIdentity } from '../../components/identity';

// Styles
import styles from '../login-page/styles.module.scss';

type PropTypes = {
  children?: never;
}

export const RegisterPage: FC<PropTypes> = () => {
  const { handleSubmit, register, errors } = useForm();

  const registerState = useRegisterState();
  const registerFetch = useRegisterFetch();
  const registerClean = useRegisterClean();

  useEffect(() => {
    document.title = 'Register';
    registerClean();
  }, []);

  const registerErrors = !registerState.isFetching && !registerState.data.success
    && registerState.data.errors.map((error) => <div key={error}>{error}</div>);

  const registerBtn = !registerState.isFetching && (
    <div>
      <button className={styles.authButton} type="submit">
        Регистрация
      </button>
    </div>
  );

  return (
    <TokenIdentity redirectOnEmpty to="/books">
      <div>
        <div className={styles.authForm}>
          <h1>
            Регистрация
          </h1>
          <form
            onSubmit={handleSubmit((values: Record<string, string>) => {
              registerFetch({ login: values.login, password: values.password });
            })}
          >
            <div className={styles.error}>{registerErrors}</div>
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

            {registerBtn}
            <Link to="/login">
              Войти
            </Link>
          </form>
        </div>
      </div>
    </TokenIdentity>
  );
};

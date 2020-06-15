// Core
import React, { ReactElement } from 'react';
import { Provider } from 'react-redux';
import {
  BrowserRouter,
  Switch,
  Route,
  Redirect,
} from 'react-router-dom';

// Styles
import './styles.module.scss';

// Store
import { store } from './init/store';

// Pages
import { BookPage } from './pages/book-page';
import { LoginPage } from './pages/login-page';
import { RegisterPage } from './pages/register-page';

// Components
import { Header } from './components/header';

export function App(): ReactElement {
  return (
    <>
      <Provider store={store}>
        <Header>
          Библиотека
        </Header>

        <BrowserRouter>
          <Switch>
            <Route path="/books">
              <BookPage />
            </Route>
            <Route path="/login">
              <LoginPage />
            </Route>
            <Route path="/register">
              <RegisterPage />
            </Route>
            <Redirect to="/books" />
          </Switch>
        </BrowserRouter>

      </Provider>
    </>
  );
}

export default App;

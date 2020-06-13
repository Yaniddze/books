// Core
import React, { ReactElement } from 'react';
import { Provider } from 'react-redux';

// Styles
import './styles.module.scss';

// Store
import { store } from './init/store';

// Domain
import { Books } from './domain/book';
import { Authors } from './domain/author';
import { Genres } from './domain/genre';

// Components
import { Header } from './components/header';

export function App(): ReactElement {
  return (
    <>
      <Provider store={store}>
        <Header>
          Библиотека
        </Header>
        <Books />
        <Authors />
        <Genres />
      </Provider>
    </>
  );
}

export default App;

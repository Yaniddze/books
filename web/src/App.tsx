import React, { ReactElement } from 'react';
import { Provider } from 'react-redux';

import { store } from './init/store';

import { Books } from './domain/book';
import { Authors } from './domain/author';

export function App(): ReactElement {
  return (
    <>
      <Provider store={store}>
        <Books />
        <Authors />
      </Provider>
    </>
  );
}

export default App;

import React, { ReactElement } from 'react';
import { Provider } from 'react-redux';

import { store } from './init/store';

import { Books } from './domain/book';

export function App(): ReactElement {
  return (
    <>
      <Provider store={store}>
        <Books />
      </Provider>
    </>
  );
}

export default App;

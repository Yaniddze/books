// Core
import React, { FC } from 'react';

// Types
import { Author } from './types';

// Hooks
import { useAuthorsFetch } from './hooks/useAuthorsFetch';

export const Authors: FC = () => {
  const { data, isFetching } = useAuthorsFetch();

  let i = 1;

  const errors = !isFetching && !data.success && (
    <p>
      {data.error.map((error: string) => `${error} \n`)}
    </p>
  );
  const fetching = isFetching && <p> Loading data from API... </p>;
  const authors = !isFetching && data.success && data.data.map((author: Author) => (
    <li key={i++}>
      {author.name}
    </li>
  ));

  return (
    <>
      <h1>
        Авторы
      </h1>
      {errors}
      {fetching}
      <ul>
        {authors}
      </ul>
    </>
  );
};

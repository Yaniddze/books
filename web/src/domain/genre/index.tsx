// Core
import React, { FC } from 'react';
import { useGenresFetch } from './hooks/useGenresFetch';
import { Genre } from './types';

export const Genres: FC = () => {
  const { data, isFetching } = useGenresFetch();

  const errors = !isFetching && !data.success && (
    <p>
      { data.error.map((error: string) => `${error}\n`) }
    </p>
  );

  const fetching = isFetching && (
    <p>
      Load data from API...
    </p>
  );

  let i = 300;

  const genres = !isFetching && data.success && data.data.map(
    (genre: Genre) => (
      <li key={i++}>
        { genre.title }
      </li>
    ),
  );

  return (
    <>
      <h1>
        Жанры
      </h1>
      { errors }
      { fetching }
      <ul>
        { genres }
      </ul>
    </>
  );
};

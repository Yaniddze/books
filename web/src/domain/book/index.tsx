// Core
import React, { ReactElement, FC } from 'react';
import { useBookFetch } from './hooks/useBookFetch';
import { Book } from './types';

export const Books: FC = () => {
  const { isFetching, data } = useBookFetch();

  const errorMessageJSX = !data.success && <p>{data.errors.map((message) => message)}</p>;
  const loaderJSX = isFetching && <p>Loading data from API...</p>;
  const listJSX = isFetching
    || data.data?.map(
      (
        {
          bookInfo,
          author,
          genre,
          id,
        }: Book,
        index: number,
      ): ReactElement => (
        <ul key={index}>
          <li>{id}</li>
          <li>{bookInfo.title}</li>
          <li>{bookInfo.year}</li>
          <li>{author.id}</li>
          <li>{author.name}</li>
          <li>{genre.id}</li>
          <li>{genre.title}</li>
        </ul>
      ),
    );

  return (
    <>
      {errorMessageJSX}
      {loaderJSX}
      {listJSX}
    </>
  );
};

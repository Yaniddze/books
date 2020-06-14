// Core
import React, { FC, useEffect, useState } from 'react';

// Components
import { BookItem } from '../../components/book-item';
import { Button } from '../../components/button';

// Hooks
import { useBookFetch } from '../../domain/book/hooks/useBookFetch';
import { useAuthorsFetch } from '../../domain/author/hooks/useAuthorsFetch';
import { useGenresFetch } from '../../domain/genre/hooks/useGenresFetch';

// Types
import { Book } from '../../domain/book/types';

// Styles
import styles from './styles.module.scss';

type PropTypes = {
  children?: never;
}

export const BookPage: FC<PropTypes> = () => {
  useEffect(() => {
    document.title = 'Книги';
  }, []);

  const bookFetchState = useBookFetch();
  const authorsFetchState = useAuthorsFetch();
  const genresFetchState = useGenresFetch();

  const [selected, setSelected] = useState<string[]>([]);

  const loading = authorsFetchState.isFetching
    && genresFetchState.isFetching
    && bookFetchState.isFetching
    && <p>Loading...</p>;
  const errors = !bookFetchState.isFetching
    && !bookFetchState.data.success
    && bookFetchState.data.errors.map(
      (error: string) => (
        <p key={error}>
          {error}
        </p>
      ),
    );

  const onItemClick = (id: string): void => {
    setSelected(((prevState: string[]) => {
      if (!prevState.includes(id)) {
        return [
          ...prevState,
          id,
        ];
      }
      return prevState.filter((value: string) => value !== id);
    }));
  };

  const deleteIcon = selected.length > 0 && (
    // eslint-disable-next-line @typescript-eslint/no-empty-function
    <Button onClick={(): void => {

    }}
    >
      { `Удалить выбранное (${selected.length})` }
    </Button>
  );

  const books = !bookFetchState.isFetching
    && !authorsFetchState.isFetching
    && authorsFetchState.data.success
    && !genresFetchState.isFetching
    && genresFetchState.data.success
    && bookFetchState.data.success
    && bookFetchState.data.books.map(
      (book: Book) => (
        <BookItem
          key={book.id}
          book={book}
          onItemClick={onItemClick}
          authors={authorsFetchState.data.authors}
          genres={genresFetchState.data.genres}
        />
      ),
    );

  return (
    <>
      {loading}
      {errors}
      {deleteIcon}
      <div style={{ display: 'flex' }}>
        <div className={styles.bookItemsContainer}>
          {books}
        </div>
      </div>
      <div style={{
        position: 'fixed',
        bottom: 0,
        right: 0,
        margin: '10px',
      }}
      >
        <button style={{ padding: '5px', borderRadius: '50%' }} type="submit">
          +
        </button>
      </div>
    </>
  );
};

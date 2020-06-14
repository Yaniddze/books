// Core
import React, { FC, useEffect, useState } from 'react';

// Components
import { BookItem } from '../../components/book-item';
import { Button, RoundButton } from '../../components/button';
import { BookItemAdded } from '../../components/book-item/item-added';

// Hooks
import { useBookFetch } from '../../domain/book/hooks/useBookFetch';
import { useAuthorsFetch } from '../../domain/author/hooks/useAuthorsFetch';
import { useGenresFetch } from '../../domain/genre/hooks/useGenresFetch';
import { useBookAdd } from '../../domain/book/hooks/useBookAdd';

// Types
import { Book, BookToAdd } from '../../domain/book/types';

// Styles
import styles from './styles.module.scss';

type PropTypes = {
  children?: never;
}

export const BookPage: FC<PropTypes> = () => {
  useEffect(() => {
    document.title = 'Книги';
  }, []);

  const dispatch = useBookAdd();

  const bookFetchState = useBookFetch();
  const authorsFetchState = useAuthorsFetch();
  const genresFetchState = useGenresFetch();

  const [selected, setSelected] = useState<string[]>([]);
  const [addedItem, setAddedItem] = useState();

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
          {addedItem}
        </div>
      </div>
      <div style={{
        position: 'fixed',
        bottom: 0,
        right: 0,
        margin: '10px',
      }}
      >
        <RoundButton onClick={(): void => {
          setAddedItem(
            <BookItemAdded
              authors={authorsFetchState.data.authors}
              genres={genresFetchState.data.genres}
              onCancel={(): void => {
                setAddedItem('');
              }}
              onSubmit={(book: BookToAdd): void => {
                setAddedItem('');
                dispatch(book);
              }}
            />,
          );
        }}
        />
      </div>
    </>
  );
};
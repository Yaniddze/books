// Core
import React, { FC, useEffect, useState } from 'react';

// Components
import { BookItem } from '../../components/book-item';
import { Button } from '../../components/button';

// Hooks
import { useBookFetch } from '../../domain/book/hooks/useBookFetch';

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

  const { data, isFetching } = useBookFetch();
  const [selected, setSelected] = useState<string[]>([]);

  const loading = isFetching && <p>Loading...</p>;
  const errors = !isFetching && !data.success && data.errors.map(
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
    <Button onClick={(): void => {
      console.log('clicked');
    }}
    >
      { `Удалить выбранное (${selected.length})` }
    </Button>
  );

  const books = !isFetching && data.success && data.books.map(
    (book: Book) => (
      <BookItem
        key={book.id}
        book={book}
        onItemClick={onItemClick}
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
    </>
  );
};

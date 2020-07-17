// Core
import React, {
  FC,
  ReactElement,
  useEffect,
  useState,
} from 'react';

// Components
import { BookItem } from '../../components/book-item';
import { Button, RoundButton } from '../../components/button';
import { BookItemAdded } from '../../components/book-item/item-added';
import { TokenIdentity } from '../../components/identity';

// Hooks
import { useBookFetch } from '../../domain/book/hooks/useBookFetch';
import { useAuthorsFetch } from '../../domain/author/hooks/useAuthorsFetch';
import { useGenresFetch } from '../../domain/genre/hooks/useGenresFetch';
import { useBookAdd } from '../../domain/book/hooks/useBookAdd';
import { useBooksDelete } from '../../domain/book/hooks/useBooksDelete';

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

  const addDispatch = useBookAdd();
  const deleteDispatch = useBooksDelete();

  const bookFetchState = useBookFetch();
  const authorsFetchState = useAuthorsFetch();
  const genresFetchState = useGenresFetch();

  const [selected, setSelected] = useState<string[]>([]);
  const [addedItem, setAddedItem] = useState<string | ReactElement>('');

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
    <Button onClick={(): void => {
      deleteDispatch(selected);
      setSelected([]);
    }}
    >
      { `Удалить выбранное (${selected.length})` }
    </Button>
  );

  const successLoaded = !bookFetchState.isFetching
    && !authorsFetchState.isFetching
    && authorsFetchState.data.success
    && !genresFetchState.isFetching
    && genresFetchState.data.success
    && bookFetchState.data.success;

  const books = successLoaded
    && bookFetchState.data.data.map(
      (book: Book) => (
        <BookItem
          key={book.id}
          book={book}
          onItemClick={onItemClick}
          authors={authorsFetchState.data.data}
          genres={genresFetchState.data.data}
          checked={selected.includes(book.id)}
        />
      ),
    );

  const roundedButton = successLoaded
    && (
    <RoundButton onClick={(): void => {
      setAddedItem(
        <BookItemAdded
          authors={authorsFetchState.data.data}
          genres={genresFetchState.data.data}
          onCancel={(): void => {
            setAddedItem('');
          }}
          onSubmit={(book: BookToAdd): void => {
            setAddedItem('');
            addDispatch(book);
          }}
        />,
      );
    }}
    />
    );

  return (
    <TokenIdentity redirectOnEmpty={false} to="/login">
      <div>
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
          {roundedButton}
        </div>
      </div>
    </TokenIdentity>
  );
};

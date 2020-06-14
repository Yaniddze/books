// Core
import React, { FC, useState } from 'react';

// Types
import { Book } from '../../domain/book/types';

// Styles
// import styles from './styles.module.scss';

// Components
import { Button } from '../button';
import { BookItemEdit } from './item-edit';
import { BookItemShow } from './item-show';
import { Author } from '../../domain/author/types';
import { Genre } from '../../domain/genre/types';
import { useBookUpdate } from '../../domain/book/hooks/useBookUpdate';

type PropTypes = {
  children?: never;
  book: Book;
  onItemClick: (id: string) => void;
  authors: Author[];
  genres: Genre[];
}

export const BookItem: FC<PropTypes> = ({
  book, onItemClick, authors, genres,
}: PropTypes) => {
  const [editButtonShown, setEditButtonShown] = useState(true);

  const { dispatch } = useBookUpdate();

  const editButton = (
    <Button
      onClick={(): void => {
        setEditButtonShown(false);
      }}
    >
      Изменить
    </Button>
  );

  const cancelButton = (
    <Button onClick={(): void => { setEditButtonShown(true); }}>Cancel</Button>
  );

  return (
    <div style={{ marginTop: '10px' }}>
      <div>
        { editButtonShown ? editButton : cancelButton }
      </div>
      { editButtonShown
        ? <BookItemShow book={book} onItemClick={onItemClick} />
        : (
          <BookItemEdit
            onSubmit={(values): void => {
              setEditButtonShown(true);
              dispatch({
                bookId: values.id,
                newGenreId: values.genreId,
                newAuthorId: values.authorId,
                newYear: Number(values.year),
                newTitle: values.title,
                newGenreTitle: genres.find((x) => x.id === values.genreId)?.title || '',
                newAuthorName: authors.find((x) => x.id === values.authorId)?.name || '',
              });
            }}
            book={book}
            authors={authors}
            genres={genres}
          />
        )}
    </div>
  );
};

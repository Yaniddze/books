// Core
import React, { FC, useState } from 'react';

// Types
import { Book } from '../../domain/book/types';

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
  const [clicked, setClicked] = useState(false);

  const { dispatch } = useBookUpdate();

  const editButton = (
    <Button
      onClick={(): void => {
        setEditButtonShown(false);
        if (clicked) {
          onItemClick(book.id);
        }
        setClicked(false);
      }}
    >
      Изменить
    </Button>
  );

  const cancelButton = (
    <Button onClick={(): void => { setEditButtonShown(true); }}>Cancel</Button>
  );

  return (
    <div style={{ margin: '10px' }}>
      <div>
        { editButtonShown ? editButton : cancelButton }
      </div>
      { editButtonShown
        ? (
          <BookItemShow
            book={book}
            clicked={clicked}
            onItemClick={(): void => {
              setClicked((prevState) => !prevState);
              onItemClick(book.id);
            }}
          />
        )
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

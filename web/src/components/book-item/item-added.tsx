// Core
import React, { FC } from 'react';

// Components
import { BookItemEdit } from './item-edit';

// Types
import { Author } from '../../domain/author/types';
import { Genre } from '../../domain/genre/types';
import { Button } from '../button';
import { BookToAdd } from '../../domain/book/types';

type PropTypes = {
  authors: Author[];
  genres: Genre[];
  onCancel: () => void;
  onSubmit: (book: BookToAdd) => void;
}

export const BookItemAdded: FC<PropTypes> = ({
  authors, genres, onCancel, onSubmit,
}: PropTypes) => (
  <div>
    <div style={{ margin: '7px' }}>
      <Button onClick={onCancel}>Cancel</Button>
    </div>

    <BookItemEdit
      book={
          {
            bookInfo: {
              title: '',
              year: 0,
            },
            author: {
              id: '',
              name: '',
            },
            genre: {
              id: '',
              title: '',
            },
            id: '',
          }
        }
      authors={authors}
      genres={genres}
      onSubmit={(values): void => {
        onSubmit({
          bookTitle: values.title,
          year: Number(values.year),
          authorId: values.authorId,
          genreId: values.genreId,
          genreTitle: genres.find((x) => x.id === values.genreId)?.title || '',
          authorName: authors.find((x) => x.id === values.authorId)?.name || '',
        });
      }}
    />
  </div>
);

// Core
import React, { FC } from 'react';
import { useForm } from 'react-hook-form';

// Types
import { Book } from '../../domain/book/types';
import { Author } from '../../domain/author/types';
import { Genre } from '../../domain/genre/types';

// Components
// import { Button } from '../button';

// Styles
import styles from './styles.module.scss';

type PropTypes = {
  children?: never;
  book: Book;
  authors: Author[];
  genres: Genre[];
  onSubmit: (values: Record<string, string>) => void;
}

export const BookItemEdit: FC<PropTypes> = ({
  book, genres, authors, onSubmit,
}: PropTypes) => {
  const { handleSubmit, register, errors } = useForm();
  const authorSelect = (
    <select className={styles.editSelect} name="authorId" defaultValue={book.author.id} ref={register}>
      {authors.map((author: Author) => (
        <option key={author.id + author.name} value={author.id}>
          {author.name}
        </option>
      ))}
    </select>
  );

  const genreSelect = (
    <select className={styles.editSelect} name="genreId" defaultValue={book.genre.id} ref={register}>
      {genres.map((genre: Genre) => (
        <option key={genre.id + genre.title} value={genre.id}>
          {genre.title}
        </option>
      ))}
    </select>
  );

  return (
    <>
      <form onSubmit={handleSubmit(onSubmit)}>
        <input
          name="id"
          type="hidden"
          ref={register}
          value={book.id}
        />

        <div className={styles.editInputTitle}>Название</div>
        <span className={styles.error}>{errors.title && errors.title.message}</span>
        <input
          name="title"
          className={styles.editInput}
          defaultValue={book.bookInfo.title}
          ref={register({
            required: 'Required',
          })}
        />

        <div className={styles.editInputTitle}>Год</div>
        <span className={styles.error}>{errors.year && errors.year.message}</span>
        <input
          name="year"
          className={styles.editInput}
          defaultValue={book.bookInfo.year}
          type="number"
          ref={register({
            required: 'Required',
            min: 0,
            max: 2020,
          })}
        />

        <div className={styles.editInputTitle}>Автор</div>
        <span className={styles.error}>{errors.authorId && errors.authorId.message}</span>
        {authorSelect}

        <div className={styles.editInputTitle}>Жанр</div>
        <span className={styles.error}>{errors.genreId && errors.genreId.message}</span>
        {genreSelect}

        <div>
          <button type="submit">Submit</button>
        </div>
      </form>
    </>
  );
};

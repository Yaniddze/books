// Core
import React, { FC, useState } from 'react';

// Types
import { Book } from '../../domain/book/types';

// Styles
import styles from './styles.module.scss';

type PropTypes = {
  children?: never;
  book: Book;
  onItemClick: (id: string) => void;
}

export const BookItem: FC<PropTypes> = ({ book, onItemClick }: PropTypes) => {
  const [clicked, setClicked] = useState(false);

  return (
    // eslint-disable-next-line max-len
    // eslint-disable-next-line jsx-a11y/click-events-have-key-events,jsx-a11y/no-static-element-interactions
    <div
      className={`${styles.bookItem} ${clicked ? styles.bookItemClicked : ''}`}
      onClick={(): void => {
        setClicked((prevState: boolean) => !prevState);
        onItemClick(book.id);
      }}
    >
      <div>
        <b>
          id:
        </b>
        { ` ${book.id}` }
      </div>
      <div>
        <b>
          Название:
        </b>
        { ` ${book.bookInfo.title}` }
      </div>
      <div>
        <b>
          Год:
        </b>
        { ` ${book.bookInfo.year}` }
      </div>
      <div>
        <b>
          Автор:
        </b>
        { ` ${book.author.name}` }
      </div>
      <div>
        <b>
          Жанр:
        </b>
        { ` ${book.genre.title}` }
      </div>
    </div>
  );
};

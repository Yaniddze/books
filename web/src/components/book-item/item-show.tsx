// Core
import React, { FC } from 'react';

// Types
import { Book } from '../../domain/book/types';

// Styles
import styles from './styles.module.scss';

// Components

type PropTypes = {
  children?: never;
  book: Book;
  onItemClick: (id: string) => void;
}

export const BookItemShow: FC<PropTypes> = ({ book }: PropTypes) => (
  <div style={{ marginTop: '10px' }}>
    <div
      className={`${styles.bookItem}`}
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
  </div>
);

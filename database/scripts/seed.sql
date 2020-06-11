CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE genre(
    id uuid PRIMARY KEY,
    title varchar(30)
);

CREATE TABLE author(
    id uuid PRIMARY KEY,
    name varchar(50)
);

CREATE TABLE books(
    id uuid PRIMARY KEY,
    title varchar(50),
    creation_date date,
    genre_id uuid REFERENCES genre(id),
    author_id uuid REFERENCES author(id)
);

INSERT INTO genre VALUES (uuid_generate_v4(), 'Detective'), (uuid_generate_v4(), 'Action');

INSERT INTO author VALUES (uuid_generate_v4(), 'Zinevich Yan'), (uuid_generate_v4(), 'Bagrov Nikolay');

INSERT INTO books VALUES (uuid_generate_v4(), 'New book', NOW(), (SELECT id FROM genre WHERE title = 'Detective' LIMIT 1), (SELECT id FROM author WHERE name = 'Zinevich Yan' LIMIT 1));
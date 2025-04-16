CREATE TABLE tasks (
    id SERIAL PRIMARY KEY,
    title TEXT NOT NULL,
    description TEXT,
    iscompleted BOOLEAN NOT NULL DEFAULT FALSE,
    createdat TIMESTAMP NOT NULL DEFAULT NOW()
);


select * from tasks;

INSERT INTO tasks (title, description, iscompleted)
VALUES ('Buy groceries', 'Milk, Bread, Eggs', false);
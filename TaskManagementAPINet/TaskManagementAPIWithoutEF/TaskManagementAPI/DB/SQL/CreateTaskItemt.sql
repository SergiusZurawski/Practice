CREATE TABLE tasks (
    id SERIAL PRIMARY KEY,
    title TEXT NOT NULL,
    description TEXT,
    iscompleted BOOLEAN NOT NULL DEFAULT FALSE,
    createdat TIMESTAMP NOT NULL DEFAULT NOW(),
    user_id INTEGER,
    FOREIGN KEY (user_id) REFERENCES users(id)
);


select * from tasks;

INSERT INTO tasks (title, description, iscompleted)
VALUES ('Buy groceries', 'Milk, Bread, Eggs', false);


CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    username TEXT UNIQUE NOT NULL,
    passwordHash TEXT NOT NULL,
    role TEXT NOT NULL DEFAULT 'User'
);

INSERT INTO users (username, passwordHash, role)
VALUES ('admin', '$2a$11$t.iWnIrWUYtBjvXnxSqODex2pqOQ9ytnYHSufgw/1eDisY4C3SI92', 'Admin');

select * from users;
select * from tasks;
DROP TABLE users;


-- Add the user_id column to the tasks table
ALTER TABLE tasks
ADD COLUMN user_id INTEGER;

-- Create the foreign key constraint
ALTER TABLE tasks
ADD CONSTRAINT fk_user
FOREIGN KEY (user_id)
REFERENCES users(id);

UPDATE tasks SET user_id = 2 WHERE id=4
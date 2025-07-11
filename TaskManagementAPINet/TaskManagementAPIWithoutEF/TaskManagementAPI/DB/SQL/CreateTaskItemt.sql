CREATE TABLE tasks (
    id SERIAL PRIMARY KEY,
    title TEXT NOT NULL,
    description TEXT,
    iscompleted BOOLEAN NOT NULL DEFAULT FALSE,
    createdat TIMESTAMP NOT NULL DEFAULT NOW(),
    task_duedate DATE, -- New field for the due date
    task_priority INTEGER, -- New field for priority (e.g., 1 for high, 2 for medium, 3 for low)
    user_id INTEGER,
    FOREIGN KEY (user_id) REFERENCES users(id)
);

CREATE TABLE tasks (
    id SERIAL PRIMARY KEY,
    title TEXT NOT NULL,
    description TEXT,
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    task_due_date DATE,
    task_priority INTEGER,
    user_id INTEGER,
    task_status_id INTEGER,
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (task_status_id) REFERENCES task_status(id)
);

ALTER TABLE tasks
ADD COLUMN task_duedate DATE;

ALTER TABLE tasks
ADD COLUMN task_priority INTEGER;


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

-- task_status
CREATE TABLE task_status (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL UNIQUE,
    row_inserted TIMESTAMP NOT NULL DEFAULT NOW(),
    row_updated TIMESTAMP NOT NULL DEFAULT NOW()
);

ALTER TABLE tasks
DROP COLUMN iscompleted,
ADD COLUMN task_status_id INTEGER,
ADD CONSTRAINT fk_task_status
    FOREIGN KEY (task_status_id) REFERENCES task_status(id);

INSERT INTO task_status (name) VALUES
('open'),
('in_progress'),
('pending'),
('paused'),
('blocked'),
('completed'),
('deleted'),
('no_longer_needed'),
('archived');

select * from task_status;

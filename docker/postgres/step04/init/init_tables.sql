CREATE TABLE recipe (
    id UUID PRIMARY KEY,
    name TEXT NOT NULL,
    category TEXT NOT NULL,
    ingredients TEXT NOT NULL,
    instructions TEXT NOT NULL
);

CREATE TABLE rating (
    id UUID PRIMARY KEY,
    recipe_id UUID NOT NULL,
    value INT NOT NULL,
    comment TEXT,
    FOREIGN KEY (recipe_id) REFERENCES recipe(id)
);
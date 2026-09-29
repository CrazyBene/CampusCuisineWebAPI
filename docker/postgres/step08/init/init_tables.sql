CREATE TABLE recipe (
    id UUID PRIMARY KEY,
    user_id UUID NOT NULL,
    name TEXT NOT NULL,
    category TEXT NOT NULL,
    ingredients TEXT NOT NULL,
    instructions TEXT NOT NULL
);

CREATE TABLE rating (
    id UUID PRIMARY KEY,
    user_id UUID NOT NULL,
    recipe_id UUID NOT NULL,
    value INT NOT NULL,
    comment TEXT,
    FOREIGN KEY (recipe_id) REFERENCES recipe(id)
);
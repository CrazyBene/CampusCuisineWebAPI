CREATE TABLE recipe (
    user_id UUID NOT NULL,
    id UUID NOT NULL,
    name TEXT NOT NULL,
    category TEXT NOT NULL,
    ingredients TEXT NOT NULL,
    instructions TEXT NOT NULL,
    PRIMARY KEY (user_id, id),
    UNIQUE(id)
);

CREATE TABLE rating (
    user_id UUID NOT NULL,
    id UUID NOT NULL,
    recipe_id UUID NOT NULL,
    value INT NOT NULL,
    comment TEXT,
    PRIMARY KEY (user_id, id),
    UNIQUE(id),
    FOREIGN KEY (recipe_id) REFERENCES recipe(id)
);
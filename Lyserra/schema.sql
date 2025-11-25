CREATE TABLE IF NOT EXISTS Master (
    masterID INTEGER PRIMARY KEY AUTOINCREMENT,
    masterName TEXT NOT NULL,
    specialTrait TEXT,
    masterType TEXT
);

CREATE TABLE IF NOT EXISTS Pet (
    petID INTEGER PRIMARY KEY AUTOINCREMENT,
    masterID INTEGER NOT NULL,
    Type TEXT,
    petName TEXT,
    weight TEXT,
    age TEXT,
    breed TEXT,
    hairColor TEXT,
    colorDesign TEXT,
    hairCut TEXT,
    eyeColor TEXT,
    accessory TEXT,
    personality TEXT,
    scent TEXT,
    mutation TEXT,
    element TEXT,
    crystal TEXT,
    evolution TEXT,
    FOREIGN KEY(masterID) REFERENCES Master(masterID)
);

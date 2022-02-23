INSERT INTO
    [User]
VALUES (
    'Gabriel Silva',
    'gabrielhs1357@gmail.com',
    'PASSWORDHASH',
    'Bio',
    'image-blob.com',
    'gabriel-silva'
)

SELECT * FROM [User]

/*
    [Id] INT NOT NULL IDENTITY(1, 1),
    [Name] NVARCHAR(80) NOT NULL,
    [Email] VARCHAR(200) NOT NULL,
    [PasswordHash] VARCHAR(255) NOT NULL,
    [Bio] TEXT NOT NULL,
    [Image] VARCHAR(2000) NOT NULL,
    [Slug] VARCHAR(80) NOT NULL, -- User URL
*/
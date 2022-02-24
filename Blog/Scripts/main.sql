-- #region Inserts

INSERT INTO
    [User]
VALUES
    (
        'Gabriel Silva',
        'gabrielhs1357@gmail.com',
        'PASSWORDHASH',
        'Bio',
        'image-blob.com',
        'gabriel-silva'
)

INSERT INTO
    [Role]
VALUES
    (
        'Autor',
        'author'
    )

INSERT INTO
    [Tag]
VALUES
    (
        'ASP.NET',
        'aspnet'
    )

INSERT INTO
    [UserRole]
VALUES
    (
        1,
        1
    )

-- #endregion

-- #region Selects

SELECT *
FROM [User]

SELECT *
FROM [Role]

SELECT *
FROM [UserRole]

SELECT *
FROM [Tag]

-- Trazer todos os Users junto com todas as suas Roles
-- Importante usar Left Join ao invés do Inner Join porque dessa forma trazemos todos os Users mesmo que eles não tenham nenhuma Role

SELECT
    [User].*,
    [Role].*
FROM [User]
    LEFT JOIN [UserRole] ON [UserRole].[UserId] = [User].[Id]
    LEFT JOIN [Role] ON [UserRole].[RoleId] = [Role].[Id]
    
-- #endregion

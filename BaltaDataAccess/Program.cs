using Microsoft.Data.SqlClient;
using Dapper;
using BaltaDataAccess.Models;
using System.Data;

const string connnectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$; TrustServerCertificate=True";

using (var connection = new SqlConnection(connnectionString))
{
    // CreateManyCategories(connection);
    // ListCategories(connection);
    // UpdateCategory(connection);
    // CreateCategory(connection);
    // ExecuteProcedure(connection);
    // ExecuteReadProcedure(connection);
    // ExecuteScalar(connection);
    // ReadView(connection);
    // OneToMany(connection);
    // QueryMultiple(connection);
    // SelectIn(connection);
    // Like(connection, "api");
    Transaction(connection);
}

static void ListCategories(SqlConnection connection)
{
    var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");

    foreach (var item in categories)
    {
        System.Console.WriteLine($"{item.Id} - {item.Title}");
    }
}

static void CreateCategory(SqlConnection connection)
{
    var category = new Category();

    category.Id = Guid.NewGuid();
    category.Title = "Amazon AWS";
    category.Url = "amazon";
    category.Summary = "AWS Cloud";
    category.Order = 8;
    category.Description = "Serviços do AWS";
    category.Featured = false;

    var insertSql = @"INSERT INTO
                    [Category]
                VALUES
                    (@Id,
                    @Title,
                    @Url,
                    @Summary,
                    @Order,
                    @Description,
                    @Featured)";

    var rows = connection.Execute(insertSql, new
    {
        category.Id,
        category.Title,
        category.Url,
        category.Summary,
        category.Order,
        category.Description,
        category.Featured
    });

    Console.WriteLine($"{rows} linhas inseridas.");
}

static void UpdateCategory(SqlConnection connection)
{
    string updateQuery = "UPDATE [Category] SET [Title]=@title WHERE [Id]=@id";

    int rows = connection.Execute(updateQuery, new
    {
        id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
        title = "Frontend 2022"
    });

    Console.WriteLine($"{rows} registros atualizados");
}

static void CreateManyCategories(SqlConnection connection)
{
    var category = new Category();

    category.Id = Guid.NewGuid();
    category.Title = "Amazon AWS";
    category.Url = "amazon";
    category.Summary = "AWS Cloud";
    category.Order = 8;
    category.Description = "Serviços do AWS";
    category.Featured = false;

    var category2 = new Category();

    category2.Id = Guid.NewGuid();
    category2.Title = "Categoria nova";
    category2.Url = "categoria-nova";
    category2.Summary = "Categoria nova";
    category2.Order = 9;
    category2.Description = "Categoria";
    category2.Featured = true;

    var insertSql = @"INSERT INTO
                    [Category]
                VALUES
                    (@Id,
                    @Title,
                    @Url,
                    @Summary,
                    @Order,
                    @Description,
                    @Featured)";

    var rows = connection.Execute(insertSql, new[]{
        new {
            category.Id,
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured
        },
        new {
            category2.Id,
            category2.Title,
            category2.Url,
            category2.Summary,
            category2.Order,
            category2.Description,
            category2.Featured
        }
    });

    Console.WriteLine($"{rows} linhas inseridas.");
}

static void ExecuteProcedure(SqlConnection connection)
{
    string procedure = "[dbo].[spDeleteStudent]";

    var param = new { StudentId = "5b608805-b8be-7b3d-00f6-114e00000000" };

    int affectedRows = connection.Execute(procedure,
                       param,
                       commandType: CommandType.StoredProcedure);

    Console.WriteLine($"{affectedRows} linhas afetadas");
}

static void ExecuteReadProcedure(SqlConnection connection)
{
    string procedure = "spGetCoursesByCategory";

    var param = new { CategoryId = "03890bbc-d90d-43b7-a32a-0d523a9f77ab" };

    var courses = connection.Query(procedure,
                       param,
                       commandType: CommandType.StoredProcedure);

    foreach (var item in courses)
        Console.WriteLine(item.Title);
}

static void ExecuteScalar(SqlConnection connection)
{
    var category = new Category();

    category.Title = "Amazon AWS 3";
    category.Url = "amazon";
    category.Summary = "AWS Cloud";
    category.Order = 8;
    category.Description = "Serviços do AWS";
    category.Featured = false;

    var insertSql = @"INSERT INTO
                    [Category]
                OUTPUT inserted.[Id]
                VALUES
                    (NEWID(),
                    @Title,
                    @Url,
                    @Summary,
                    @Order,
                    @Description,
                    @Featured)";

    var id = connection.ExecuteScalar<Guid>(insertSql, new
    {
        category.Title,
        category.Url,
        category.Summary,
        category.Order,
        category.Description,
        category.Featured
    });

    Console.WriteLine($"ID da categoria inserida: {id}.");
}

static void ReadView(SqlConnection connection)
{
    string sql = "SELECT * FROM vwCourses";

    var courses = connection.Query(sql);

    foreach (var item in courses)
    {
        System.Console.WriteLine($"{item.Id} - {item.Title}");
    }
}

static void OneToOne(SqlConnection connection)
{
    string sql = @"
                    SELECT 
                        *
                    FROM
                        [CareerItem]
                    INNER JOIN
                        [Course] ON [CareerItem].[CourseId] = [Course].[Id]";

    var careerItems = connection.Query<CareerItem, Course, CareerItem>(
        sql,
        (careerItem, course) =>
    {
        careerItem.Course = course;
        return careerItem;
    }, splitOn: "Id");

    foreach (var careerItem in careerItems)
        Console.WriteLine($"{careerItem.Title} - {careerItem.Course.Title}");
}

static void OneToMany(SqlConnection connection)
{
    string sql = @"
                    SELECT 
                        [Career].[Id],
                        [Career].[Title],
                        [CareerItem].[CareerId],
                        [CareerItem].[Title]
                    FROM 
                        [Career] 
                    INNER JOIN 
                        [CareerItem] ON [CareerItem].[CareerId] = [Career].[Id]
                    ORDER BY
                        [Career].[Title]";

    var careers = new List<Career>();

    var items = connection.Query<Career, CareerItem, Career>(
        sql,
        (career, careerItem) =>
    {
        var car = careers.Where(x => x.Id == career.Id).FirstOrDefault();

        if (car == null)
        {
            car = career;
            car.Items.Add(careerItem);
            careers.Add(car);
        }
        else
            car.Items.Add(careerItem);

        return career;
    }, splitOn: "CareerId");

    foreach (var career in careers)
    {
        Console.WriteLine(career.Title);

        foreach (var careerItem in career.Items)
            Console.WriteLine($" - {careerItem.Title}");
    }
}

static void QueryMultiple(SqlConnection connection)
{
    var sql = "SELECT * FROM [Category]; SELECT * FROM [Course];";

    using (var multi = connection.QueryMultiple(sql))
    {
        var categories = multi.Read<Category>();
        var courses = multi.Read<Course>();

        foreach (var item in categories)
            Console.WriteLine(item.Title);

        foreach (var item in courses)
            Console.WriteLine(item.Title);
    }
}

static void SelectIn(SqlConnection connection)
{
    string sql = @"
                    SELECT * FROM Career WHERE [Id] IN @Id";

    var items = connection.Query<Career>(sql, new
    {
        Id = new[] {
            "e6730d1c-6870-4df3-ae68-438624e04c72",
            "01ae8a85-b4e8-4194-a0f1-1c6190af54cb",
        }
    });

    foreach (var item in items)
        Console.WriteLine(item.Title);
}

static void Like(SqlConnection connection, string term)
{
    string sql = @"SELECT * FROM [Course] WHERE [Title] LIKE @exp";

    var items = connection.Query<Career>(sql, new
    {
        exp = $"%{term}%"
    });

    foreach (var item in items)
        Console.WriteLine(item.Title);
}

static void Transaction(SqlConnection connection)
{
    var category = new Category();

    category.Id = Guid.NewGuid();
    category.Title = "Amazon AWS ZZZ";
    category.Url = "amazon";
    category.Summary = "AWS Cloud";
    category.Order = 8;
    category.Description = "Serviços do AWS";
    category.Featured = false;

    var insertSql = @"INSERT INTO
                    [Category]
                VALUES
                    (@Id,
                    @Title,
                    @Url,
                    @Summary,
                    @Order,
                    @Description,
                    @Featured)";

    connection.Open();

    using (var transaction = connection.BeginTransaction())
    {
        var rows = connection.Execute(insertSql, new
        {
            category.Id,
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured
        }, transaction);

        transaction.Commit();
        // transaction.Rollback();
    }
}

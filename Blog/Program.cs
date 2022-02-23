using Blog.Models;
// using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

// ReadUsers();
// ReadUser();
CreateUser();

const string CONNECTION_STRING = @"Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$; TrustServerCertificate=True";

static void ReadUsers()
{
    using (var connection = new SqlConnection(CONNECTION_STRING))
    {
        // GetAll => SELECT * FROM
        var users = connection.GetAll<User>();

        foreach (var user in users)
            Console.WriteLine(user.Name);
    }
}

static void ReadUser()
{
    using (var connection = new SqlConnection(CONNECTION_STRING))
    {
        // Get => SELECT * FROM ... WHERE ID = ...
        var user = connection.Get<User>(1);

        Console.WriteLine(user.Name);
    }
}

static void CreateUser()
{
    var user = new User()
    {
        Name = "Equipe balta.io",
        Email = "hello@balta.io",
        PasswordHash = "PASSWORDHASH",
        Bio = "Equipe do balta",
        Image = "https://...",
        Slug = "equipe-balta"
    };

    using (var connection = new SqlConnection(CONNECTION_STRING))
    {
        // Get => SELECT * FROM ... WHERE ID = ...
        var id = connection.Insert<User>(user);

        Console.WriteLine(id);
    }
}

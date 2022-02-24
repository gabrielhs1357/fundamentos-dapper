using Blog.Models;
using Blog.Repositories;
// using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

const string CONNECTION_STRING = @"Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$; TrustServerCertificate=True";

var connection = new SqlConnection(CONNECTION_STRING);

connection.Open();

ReadUsers(connection);
// ReadUser();
// CreateUser();
// UpdateUser();

connection.Close();


static void ReadUsers(SqlConnection connection)
{
    var repository = new UserRepository(connection);
    var users = repository.Get();

    foreach (var user in users)
        Console.WriteLine(user.Name);
}

static void ReadUser(SqlConnection connection)
{
    var repository = new UserRepository(connection);

    // Get => SELECT * FROM ... WHERE ID = ...
    var user = connection.Get<User>(1);

    Console.WriteLine(user.Name);

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
        var id = connection.Insert<User>(user);

        Console.WriteLine($"Id gerado: {id}");
    }
}

static void UpdateUser()
{
    var user = new User()
    {
        Id = 2, // Obrigatório passa o Id para fazer o update
        Name = "Equipe balta.io (updated)",
        Email = "hello@balta.io",
        PasswordHash = "PASSWORDHASH",
        Bio = "Equipe do balta",
        Image = "https://...",
        Slug = "equipe-balta"
    };

    using (var connection = new SqlConnection(CONNECTION_STRING))
    {
        connection.Update<User>(user);

        Console.WriteLine("Update realizado.");
    }
}

static void DeleteUser()
{
    using (var connection = new SqlConnection(CONNECTION_STRING))
    {
        var user = connection.Get<User>(2);

        connection.Delete<User>(user);

        Console.WriteLine("Delete realizado.");
    }
}

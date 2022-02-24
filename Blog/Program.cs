using Blog.Models;
using Blog.Repositories;
// using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

const string CONNECTION_STRING = @"Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$; TrustServerCertificate=True";

var connection = new SqlConnection(CONNECTION_STRING);

connection.Open();

ReadUsersWithRoles(connection);
// ReadUsers(connection);
// ReadRoles(connection);
// ReadTags(connection);
// ReadUser();
// CreateUser();
// UpdateUser();

connection.Close();

static void ReadUsersWithRoles(SqlConnection connection)
{
    var repository = new UserRepository(connection);
    var users = repository.GetWithRoles();

    foreach (var user in users)
    {
        Console.WriteLine(user.Name);
        foreach (var role in user.Roles)
            Console.WriteLine(role.Name);
    }
}

static void ReadUsers(SqlConnection connection)
{
    var repository = new Repository<User>(connection);
    var users = repository.Get();

    foreach (var user in users)
        Console.WriteLine(user.Name);
}

static void ReadRoles(SqlConnection connection)
{
    var repository = new Repository<Role>(connection);
    var roles = repository.Get();

    foreach (var role in roles)
        Console.WriteLine(role.Name);
}

static void ReadTags(SqlConnection connection)
{
    var repository = new Repository<Tag>(connection);
    var tags = repository.Get();

    foreach (var tag in tags)
        Console.WriteLine(tag.Name);
}
